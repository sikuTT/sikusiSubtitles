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
            VoiceTarget = token.Value<string>("VoiceTarget") ?? "";
            var targetList = token.Value<JArray>("TranslateTargetList");
            if (targetList != null) {
                foreach (var target in targetList) {
                    TranslateTargetList.Add(new TranslateTarget() {
                        Language = target.Value<string>("Language") ?? "",
                        Target = target.Value<string>("Target") ?? "",
                    });
                }
            }
        }

        public override JObject Save() {
            return new JObject {
                new JProperty("VoiceTarget", VoiceTarget),
                new JProperty("TranslateTargetList", JArray.FromObject(TranslateTargetList)),
            };
        }

        public bool Start(ObsService obsService) {
            return false;
        }

        public void Stop() {
        }

        async public Task SetTextAsync(string sourceName, string text) {
        }
    }
}
