using Google.Apis.Services;
using Google.Apis.Translate.v2;
using Google.Apis.Translate.v2.Data;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace sikusiSubtitles.Translation {
    public class GoogleBasicTranslationService : TranslationService {
        private List<Language> languages = new GoogleTranslationLanguages().Languages;

        public string Key { get; set; } = "";

        public GoogleBasicTranslationService(ServiceManager serviceManager) : base(serviceManager, "GoogleBasicTranslation", "Google Cloud Translation", 200) {
            settingsPage = new GoogleBasicTranslationPage(ServiceManager, this);
        }

        public override void Load(JToken token) {
            Key = Decrypt(token.Value<string>("Key") ?? "");
        }

        public override JObject Save() {
            return new JObject {
                new JProperty("Key", Encrypt(Key))
            };
        }

        public override List<Language> GetLanguages() {
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

                var service = new TranslateService(new BaseClientService.Initializer() { ApiKey = this.Key });

                string[] srcText = new[] { text };
                foreach (var to in toList) {
                    TranslationsListResponse response = await service.Translations.List(srcText, to).ExecuteAsync();

                    // We need to change this code...
                    // currently this code
                    foreach (var translation in response.Translations) {
                        result.DetectLanguage = translation.DetectedSourceLanguage;
                        var translatedText = translation.TranslatedText.Replace("&#39;", "'").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&quot;", "\"").Replace("&amp;", "&");
                        result.Translations.Add(new TranslationResult.Translation() { Text = translatedText, Language = to });
                        Debug.WriteLine("GoogleBasicTranslationService: " + translatedText);
                    }
                }
            } catch (Exception ex) {
                Debug.WriteLine("GoogleAppsScriptTranslationService: " + ex.Message);
                result.Error = true;
                result.Translations.Add(new TranslationResult.Translation() { Text = ex.Message });
            }
            return result;
        }

        private bool CheckParameters() {
            if (this.Key == null || this.Key == "") {
                MessageBox.Show("APIキーが設定されていません。", null, MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            } else {
                return true;
            }
        }
    }
}
