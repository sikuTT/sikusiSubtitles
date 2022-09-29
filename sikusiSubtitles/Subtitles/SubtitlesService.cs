using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.Subtitles {
    public class SubtitlesService : Service {
        public string TranslationEngine { get; set; } = "";
        public string TranslationLanguageFrom { get; set; } = "";
        public string VoiceTarget { get; set; } = "";
        public List<string> TranslationLanguageToList { get; set; } = new List<string>();
        public bool ClearInterval { get; set; } = false;
        public int ClearIntervalTime { get; set; } = 1;
        public bool AdditionalClear { get; set; } = false;
        public int AdditionalClearTime { get; set; } = 1;

        public SubtitlesService(ServiceManager serviceManager) : base(serviceManager, SubtitlesServiceManager.ServiceName, "Subtitles", "字幕", 100) {
        }

        public override UserControl? GetSettingPage() {
            return new SubtitlesPage(ServiceManager, this);
        }

        public override void Load(JToken token) {
            TranslationEngine = token.Value<string>("TranslationEngine") ?? "";
            TranslationLanguageFrom = token.Value<string>("TranslationLanguageFrom") ?? "";
            VoiceTarget = token.Value<string>("VoiceTarget") ?? "";
            var toList = token.Value<JArray>("TranslationLanguageToList");
            if (toList != null) {
                foreach (var to in toList) {
                    TranslationLanguageToList.Add(to.ToString());
                }
            }
            ClearInterval = token.Value<bool>("ClearInterval");
            ClearIntervalTime = token.Value<int?>("ClearIntervalTime") ?? 1;
            AdditionalClear = token.Value<bool>("AdditionalClear");
            AdditionalClearTime = token.Value<int?>("AdditionalClearTime") ?? 1;
        }

        public override JObject? Save() {
            // 空文字の翻訳先は削除する
            TranslationLanguageToList.RemoveAll(to => to == "");

            return new JObject {
                new JProperty("TranslationEngine", TranslationEngine),
                new JProperty("TranslationLanguageFrom", TranslationLanguageFrom),
                new JProperty("VoiceTarget", VoiceTarget),
                new JProperty("TranslationLanguageToList", TranslationLanguageToList),
                new JProperty("ClearInterval", ClearInterval),
                new JProperty("ClearIntervalTime", ClearIntervalTime),
                new JProperty("AdditionalClear", AdditionalClear),
                new JProperty("AdditionalClearTime", AdditionalClearTime),
            };
        }
    }
}
