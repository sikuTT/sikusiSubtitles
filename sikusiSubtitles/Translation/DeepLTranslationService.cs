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

        public override void Init() {
            settingsPage = new DeepLTranslationPage(ServiceManager, this);
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
            new Language("bg", "ブルガリア語"),
            new Language("cs", "チェコ語"),
            new Language("da", "デンマーク語"),
            new Language("de", "ドイツ語"),
            new Language("el", "ギリシャ語"),
            new Language("en-GB", "英語（イギリス）"),
            new Language("en-US", "英語（アメリカ）"),
            new Language("es", "スペイン語"),
            new Language("et", "エストニア語"),
            new Language("fi", "フィンランド語"),
            new Language("fr", "フランス語"),
            new Language("hu", "ハンガリー語"),
            new Language("id", "インドネシア語"),
            new Language("it", "イタリア語"),
            new Language("ja", "日本語"),
            new Language("lt", "リトアニア語"),
            new Language("lv", "ラトビア語"),
            new Language("nl", "オランダ語"),
            new Language("pl", "ポーランド語"),
            new Language("pt-BR", "ポルトガル語（ブラジル）"),
            new Language("pt-PT", "ポルトガル語（ポルトガル）"),
            new Language("ro", "ルーマニア語"),
            new Language("ru", "ロシア語"),
            new Language("sk", "スロバキア語"),
            new Language("sl", "スロベニア語"),
            new Language("sv", "スウェーデン語"),
            new Language("tr", "トルコ語"),
            new Language("uk", "ウクライナ語"),
            new Language("zh", "中国語（簡体字）"),
        };
    }
}
