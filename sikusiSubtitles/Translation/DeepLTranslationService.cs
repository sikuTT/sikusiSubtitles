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
        }

        public override async void Translate(string text) {
            if (CheckParameters() == false) {
                return;
            }

            var toList = new List<string>();
            if (To1 != null) toList.Add(To1);
            if (To2 != null) toList.Add(To2);
            var result = await TranslateAsync(text, From, toList.ToArray());
            this.InvokeTranslated(result);
        }

        public override async void Translate(string text, string to) {
            if (CheckParameters() == false) {
                return;
            }

            var toList = new string[] { to };
            var result = await TranslateAsync(text, null, toList);
            this.InvokeTranslated(result);
        }

        private async Task<TranslationResult> TranslateAsync(string text, string? from, string[] toList) {
            var result = new TranslationResult();

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
    }
}
