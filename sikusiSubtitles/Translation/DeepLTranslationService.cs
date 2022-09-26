using DeepL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.Translation {
    public class DeepLTranslationService : TranslationService {
        public string Key { get; set; } = "";

        public DeepLTranslationService(ServiceManager serviceManager) : base(serviceManager, "DeepL", "DeepL", 400) {
            SettingPage = new DeepLTranslationPage(serviceManager, this);
            this.languages.Sort((a, b) => a.Item2.CompareTo(b.Item2));
        }

        public override void Load(JToken token) {
            Key = Decrypt(token.Value<string>("Key") ?? "");
        }

        public override JObject Save() {
            return new JObject {
                new JProperty("Key", Encrypt(Key))
            };
        }

        public override List<Tuple<string, string>> GetLanguages() {
            return this.languages;
        }

        public override async Task<TranslationResult> TranslateAsync(string text, string? from, string to) {
            var result = await TranslateAsync(text, from, new string[] { to });
            return result;
        }

        public override async Task<TranslationResult> TranslateAsync(string text, string? from, string[] toList) {
            var result = new TranslationResult();

            try {
                if (CheckParameters() == false) return result;

                var translator = new Translator(this.Key);

                foreach (var to in toList) {
                    var translatedText = await translator.TranslateTextAsync(text, from, to);
                    Debug.WriteLine("DeepLTranslationService: " + translatedText);
                    result.Translations.Add(new TranslationResult.Translation() { Text = translatedText.Text, Language= to });
                }
            } catch (Exception ex) {
                Debug.WriteLine("DeepLTranslationService: " + ex.Message);
                result.Error = true;
                result.Translations.Add(new TranslationResult.Translation() { Text = ex.Message });
            }
            return result;
        }

        private bool CheckParameters() {
            if (this.Key == "") {
                MessageBox.Show("APIキーが設定されていません。", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            } else {
                return true;
            }
        }

        private List<Tuple<string, string>> languages = new List<Tuple<string, string>> {
            new Tuple<string, string>("bg", "Bulgarian"),
            new Tuple<string, string>("cs", "Czech"),
            new Tuple<string, string>("da", "Danish"),
            new Tuple<string, string>("de", "German"),
            new Tuple<string, string>("el", "Greek"),
            new Tuple<string, string>("en-GB", "English (British)"),
            new Tuple<string, string>("en-US", "English (American)"),
            new Tuple<string, string>("es", "Spanish"),
            new Tuple<string, string>("et", "Estonian"),
            new Tuple<string, string>("fi", "Finnish"),
            new Tuple<string, string>("fr", "French"),
            new Tuple<string, string>("hu", "Hungarian"),
            new Tuple<string, string>("id", "Indonesian"),
            new Tuple<string, string>("it", "Italian"),
            new Tuple<string, string>("ja", "Japanese"),
            new Tuple<string, string>("lt", "Lithuanian"),
            new Tuple<string, string>("lv", "Latvian"),
            new Tuple<string, string>("nl", "Dutch"),
            new Tuple<string, string>("pl", "Polish"),
            new Tuple<string, string>("pt-BR", "Portuguese (Brazilian)"),
            new Tuple<string, string>("pt-PT", "Portuguese (European)"),
            new Tuple<string, string>("ro", "Romanian"),
            new Tuple<string, string>("ru", "Russian"),
            new Tuple<string, string>("sk", "Slovak"),
            new Tuple<string, string>("sl", "Slovenian"),
            new Tuple<string, string>("sv", "Swedish"),
            new Tuple<string, string>("tr", "Turkish"),
            new Tuple<string, string>("zh", "Chinese"),
        };
    }
}
