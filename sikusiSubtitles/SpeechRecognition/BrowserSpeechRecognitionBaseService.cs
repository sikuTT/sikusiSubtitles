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
    public abstract class BrowserSpeechRecognitionBaseService : SpeechRecognitionService {
        public enum BrowserEnum {
            Edge, Chrome, Firefox,
        }

        BrowserSpeechRecognitionWebServer? webServer;
        BrowserSpeechRecognitionWebSocketServer? webSocketServer;

        public BrowserSpeechRecognitionBaseService(ServiceManager serviceManager, string name, string displayName, int index) : base(serviceManager, name, displayName, index) { }

        public override List<Language> GetLanguages() {
            return this.Languages;
        }

        public override bool Start() {
            var manager = this.ServiceManager.GetManager<SpeechRecognitionServiceManager>();
            if (manager == null || manager.Language == "") {
                MessageBox.Show("言語を選択してください。", null, MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            var browserPageService = this.ServiceManager.GetService<BrowserSpeechRecognitionPageService>();
            if (browserPageService == null) {
                MessageBox.Show("設定が取得できません。", null, MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            webServer = new BrowserSpeechRecognitionWebServer(browserPageService.HttpServerPort);
            webServer.Start();

            webSocketServer = new BrowserSpeechRecognitionWebSocketServer(browserPageService.WebSocketPort);
            webSocketServer.Recognizing += RecognizingHandler;
            webSocketServer.Recognized += RecognizedHandler;
            webSocketServer.Closed += ClosedHandler;
            webSocketServer.Start();

            var uri = $"http://127.0.0.1:{browserPageService.HttpServerPort}/";
            if (this.LunchBrowser(uri, manager.Language) == false) {
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
                Debug.WriteLine("BrowserSpeechRecognitionBaseService.Stop: " + ex.Message);
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
                Debug.WriteLine("BrowserSpeechRecognitionBaseService.Stop: " + ex.Message);
            }
        }


        protected abstract string? GetBrowserPath();
        protected abstract string GetBrowserArgs(string uri);

        protected bool LunchBrowser(string uri, string language) {
            try {
                var browserPageService = this.ServiceManager.GetService<BrowserSpeechRecognitionPageService>();
                if (browserPageService == null) {
                    MessageBox.Show("設定が取得できません。", null, MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                uri += $"browser-recognition/index.html?lang={language}&port={browserPageService.WebSocketPort}";
                var path = GetBrowserPath();
                var args = GetBrowserArgs(uri);

                if (path != null) {
                    ProcessStartInfo pi = new ProcessStartInfo() {
                        FileName = path,
                        Arguments = args,
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
