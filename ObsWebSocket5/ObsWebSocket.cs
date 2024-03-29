﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ObsWebSocket5.Message;
using ObsWebSocket5.Message.Data.Request;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Security.Cryptography;
using System.Text;

namespace ObsWebSocket5 {
    internal class ReceiveData {
        public WebSocketReceiveResult? Result { get; set; }
        public JObject? Data { get; set; }
    }

    public partial class ObsWebSocket {
        SemaphoreSlim recvSemaphore = new SemaphoreSlim(1, 1);
        Dictionary<string, JObject> receiveDataList = new Dictionary<string, JObject>();

        SemaphoreSlim sendSemaphore = new SemaphoreSlim(1, 1);

        ClientWebSocket? webSocket;
        string? password;
        EventSubscription eventSubscriptions = EventSubscription.None;

        public event EventHandler<Event.D>? EventReceived;
        public event EventHandler<WebSocketCloseCode?>? Closed;

        public bool IsConnected { get { return webSocket != null && webSocket.State ==WebSocketState.Open; } }

        private DateTime? lastSendTime;

        async public Task ConnectAsync(string uri, EventSubscription eventSubscriptions = EventSubscription.None) {
            try {
                webSocket = new ClientWebSocket();
                this.eventSubscriptions = eventSubscriptions;
                await webSocket.ConnectAsync(new Uri(uri), CancellationToken.None);
                await RecvHelloAsync();
#pragma warning disable CS4014 // この呼び出しは待機されなかったため、現在のメソッドの実行は呼び出しの完了を待たずに続行されます
                // ReceiveMessage();
                Task.Run(async () => await ReceiveMessage());
#pragma warning restore CS4014 // この呼び出しは待機されなかったため、現在のメソッドの実行は呼び出しの完了を待たずに続行されます
            } catch (Exception) {
                if (webSocket != null) {
                    webSocket.Dispose();
                    webSocket = null;
                }
                throw;
            }
        }

        async public Task ConnectAsync(string uri, string password, EventSubscription eventSubscriptions = EventSubscription.None) {
            this.password = password;
            await ConnectAsync(uri, eventSubscriptions);
        }

        async public Task CloseAsync() {
            if (webSocket != null) {
                if (webSocket.State == WebSocketState.Open) {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);
                    webSocket.Dispose();
                    webSocket = null;
                }
                return;
            } else {
                return;
            }
        }

        private T GetResponseOrThrow<T>(RequestResponse<T>? response) where T : ResponseData, new() {
            if (response?.d?.requestStatus.code == RequestStatus.Success) {
                if (response?.d?.responseData != null) {
                    return response.d.responseData;
                } else {
                    return new T();
                }
            } else {
                throw new ResponseException(response?.d?.requestStatus.code, response?.d?.requestStatus.comment);
            }
        }

        /** OBSからのメッセージを待つ */
        async private Task ReceiveMessage() {
            while (IsConnected) {
                var recvData = await ReceiveAsync();
                if (recvData?.Data != null) {
                    var op = recvData.Data["op"];
                    if (op != null) {
                        if ((int)op == (int)WebSocketOpCode.Event) {
                            var data = recvData.Data.ToObject<Event>();
                            if (data != null) {
                                var d = CreateEventData(data);
                                if (d != null) {
                                    EventReceived?.Invoke(this, d);
                                }
                            }
                        } else if ((int)op == (int)WebSocketOpCode.RequestResponse) {
                            var responseData = recvData.Data.ToObject<RequestResponse<JObject>>();
                            if (responseData != null) {
                                recvSemaphore.Wait();
                                receiveDataList.Add(responseData.d.requestId, recvData.Data);
                                recvSemaphore.Release();
                            }
                        }
                    }
                } else {
                    break;
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
                        WebSocketCloseCode? code = null;
                        if (data.Result.CloseStatus != null) {
                            code = (WebSocketCloseCode)data.Result.CloseStatus;
                        }
                        Closed?.Invoke(this, code);
                        throw new WebSocketClosedException(code, data.Result.CloseStatusDescription);
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
            await SendAsync(request);

            // レスポンスを取得できるまで待つ
            for (var i = 0; i < 100; i++) {
                try {
                    recvSemaphore.Wait();
                    JObject? recvData;
                    if (receiveDataList.TryGetValue(requestId, out recvData)) {
                        var response = recvData.ToObject<RequestResponse<T>>();
                        if (response?.d.requestId == requestId) {
                            receiveDataList.Remove(requestId);
                            return response;
                        }
                    }
                } finally {
                    recvSemaphore.Release();
                }
                Thread.Sleep(10);
            }
            return null;
        }

        /** OBSにメッセージを送信する */
        async ValueTask SendAsync<T>(T message) {
            if (webSocket != null) {
                sendSemaphore.Wait();

                var currentTime = DateTime.Now;
                if (lastSendTime != null) {
                    var waitTo = ((DateTime)lastSendTime).AddMilliseconds(200);
                    var distance = waitTo - currentTime;
                    if (distance.Milliseconds > 0) {
                        await Task.Delay(distance);
                    }
                }

                var jsonString = JsonConvert.SerializeObject(message, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                byte[] buffer = Encoding.UTF8.GetBytes(jsonString);
                var memory = new Memory<byte>(buffer);
                await webSocket.SendAsync(memory, WebSocketMessageType.Text, true, CancellationToken.None);
                lastSendTime = DateTime.Now;

                sendSemaphore.Release();
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
                identity.d.eventSubscriptions = (int)this.eventSubscriptions;
                await SendAsync(identity);
            }
        }

        /** Identified (OpCode 2) */
        async private Task RecvIdentifiedAsync() {
            if (webSocket != null) {
                var data = await ReceiveAsync();
            }
        }
    }
}
