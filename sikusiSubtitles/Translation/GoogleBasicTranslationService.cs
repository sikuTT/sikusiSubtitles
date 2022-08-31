using Google.Apis.Services;
using Google.Apis.Translate.v2;
using Google.Apis.Translate.v2.Data;
using sikusiSubtitles.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.Translation {
    public class GoogleBasicTranslationService : TranslationService {
        private Tuple<string, string>[] langueages = new GoogleTranslationLanguages().Languages;

        public string? Key { get; set; }
        public string? From { get; set; }
        public string? To1 { get; set; }
        public string? To2 { get; set; }

        public GoogleBasicTranslationService(ServiceManager serviceManager) : base(serviceManager, "GoogleBasic", "Google Cloud Translation - Basic", 100) {
        }

        public override async void Translate(object obj, string text) {
            if (CheckParameters() == false) {
                return;
            }

            List<string> toList = new List<string>();
            if (To1 != null) toList.Add(To1);
            if (To2 != null) toList.Add(To2);
            var result = await TranslateAsync(obj, text, From, toList.ToArray());
            this.InvokeTranslated(result);
            return;
        }

        public override async void Translate(object obj, string text, string to) {
            if (CheckParameters() == false) {
                return;
            }

            var toList = new string[] { to };
            var result = await TranslateAsync(obj, text, null, toList);
            this.InvokeTranslated(result);
            return;
        }

        public async Task<TranslationResult> TranslateAsync(object obj, string text, string? from, string[] toList) {
            TranslationResult result = new TranslationResult(obj);
            if (toList.Length == 0)
                return result;

            try {
                var service = new TranslateService(new BaseClientService.Initializer() { ApiKey = this.Key });

                string[] srcText = new[] { text };
                foreach (var to in toList) {
                    TranslationsListResponse response = await service.Translations.List(srcText, to).ExecuteAsync();

                    // We need to change this code...
                    // currently this code
                    foreach (var translation in response.Translations) {
                        result.DetectLanguage = translation.DetectedSourceLanguage;
                        var translatedText = translation.TranslatedText.Replace("&#39;", "'").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&quot;", "\"").Replace("&amp;", "&");
                        result.Translations.Add(new TranslationResult.Translation() { Text = translatedText });
                        Debug.WriteLine("GoogleBasicTranslationService: " + translatedText);
                    }
                }
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message);
                result.Translations.Add(new TranslationResult.Translation() { Text = ex.Message });
                result.Error = true;
            }

            return result;
        }

        public override Tuple<string, string>[] GetLanguages() {
            return this.langueages;
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
