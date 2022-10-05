using DeepL;
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
    public class DeepLTranslationService : TranslationService {
        public string Key { get; set; } = "";

        public DeepLTranslationService(ServiceManager serviceManager) : base(serviceManager, "DeepL", "DeepL", 400) {
            this.languages.Sort((a, b) => a.Name.CompareTo(b.Name));
        }

        public override UserControl? GetSettingPage()
        {
            return new DeepLTranslationPage(ServiceManager, this);
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
                MessageBox.Show("APIキーが設定されていません。", null, MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            } else {
                return true;
            }
        }

        private List<Language> languages = new List<Language> {
            new Language("bg", "Bulgarian"),
            new Language("cs", "Czech"),
            new Language("da", "Danish"),
            new Language("de", "German"),
            new Language("el", "Greek"),
            new Language("en-GB", "English (British)"),
            new Language("en-US", "English (American)"),
            new Language("es", "Spanish"),
            new Language("et", "Estonian"),
            new Language("fi", "Finnish"),
            new Language("fr", "French"),
            new Language("hu", "Hungarian"),
            new Language("id", "Indonesian"),
            new Language("it", "Italian"),
            new Language("ja", "Japanese"),
            new Language("lt", "Lithuanian"),
            new Language("lv", "Latvian"),
            new Language("nl", "Dutch"),
            new Language("pl", "Polish"),
            new Language("pt-BR", "Portuguese (Brazilian)"),
            new Language("pt-PT", "Portuguese (European)"),
            new Language("ro", "Romanian"),
            new Language("ru", "Russian"),
            new Language("sk", "Slovak"),
            new Language("sl", "Slovenian"),
            new Language("sv", "Swedish"),
            new Language("tr", "Turkish"),
            new Language("zh", "Chinese"),
        };
    }
}
