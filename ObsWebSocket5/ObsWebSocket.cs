using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ObsWebSocket5.Message;
using ObsWebSocket5.Message.Data;
using ObsWebSocket5.Message.Data.Response.InputSettings;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Text;

namespace ObsWebSocket5 {
    public class ObsWebSocket {
        SemaphoreSlim recvSemaphore = new SemaphoreSlim(1, 1);
        SemaphoreSlim sendSemaphore = new SemaphoreSlim(1, 1);
        string? responseMessage;

        ClientWebSocket? webSocket;
        string? password;

        public bool IsConnected { get { return webSocket != null && webSocket.State ==WebSocketState.Open; } }

        async public Task ConnectAsync(string uri) {
            webSocket = new ClientWebSocket();
            try {
                await webSocket.ConnectAsync(new Uri(uri), CancellationToken.None);
                Task.Run(() => ReceiveMessage());
            } catch (Exception) {
                webSocket.Dispose();
                webSocket = null;
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

        /** Identify (OpCode 1) */
        async private Task SendIdentifyAsync() {
            // Send Identity
            var identity = new Message.Identify();
            identity.d.rpcVersion = 1;
            if (password != null) {
                // identity.d.authentication = "";
            }
            // identity.d.eventSubscriptions = (int)Message.EventSubscription.All;
            await SendAsync(identity);
        }

        async private Task<string?> ReceiveAsync() {
            if (webSocket == null) return null;

            var buffer = new List<byte>();
            WebSocketReceiveResult result;

            do {
                var temp = new byte[1024];
                var segment = new ArraySegment<byte>(temp);
                result = await webSocket.ReceiveAsync(segment, CancellationToken.None);

                switch (result.MessageType) {
                    case WebSocketMessageType.Close:
                        await CloseAsync();
                        return null;
                    case WebSocketMessageType.Binary:
                        await CloseAsync();
                        return null;
                    default:
                        break;
                }

                buffer.AddRange(temp);
            } while (!result.EndOfMessage);

            var message = Encoding.UTF8.GetString(buffer.ToArray(), 0, buffer.Count);
            Debug.WriteLine(message);
            Debug.WriteLine("");
            return message;
        }

        async ValueTask SendAsync<T>(T message) {
            if (webSocket != null) {
                var jsonString = JsonConvert.SerializeObject(message, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                byte[] buffer = Encoding.UTF8.GetBytes(jsonString);
                var memory = new Memory<byte>(buffer);
                await webSocket.SendAsync(memory, WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }

        async private Task ReceiveMessage() {
            while (IsConnected) {
                var needWaitSendSemaphore = false;
                try {
                    recvSemaphore.Wait();
                    var message = await ReceiveAsync();
                    if (message != null) {
                        var obj = JsonConvert.DeserializeObject<JObject>(message);
                        if (obj != null) {
                            var op = obj["op"];
                            if (op != null) {
                                if ((int)op == (int)Message.WebSocketOpCode.Hello) {
                                    await SendIdentifyAsync();
                                } else if ((int)op == (int)Message.WebSocketOpCode.Identified) {
                                } else if ((int)op == (int)Message.WebSocketOpCode.Event) {
                                } else if ((int)op == (int)Message.WebSocketOpCode.RequestResponse) {
                                    responseMessage = message;
                                    needWaitSendSemaphore = true;
                                }
                            }
                        }
                    }
                } finally {
                    recvSemaphore.Release();
                    // Thread.Sleep(100);
                }

                if (needWaitSendSemaphore) {
                    sendSemaphore.Wait();
                    sendSemaphore.Release();
                }
            }
        }

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
                if (responseMessage != null) {
                    var responseData = JsonConvert.DeserializeObject<Message.RequestResponse<T>>(responseMessage);
                    responseMessage = null;
                    return responseData;
                }
            } finally {
                recvSemaphore.Release();
                sendSemaphore.Release();
            }
            return null;
        }
    }
}
