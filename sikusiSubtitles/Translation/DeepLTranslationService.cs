using DeepL;
using sikusiSubtitles.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.Translation {
    public class DeepLTranslationService : TranslationService {
        public string? Key { get; set; }
        public string? From { get; set; }
        public string? To1 { get; set; }
        public string? To2 { get; set; }

        public DeepLTranslationService(ServiceManager serviceManager) : base(serviceManager, "DeepL", "DeepL", 400) {
            this.languages.Sort((a, b) => a.Item2.CompareTo(b.Item2));
        }

        public override async void Translate(object obj, string text) {
            if (CheckParameters() == false) {
                return;
            }

            var toList = new List<string>();
            if (To1 != null) toList.Add(To1);
            if (To2 != null) toList.Add(To2);
            var result = await TranslateAsync(obj, text, From, toList.ToArray());
            this.InvokeTranslated(result);
        }

        public override async void Translate(object obj, string text, string to) {
            if (CheckParameters() == false) {
                return;
            }

            var toList = new string[] { to };
            var result = await TranslateAsync(obj, text, null, toList);
            this.InvokeTranslated(result);
        }

        public override List<Tuple<string, string>> GetLanguages() {
            return this.languages;
        }

        private async Task<TranslationResult> TranslateAsync(object obj, string text, string? from, string[] toList) {
            var result = new TranslationResult(obj);

            if (this.Key != null) {
                var translator = new Translator(this.Key);

                foreach (var to in toList) {
                    var translatedText = await translator.TranslateTextAsync(text, from, to);
                    Debug.WriteLine("DeepLTranslationService: " + translatedText);
                    result.Translations.Add(new TranslationResult.Translation() { Text = translatedText.Text });
                }
            }
            return result;
        }

        private bool CheckParameters() {
            if (this.Key == null || this.Key == "") {
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
            new Tuple<string, string>("en", "English"),
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
            new Tuple<string, string>("pt", "Portuguese"),
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
