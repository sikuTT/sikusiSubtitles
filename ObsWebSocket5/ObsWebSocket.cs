using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ObsWebSocket5.Message;
using ObsWebSocket5.Message.Data;
using ObsWebSocket5.Message.Data.Response.InputSettings;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Security.Cryptography;
using System.Text;

namespace ObsWebSocket5 {
    internal class ReceiveData {
        public WebSocketReceiveResult? Result { get; set; }
        public JObject? Data { get; set; }
    }

    public class ObsWebSocket {
        SemaphoreSlim recvSemaphore = new SemaphoreSlim(1, 1);
        SemaphoreSlim sendSemaphore = new SemaphoreSlim(1, 1);
        ReceiveData? receiveData;

        ClientWebSocket? webSocket;
        string? password;

        public bool IsConnected { get { return webSocket != null && webSocket.State ==WebSocketState.Open; } }

        async public Task ConnectAsync(string uri) {
            webSocket = new ClientWebSocket();
            try {
                await webSocket.ConnectAsync(new Uri(uri), CancellationToken.None);
                await RecvHelloAsync();
                Task.Run(() => ReceiveMessage());
            } catch (Exception) {
                if (webSocket != null) {
                    webSocket.Dispose();
                    webSocket = null;
                }
                throw;
            }
        }

        async public Task ConnectAsync(string uri, string password) {
            this.password = password;
            await ConnectAsync(uri);
        }

        async public Task CloseAsync() {
            if (webSocket != null) {
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);
                webSocket.Dispose();
                webSocket = null;
                return;
            } else {
                return;
            }
        }

        async public Task<RequestResponse<GetSceneListResponse>?> GetSceneListAsync() {
            var requestData = new GetSceneListRequest();
            var responseData = await SendRequestAsync<GetSceneListResponse>(requestData);
            return responseData;
        }

        async public Task<RequestResponse<GetGroupSceneItemListResponse>?> GetGroupSceneItemListAsync(string sceneName) {
            var requestData = new GetGroupSceneItemListRequest() { sceneName = sceneName };
            var responseData = await SendRequestAsync<GetGroupSceneItemListResponse>(requestData);
            return responseData;
        }

        async public Task<RequestResponse<GetSceneItemListResponse>?> GetSceneItemListAsync(string sceneName) {
            var requestData = new GetSceneItemListRequest() { sceneName = sceneName };
            return await SendRequestAsync<GetSceneItemListResponse>(requestData);
        }

        async public Task<RequestResponse<GetInputDefaultSettingsResponse>?> GetInputDefaultSettingsAsync(string inputKind) {
            var requestData = new GetInputDefaultSettingsRequest() { inputKind = inputKind };
            var responseData = await SendRequestAsync<GetInputDefaultSettingsResponse>(requestData);
            if (responseData?.d?.responseData != null) {
                var settings = responseData.d.responseData.defaultInputSettings as JObject;
                if (settings != null) {
                    if (inputKind == "text_gdiplus_v2") {
                        var data = settings.ToObject<TextGdiplusV2>();
                        if (data != null) {
                            responseData.d.responseData.defaultInputSettings = data;
                        }
                    }
                }
            }
            return responseData;
        }

        async public Task<RequestResponse<GetInputSettingsResponse>?> GetInputSettingsAsync(string inputName) {
            var requestData = new GetInputSettingsRequest() { inputName = inputName };
            var responseData = await SendRequestAsync<GetInputSettingsResponse>(requestData);
            if (responseData?.d?.responseData != null) {
                var settings = responseData.d.responseData.inputSettings as JObject;
                if (settings != null) {
                    if (responseData.d.responseData.inputKind == "text_gdiplus_v2") {
                        var data = settings.ToObject<TextGdiplusV2>();
                        if (data != null) {
                            responseData.d.responseData.inputSettings = data;
                        }
                    }
                }
            }
            return responseData;
        }

        async public Task<RequestResponse<ResponseData>?> SetInputSettingsAsync(string inputName, TextGdiplusV2 settings) {
            var requestData = new SetInputSettingsRequest() { inputName = inputName, inputSettings = settings };
            return await SendRequestAsync<ResponseData>(requestData);
        }

        /** OBSからのメッセージを待つ */
        async private Task ReceiveMessage() {
            while (IsConnected) {
                var needWaitSendSemaphore = false;
                try {
                    recvSemaphore.Wait();
                    receiveData = null;
                    receiveData = await ReceiveAsync();
                    if (receiveData?.Data != null) {
                        var op = receiveData.Data["op"];
                        if (op != null) {
                            if ((int)op == (int)Message.WebSocketOpCode.Hello) {
                                await SendIdentifyAsync(receiveData.Data.ToObject<Hello>());
                            } else if ((int)op == (int)Message.WebSocketOpCode.Identified) {
                            } else if ((int)op == (int)Message.WebSocketOpCode.Event) {
                            } else if ((int)op == (int)Message.WebSocketOpCode.RequestResponse) {
                                needWaitSendSemaphore = true;
                            }
                        }
                    } else {
                        break;
                    }
                } finally {
                    recvSemaphore.Release();
                }

                if (needWaitSendSemaphore) {
                    sendSemaphore.Wait(5000);
                    sendSemaphore.Release();
                }
            }
        }

        /** OBSからのメッセージを読み込む */
        async private Task<ReceiveData?> ReceiveAsync() {
            if (webSocket == null) return null;

            ReceiveData data = new ReceiveData();
            var buffer = new List<byte>();

            do {
                var temp = new byte[1024];
                var segment = new ArraySegment<byte>(temp);
                data.Result = await webSocket.ReceiveAsync(segment, CancellationToken.None);

                switch (data.Result.MessageType) {
                    case WebSocketMessageType.Close:
                        await CloseAsync();
                        return data;
                    case WebSocketMessageType.Binary:
                        await CloseAsync();
                        return data;
                    default:
                        break;
                }

                buffer.AddRange(temp);
            } while (!data.Result.EndOfMessage);

            var message = Encoding.UTF8.GetString(buffer.ToArray(), 0, buffer.Count);
            Debug.WriteLine(message);
            Debug.WriteLine("");
            data.Data = JsonConvert.DeserializeObject<JObject>(message);
            return data;
        }

        /** OBSにメッセージを送信しレスポンスを受け取る */
        async private Task<RequestResponse<T>?> SendRequestAsync<T>(RequestData requestData) where T : ResponseData {
            var requestId = Guid.NewGuid().ToString();
            var request = new Request();
            request.d.requestType = requestData.GetRequestType();
            request.d.requestId = requestId;
            request.d.requestData = requestData;

            sendSemaphore.Wait();
            await SendAsync(request);
            try {
                recvSemaphore.Wait();
                if (receiveData?.Data != null) {
                    var data = receiveData.Data.ToObject<RequestResponse<T>>();
                    receiveData = null;
                    return data;
                }
            } finally {
                recvSemaphore.Release();
                sendSemaphore.Release();
            }
            return null;
        }

        /** OBSにメッセージを送信する */
        async ValueTask SendAsync<T>(T message) {
            if (webSocket != null) {
                var jsonString = JsonConvert.SerializeObject(message, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                byte[] buffer = Encoding.UTF8.GetBytes(jsonString);
                var memory = new Memory<byte>(buffer);
                await webSocket.SendAsync(memory, WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }

        /** Hello (OpCode 0) */
        async private Task RecvHelloAsync() {
            if (webSocket != null) {
                var data = await ReceiveAsync();
                if (data?.Data != null) {
                    await SendIdentifyAsync(data.Data.ToObject<Hello>());
                    await RecvIdentifiedAsync();
                    return;
                }
            }
            throw new WebSocketException();
        }

        /** Identify (OpCode 1) */
        async private Task SendIdentifyAsync(Hello? hello) {
            if (hello != null) {
                // Send Identity
                var identity = new Message.Identify();
                identity.d.rpcVersion = 1;
                if (hello.d.authentication != null) {
                    using SHA256 sha256 = SHA256.Create();
                    string base64Secret = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(password + hello.d.authentication.salt)));
                    identity.d.authentication = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(base64Secret + hello.d.authentication.challenge)));
                }
                // identity.d.eventSubscriptions = (int)Message.EventSubscription.All;
                await SendAsync(identity);
            }
        }

        /** Identified (OpCode 2) */
        async private Task RecvIdentifiedAsync() {
            if (webSocket != null) {
                var data = await ReceiveAsync();
                if (data?.Data != null) {
                    return;
                } else {
                    throw new AuthenticationFailedException();
                }
            }
            throw new WebSocketException();
        }
    }
}
