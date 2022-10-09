﻿using Newtonsoft.Json.Linq;
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
            new Language("ja-JP", "日本語"),
            new Language("en-US", "英語"),
            new Language("es-ES", "スペイン語（スペイン）"),
            new Language("pt-P", "ポルトガル語（ポルトガル）"),
            new Language("pt-BR", "ポルトガル語（ブラジル）"),
            new Language("ko-KR", "韓国語"),
            new Language("zh", "中国語（簡体字、中国本土）"),
            new Language("cmn-Hant-TW", "中国語（繁体字、台湾）"),
        };
    }
}
