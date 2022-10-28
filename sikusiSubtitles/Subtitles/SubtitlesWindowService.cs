using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace sikusiSubtitles.Subtitles {
    public class SubtitlesWindowService : Service {
        public string BackgroundColor { get; set; } = "#00FF00";
        public string FontFamily { get; set; } = SystemFonts.MessageFontFamily.Source;
        public int FontSize { get; set; } = 12;
        public bool Bold { get; set; } = false;
        public bool Italic { get; set; } = false;
        public string FontColor { get; set; } = "#FFFFFF";
        public bool Stroke { get; set; } = true;
        public int StrokeWidth { get; set; } = 2;
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
            BackgroundColor = token.Value<string>("BackgroundColor") ?? BackgroundColor;
            FontFamily = token.Value<string>("FontFamily") ?? FontFamily;
            FontSize = token.Value<int?>("FontSize") ?? FontSize;
            Bold = token.Value<bool?>("Bold") ?? Bold;
            Italic = token.Value<bool?>("Italic") ?? Italic;
            FontColor = token.Value<string>("FontColor") ?? FontColor;
            Stroke = token.Value<bool?>("Stroke") ?? Stroke;
            StrokeWidth = token.Value<int?>("StrokeWidth") ?? StrokeWidth;
            StrokeColor = token.Value<string>("StrokeColor") ?? StrokeColor;
        }

        public override JObject? Save() {
            return new JObject {
                new JProperty("BackgroundColor", BackgroundColor),
                new JProperty("FontFamily", FontFamily),
                new JProperty("FontSize", FontSize),
                new JProperty("Bold", Bold),
                new JProperty("Italic", Italic),
                new JProperty("FontColor", FontColor),
                new JProperty("Stroke", Stroke),
                new JProperty("StrokeWidth", StrokeWidth),
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
