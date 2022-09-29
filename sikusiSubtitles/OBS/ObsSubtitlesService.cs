using Newtonsoft.Json.Linq;
using ObsWebSocket5.Message.Data.InputSettings;
using sikusiSubtitles.SpeechRecognition;
using sikusiSubtitles.Translation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.OBS {
    class SubtitlesText {
        public SubtitlesText(string text, bool recognized) {
            Text = text;
            Recognized = recognized;
        }
        public string Text { get; set; }
        public bool Recognized { get; set; }
    }

    public class TranslateTarget {
        public string Language { get; set; } = "";
        public string Target { get; set; } = "";
    }

    public class ObsSubtitlesService : sikusiSubtitles.Service {
        public string VoiceTarget { get; set; } = "";
        public List<TranslateTarget> TranslateTargetList { get; set; } = new List<TranslateTarget>();

        public ObsSubtitlesService(ServiceManager serviceManager) : base(serviceManager, ObsServiceManager.ServiceName, "ObsSubtitles", "字幕", 200) {
        }

        public override UserControl? GetSettingPage() {
            return new ObsSubtitlesPage(ServiceManager, this);
        }

        public override void Load(JToken token) {
        }

//        public override JObject Save() {

//        }

        public bool Start(ObsService obsService) {
            return false;
        }

        public void Stop() {
        }

        async public Task SetTextAsync(string sourceName, string text) {
        }
    }
}
