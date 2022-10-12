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

        public override List<Language> GetLanguages() {
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
                if (webServer != null) {
                    webServer.Stop();
                    webServer = null;
                }
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

        List<Language> Languages = new List<Language>() {
            // new Language("ar-BH", "Arabic (Bahrain) modern standard"),
            // new Language("ar-EG", "Arabic (Egypt)"),
            // new Language("ar-KW", "Arabic (Kuwait)"),
            // new Language("ar-QA", "Arabic (Qatar)"),
            // new Language("ar-SA", "Arabic (Saudi Arabia)"),
            // new Language("ar-SY", "Arabic (Syria)"),
            // new Language("ar-AE", "Arabic (UAE)"),
            // new Language("ca-ES", "Catalan"),
            // new Language("zh-HK", "Chinese (Cantonese Traditional)"),
            new Language("zh-CN", "中国語(簡体字)"),
            new Language("cmn-Hant-TW", "中国語(繁体字)"),
            // new Language("da-DK", "Danish (Denmark)"),
            // new Language("nl-NL", "Dutch (Netherlands)"),
            // new Language("en-AU", "English (Australia)"),
            // new Language("en-CA", "English (Canada)"),
            // new Language("en-IN", "English (India)"),
            // new Language("en-NZ", "English (New Zealand)"),
            // new Language("en-GB", "English (United Kingdom)"),
            new Language("en-US", "英語（アメリカ）"),
            // new Language("fi-FI", "Finnish (Finland)"),
            // new Language("fr-CA", "French (Canada)"),
            new Language("fr-FR", "フランス語（フランス）"),
            // new Language("de-DE", "German (Germany)"),
            // new Language("gu-IN", "Gujarati (Indian)"),
            // new Language("hi-IN", "Hindi (India)"),
            new Language("it-IT", "イタリア語（イタリア）"),
            new Language("ja-JP", "日本語"),
            new Language("ko-KR", "韓国語"),
            // new Language("mr-IN", "Marathi (India)"),
            // new Language("nb-NO", "Norwegian (Bokmål) (Norway)"),
            // new Language("pl-PL", "Polish (Poland)"),
            new Language("pt-BR", "ポルトガル語（ブラジル）"),
            new Language("pt-PT", "ポルトガル語（ポルトガル）"),
            new Language("ru-RU", "ロシア語（ロシア）"),
            // new Language("es-MX", "Spanish (Mexico)"),
            new Language("es-ES", "スペイン語（スペイン）"),
            // new Language("sv-SE", "Swedish (Sweden)"),
            // new Language("ta-IN", "Tamil (India)"),
            // new Language("te-IN", "Telugu (India)"),
            new Language("th-TH", "タイ語（タイ）"),
            // new Language("tr-TR", "Turkish (Turkey)"),
        };
    }
}
