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
    public class BrowserSpeechRecognitionPageService : Service {
        public int HttpServerPort { get; set; } = 14949;
        public int WebSocketPort { get; set; } = 14950;

        public BrowserSpeechRecognitionPageService(ServiceManager serviceManager) : base(serviceManager, SpeechRecognitionServiceManager.ServiceName, "BrowserSpeechRecognition", "ブラウザー", 100) {
            settingsPage = new BrowserSpeechRecognitionPage(ServiceManager, this);
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
    }
}
