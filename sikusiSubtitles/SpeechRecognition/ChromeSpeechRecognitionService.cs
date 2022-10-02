using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace sikusiSubtitles.SpeechRecognition {
    public class ChromeSpeechRecognitionService : SpeechRecognitionService {
                public int HttpServerPort { get; set; } = 14949;
        public int WebSocketPort { get; set; } = 14950;

        ChromeSpeechRecognitionWebServer? webServer;
        ChromeSpeechRecognitionWebSocketServer? webSocketServer;

        public ChromeSpeechRecognitionService(ServiceManager serviceManager) : base(serviceManager, "ChromeSpeechRecognition", "Google Chrome", 100) {
        }

        public override UserControl? GetSettingPage()
        {
            return new ChromeSpeechRecognitionPage(ServiceManager, this);
        }
        public override void Load(JToken token) {
            HttpServerPort = token.Value<int?>("HttpServerPort") ?? HttpServerPort;
            WebSocketPort = token.Value<int?>("WebSocketPort") ?? WebSocketPort;
        }

        public override JObject Save() {
            return new JObject {
                new JProperty("HttpServerPort", HttpServerPort),
                new JProperty("WebSocketPort", WebSocketPort),
            };
        }

        public override List<Tuple<string, string>> GetLanguages() {
            return this.Languages;
        }

        public override bool Start() {
            var manager = this.ServiceManager.GetManager<SpeechRecognitionServiceManager>();
            if (manager == null || manager.Language == "") {
                MessageBox.Show("言語を選択してください。", null, MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            webServer = new ChromeSpeechRecognitionWebServer(HttpServerPort);
            webServer.Start();

            webSocketServer = new ChromeSpeechRecognitionWebSocketServer(WebSocketPort);
            webSocketServer.Recognizing += RecognizingHandler;
            webSocketServer.Recognized += RecognizedHandler;
            webSocketServer.Closed += ClosedHandler;
            webSocketServer.Start();

            var uri = $"http://127.0.0.1:{HttpServerPort}/";
            if (this.LunchChrome(uri, manager.Language) == false) {
                Stop();
                return false;
            }

            return true;
        }

        public override void Stop() {
            try {
                Listener = new HttpListener();
                Listener.Prefixes.Add(uri);
                Listener.Start();
                IAsyncResult result = Listener.BeginGetContext(new AsyncCallback(ListenerCallback), Listener);
            } catch (HttpListenerException ex) {
                Debug.WriteLine(ex.Message);
                MessageBox.Show("WEBサーバーを開始できませんでした。", null, MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            } catch (Exception ex) {
                Debug.WriteLine("ChromeSpeechRecognitionService.Stop: " + ex.Message);
            }

            try {
                if (webSocketServer != null) {
                    webSocketServer.Recognizing -= RecognizingHandler;
                    webSocketServer.Recognized -= RecognizedHandler;
                    webSocketServer.Closed -= ClosedHandler;
                    webSocketServer.Stop();
                    webSocketServer = null;
                }
            } catch (Exception ex) {
                Debug.WriteLine("ChromeSpeechRecognitionService.Stop: " + ex.Message);
            }
        }


        private bool LunchChrome(string uri, string language) {
            try {   
                uri += $"browser-recognition/index.html?lang={language}&port={WebSocketPort}";
                // var chromePath = Microsoft.Win32.Registry.GetValue(@"HKEY_CLASSES_ROOT\ChromeHTML\shell\open\command", null, null) as string;
                var chromePath = Microsoft.Win32.Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\chrome.exe", null, null) as string;
                
                if (chromePath != null) {
                    // chromePath = chromePath.Replace("%1", $"\"--app={uri}\"");

                    ProcessStartInfo pi = new ProcessStartInfo() {
                        FileName = chromePath,
                        Arguments = $"--app={uri}",
                        UseShellExecute = true,
                    };
                    System.Diagnostics.Process.Start(pi);
                }
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        private void RecognizingHandler(object? sender, SpeechRecognitionEventArgs args) {
            this.InvokeRecognizing(args.Text);
        }

        private void RecognizedHandler(object? sender, SpeechRecognitionEventArgs args) {
            this.InvokeRecognized(args.Text);
        }

        private void ClosedHandler(object? sender, bool args) {
            InvokeServiceStopped(args);
        }

        List<Tuple<string, string>> Languages = new List<Tuple<string, string>>() {
            new Tuple<string, string>("ja-JP", "日本語"),
            new Tuple<string, string>("en-US", "英語"),
            new Tuple<string, string>("es-ES", "スペイン語（スペイン）"),
            new Tuple<string, string>("pt-P", "ポルトガル語（ポルトガル）"),
            new Tuple<string, string>("pt-BR", "ポルトガル語（ブラジル）"),
            new Tuple<string, string>("ko-KR", "韓国語"),
            new Tuple<string, string>("zh", "中国語（簡体字、中国本土）"),
            new Tuple<string, string>("cmn-Hant-TW", "中国語（繁体字、台湾）"),
        };
    }
}
