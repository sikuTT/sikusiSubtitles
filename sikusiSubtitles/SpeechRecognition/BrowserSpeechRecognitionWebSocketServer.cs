using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace sikusiSubtitles.SpeechRecognition {
    public class BrowserSpeechRecognitionWebSocketServer {
        HttpListener? Server;

        public event EventHandler<SpeechRecognitionEventArgs>? Recognizing;
        public event EventHandler<SpeechRecognitionEventArgs>? Recognized;
        public event EventHandler<bool>? Closed;

        public BrowserSpeechRecognitionWebSocketServer(int port) {
            Server = new HttpListener();
            Server.Prefixes.Add($"http://127.0.0.1:{port}/");
        }

        public bool Start() {
            if (Server != null) {
                try {
                    Server.Start();
                } catch (Exception) {
                    MessageBox.Show("WebSocketサーバーを開始できませんでした。", null, MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                return true;
            }
            return false;
        }

        public async void Listen() {
            while (Server != null) {
                try {
                    var hc = await Server.GetContextAsync();
                    if (!hc.Request.IsWebSocketRequest) {
                        hc.Response.StatusCode = 400;
                        hc.Response.Close();
                        continue;
                    }

                    Task task = Task.Run(async () => {
                        var wsc = await hc.AcceptWebSocketAsync(null);
                        var webSocket = wsc.WebSocket;
                        await Receive(webSocket);
                    });
                } catch (Exception ex) {
                    Debug.WriteLine("BrowserSpeechRecognitionWebSocketServer.Start: " + ex.Message);
                    break;
                }
            }
        }

        public void Stop() {
            if (Server != null) {
                Server.Close();
                Server = null;
            }
        }

        async Task Receive(WebSocket webSocket) {
            while (webSocket.State == WebSocketState.Open) {
                try {
                    var buffer = new List<byte>();
                    WebSocketReceiveResult? result;
                    do {
                        var temp = new byte[1024];
                        var segment = new ArraySegment<byte>(temp);
                        result = await webSocket.ReceiveAsync(segment, CancellationToken.None);

                        switch (result.MessageType) {
                            case WebSocketMessageType.Close:
                                Closed?.Invoke(this, true);
                                return;
                            case WebSocketMessageType.Binary:
                                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Binary not supported", CancellationToken.None);
                                return;
                            default:
                                break;
                        }

                        var array = new byte[result.Count];
                        Array.Copy(temp, array, result.Count);
                        buffer.AddRange(array);
                    } while (!result.EndOfMessage);

                    var str = Encoding.UTF8.GetString(buffer.ToArray()).ToString();
                    var obj = JsonConvert.DeserializeObject<SpeechRecognitionEventArgs>(str);
                    if (obj != null) {
                        if (obj.Recognized) {
                            this.Recognized?.Invoke(this, obj);
                        } else {
                            this.Recognizing?.Invoke(this, obj);
                        }
                    }

                } catch (Exception ex) {
                    Debug.WriteLine("BrowserSpeechRecognitionWebSocketServer.Receive: " + ex.Message);
                }
            }
        }
    }
}
