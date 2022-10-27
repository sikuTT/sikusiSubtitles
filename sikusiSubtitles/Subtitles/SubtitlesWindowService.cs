using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace sikusiSubtitles.Subtitles {
    public class SubtitlesWindowService : Service {
        public string FontFamily = "";
        public string BackgroundColor { get; set; } = "#00FF00";
        public string FontColor { get; set; } = "#FFFFFF";
        public string StrokeColor{ get; set; } = "#000000";

        SubtitlesWindow? subtitlesWindow;
        public SubtitlesWindowService(ServiceManager serviceManager) : base(serviceManager, SubtitlesServiceManager.ServiceName, "SubtitlesWindow", "字幕ウィンドウ", 100) {
            var button = new ToggleButton {
                Content = "字幕",
            };
            button.Click += Button_Click;
            ServiceManager.AddTopFlowControl(button, 200);

            // 設定ページ
            settingsPage = new SubtitlesWindowPage(ServiceManager, this);
        }

        public override void Load(JToken token) {
            FontFamily = token.Value<string>("FontFamily") ?? FontFamily;
            BackgroundColor = token.Value<string>("BackgroundColor") ?? BackgroundColor;
            FontColor = token.Value<string>("FontColor") ?? FontColor;
            StrokeColor = token.Value<string>("StrokeColor") ?? StrokeColor;
        }

        public override JObject? Save() {
            return new JObject {
                new JProperty("FontFamily", FontFamily),
                new JProperty("BackgroundColor", BackgroundColor),
                new JProperty("FontColor", FontColor),
                new JProperty("StrokeColor", StrokeColor),
            };
        }

        public override void Finish() {
            if (subtitlesWindow != null) {
                subtitlesWindow.Close();
            }
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e) {
            subtitlesWindow = new SubtitlesWindow(ServiceManager, this);
            subtitlesWindow.Show();
        }
    }
}
