using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace sikusiSubtitles.Translation {
    public class AzureTranslationService : TranslationService {
        public string Key { get; set; } = "";
        public string Region { get; set; } = "";

        private static readonly string endpoint = "https://api.cognitive.microsofttranslator.com/";
        private HttpClient HttpClient = new HttpClient();

        public AzureTranslationService(ServiceManager serviceManager) : base(serviceManager, "AzureTranslation", "Azure Cognitive Services - Translator", 300) {
            this.languages.Sort((a, b) => a.Name.CompareTo(b.Name));
        }

        public override UserControl? GetSettingPage()
        {
            return new AzureTranslationPage(ServiceManager, this);
        }

        public override void Load(JToken token) {
            Key = Decrypt(token.Value<string>("Key") ?? "");
            Region = token.Value<string>("Region") ?? "";
        }

        public override JObject Save() {
            return new JObject {
                new JProperty("Key", Encrypt(Key)),
                new JProperty("Region", Region)
            };
        }

        public override List<Language> GetLanguages() {
            return this.languages;
        }

        public async override Task<TranslationResult> TranslateAsync(string text, string? from, string to) {
            var result = await TranslateAsync(text, from, new string[] { to });
            return result;
        }

        public async override Task<TranslationResult> TranslateAsync(string text, string? from, string[] toList) {
            var result = new TranslationResult();

            try {
                if (CheckParameters() == false) return result;
                if (toList.Length == 0) return result;

                string route = String.Format("translate?api-version=3.0");
                if (from != null) {
                    route = route + "&from=" + from;
                }
                foreach (var to in toList) {
                    route = route + "&to=" + to;
                }

                object[] body = new object[] { new { Text = text } };
                var requestBody = JsonConvert.SerializeObject(body);
                var uri = new Uri(endpoint + route);

                using var request = new HttpRequestMessage();

                // Build the request.
                request.Method = HttpMethod.Post;
                request.RequestUri = uri;
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                request.Headers.Add("Ocp-Apim-Subscription-Key", this.Key);
                request.Headers.Add("Ocp-Apim-Subscription-Region", this.Region);

                // Send the request and get response.
                HttpResponseMessage response = await this.HttpClient.SendAsync(request).ConfigureAwait(false);

                // Read response.
                string responseText = await response.Content.ReadAsStringAsync();
                AzureTranslationResult[]? azureResults = JsonConvert.DeserializeObject<AzureTranslationResult[]>(responseText);

                if (azureResults == null) return result;
                foreach (var azureResult in azureResults) {
                    result.DetectLanguage = azureResult.detectedLanguage?.language;

                    if (azureResult.translations == null)
                        continue;

                    foreach (var translation in azureResult.translations) {
                        Debug.WriteLine("AzureTranslationService: " + translation.text);
                        var t = new TranslationResult.Translation() {
                            Text = translation.text ?? "",
                            Language = translation.to
                        };
                        result.Translations.Add(t);
                    }
                }
            } catch (Exception ex) {
                Debug.WriteLine("AzureTranslationService: " + ex.Message);
                result.Error = true;
                result.Translations.Add(new TranslationResult.Translation() { Text = ex.Message });
            }

            return result;
        }

        private bool CheckParameters() {
            if (this.Key == null || this.Key == "") {
                MessageBox.Show("APIキーが設定されていません。", null, MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            } else if (this.Region == null || this.Region == "") {
                MessageBox.Show("地域が設定されていません。", null, MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            } else {
                return true;
            }
        }

        // 対応している言語のリスト
        private List<Language> languages = new List<Language> {
            new Language("af", "アフリカーンス語"),
            new Language("am", "アムハラ語"),
            new Language("ar", "アラビア語"),
            new Language("as", "アッサム語"),
            new Language("az", "アゼルバイジャン語"),
            new Language("ba", "バシキール語"),
            new Language("bg", "ブルガリア語"),
            new Language("bn", "ベンガル語"),
            new Language("bo", "チベット語"),
            new Language("bs", "ボスニア語"),
            new Language("ca", "カタロニア語"),
            new Language("cs", "チェコ語"),
            new Language("cy", "ウェールズ語"),
            new Language("da", "デンマーク語"),
            new Language("de", "ドイツ語"),
            new Language("dv", "ディベヒ語"),
            new Language("el", "ギリシャ語"),
            new Language("en", "英語"),
            new Language("es", "スペイン語"),
            new Language("et", "エストニア語"),
            new Language("eu", "バスク語"),
            new Language("fa", "ペルシア語"),
            new Language("fi", "フィンランド語"),
            new Language("fil", "フィリピノ語"),
            new Language("fj", "フィジー語"),
            new Language("fo", "フェロー語"),
            new Language("fr", "フランス語"),
            new Language("fr-CA", "フランス語（カナダ）"),
            new Language("ga", "アイルランド語"),
            new Language("gl", "ガリシア語"),
            new Language("gu", "グジャラート語"),
            new Language("he", "ヘブライ語"),
            new Language("hi", "ヒンディー語"),
            new Language("hr", "クロアチア語"),
            new Language("hsb", "高地ソルブ語"),
            new Language("ht", "クレオール語（ハイチ）"),
            new Language("hu", "ハンガリー語"),
            new Language("hy", "アルメニア語"),
            new Language("id", "インドネシア語"),
            new Language("ikt", "Inuinnaqtun"),
            new Language("is", "アイスランド語"),
            new Language("it", "イタリア語"),
            new Language("iu", "イヌクティトット語"),
            new Language("iu-Latn", "イヌクティトット語（ラテン文字）"),
            new Language("ja", "日本語"),
            new Language("ka", "ジョージア語"),
            new Language("kk", "カザフ語"),
            new Language("km", "クメール語"),
            new Language("kmr", "クルド語 (北)"),
            new Language("kn", "カンナダ語"),
            new Language("ko", "韓国語"),
            new Language("ku", "クルド語 (中央)"),
            new Language("ky", "キルギス語"),
            new Language("lo", "ラオ語"),
            new Language("lt", "リトアニア語"),
            new Language("lv", "ラトビア語"),
            new Language("lzh", "Chinese (Literary)"),
            new Language("mg", "マダガスカル語"),
            new Language("mi", "マオリ語"),
            new Language("mk", "マケドニア語"),
            new Language("ml", "マラヤーラム語"),
            new Language("mn-Cyrl", "モンゴル語（キリル文字）"),
            new Language("mn-Mong", "モンゴル語（モンゴル文字）"),
            new Language("mr", "マラーティー語"),
            new Language("ms", "マレー語"),
            new Language("mt", "マルタ語"),
            new Language("mww", "フモン語"),
            new Language("my", "ミャンマー語"),
            new Language("nb", "ノルウェー語(ブークモール)"),
            new Language("ne", "ネパール語"),
            new Language("nl", "オランダ語"),
            new Language("or", "オディア語"),
            new Language("otq", "ケレタロ オトミ語"),
            new Language("pa", "パンジャブ語"),
            new Language("pl", "ポーランド語"),
            new Language("prs", "ダリー語"),
            new Language("ps", "パシュトー語"),
            new Language("pt", "ポルトガル語 (ブラジル)"),
            new Language("pt-PT", "ポルトガル語 (ポルトガル)"),
            new Language("ro", "ルーマニア語"),
            new Language("ru", "ロシア語"),
            new Language("sk", "スロバキア語"),
            new Language("sl", "スロベニア語"),
            new Language("sm", "サモア語"),
            new Language("so", "ソマリ語"),
            new Language("sq", "アルバニア語"),
            new Language("sr-Cyrl", "セルビア語 (キリル文字)"),
            new Language("sr-Latn", "セルビア語 (ラテン文字)"),
            new Language("sv", "スウェーデン語"),
            new Language("sw", "スワヒリ語"),
            new Language("ta", "タミル語"),
            new Language("te", "テルグ語"),
            new Language("th", "タイ語"),
            new Language("ti", "ティグリニア語"),
            new Language("tk", "トルクメン語"),
            new Language("tlh-Latn", "クリンゴン語 (ラテン文字)"),
            new Language("tlh-Piqd", "クリンゴン語 (pIqaD)"),
            new Language("to", "トンガ語"),
            new Language("tr", "トルコ語"),
            new Language("tt", "タタール語"),
            new Language("ty", "タヒチ語"),
            new Language("ug", "ウイグル語"),
            new Language("uk", "ウクライナ語"),
            new Language("ur", "ウルドゥー語"),
            new Language("uz", "ウズベク語"),
            new Language("vi", "ベトナム語"),
            new Language("yua", "ユカテコ語"),
            new Language("yue", "広東語 (繁体字)"),
            new Language("zu", "ズールー語"),
            new Language("zh-Hans", "中国語 (簡体字)"),
            new Language("zh-Hant", "中国語 (繁体字)"),
        };
    }

    public class AzureTranslationResult {
        public Language? detectedLanguage { get; set; }
        public List<Translation>? translations { get; set; }

        public class Translation {
            public string? text { get; set; }
            public string? to { get; set; }
        }

        public class Language {
            public string? language { get; set; }
            public double? score { get; set; }
        }
    }
}
