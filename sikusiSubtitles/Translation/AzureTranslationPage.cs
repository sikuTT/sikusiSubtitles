using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sikusiSubtitles.Translation {
    public partial class AzureTranslationPage : SettingPage {
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

        private static readonly string endpoint = "https://api.cognitive.microsofttranslator.com/";


        HttpClient HttpClient = new HttpClient();

        public bool IsTo1 { get { return this.to1CheckBox.Checked; } }
        public bool IsTo2 { get { return this.to2CheckBox.Checked; } }

        public string? From { get { return this.fromComboBox.SelectedIndex >= 0 ? this.Languages[this.fromComboBox.SelectedIndex].Item1 : null; } }

        /**
         * 翻訳先の言語一覧
         */
        public List<string> To {
            get {
                var to = new List<String>();
                if (this.to1CheckBox.Checked && this.to1ComboBox.SelectedIndex != -1) {
                    to.Add(this.Languages[this.to1ComboBox.SelectedIndex].Item1);
                }
                if (this.to2CheckBox.Checked && this.to2ComboBox.SelectedIndex != -1) {
                    to.Add(this.Languages[this.to2ComboBox.SelectedIndex].Item1);
                }
                return to;
            }
        }

        /**
         * 設定の読み込み
         */
        public override void LoadSettings() {
            this.keyTextBox.Text = this.Decrypt(Properties.Settings.Default.AzureTranslationKey);
            this.regionTextBox.Text = Properties.Settings.Default.AzureTranslationRegion;
            this.to1CheckBox.Checked = Properties.Settings.Default.AzureTranslationTo1Run;
            this.to2CheckBox.Checked = Properties.Settings.Default.AzureTranslationTo2Run;

            for (var i = 0; i < this.Languages.Length; i++) {
                if (this.Languages[i].Item1 == Properties.Settings.Default.AzureTranslationFrom) {
                    this.fromComboBox.SelectedIndex = i;
                } else if (this.Languages[i].Item1 == Properties.Settings.Default.AzureTranslationTo1) {
                    this.to1ComboBox.SelectedIndex = i;
                } else if (this.Languages[i].Item1 == Properties.Settings.Default.AzureTranslationTo2) {
                    this.to2ComboBox.SelectedIndex = i;
                }
            }
        }

        /**
         * 設定の保存
         */
        public override void SaveSettings() {
            Properties.Settings.Default.AzureTranslationKey = this.Encrypt(this.keyTextBox.Text);
            Properties.Settings.Default.AzureTranslationRegion = this.regionTextBox.Text;

            if (this.fromComboBox.SelectedIndex >= 0) {
                Properties.Settings.Default.AzureTranslationFrom = this.Languages[this.fromComboBox.SelectedIndex].Item1;
            } else {
                Properties.Settings.Default.AzureTranslationFrom = "";
            }

            if (this.to1ComboBox.SelectedIndex >= 0) {
                Properties.Settings.Default.AzureTranslationTo1 = this.Languages[this.to1ComboBox.SelectedIndex].Item1;
            } else {
                Properties.Settings.Default.AzureTranslationTo1 = "";
            }
            Properties.Settings.Default.AzureTranslationTo1Run = this.to1CheckBox.Checked;

            if (this.to2ComboBox.SelectedIndex >= 0) {
                Properties.Settings.Default.AzureTranslationTo2 = this.Languages[this.to2ComboBox.SelectedIndex].Item1;
            } else {
                Properties.Settings.Default.AzureTranslationTo2 = "";
            }
            Properties.Settings.Default.AzureTranslationTo2Run = this.to2CheckBox.Checked;
        }

        public AzureTranslationPage() {
            InitializeComponent();
            Array.Sort(this.Languages, (a, b) => a.Item2.CompareTo(b.Item2));
        }

        public async Task<TranslationResult> TranslateAsync(string text, string? from, string[] toList) {
            var result = new TranslationResult();
            if (toList.Length == 0)
                return result;

            string route = String.Format("translate?api-version=3.0");
            if (from != null) {
                route = route + "&from=" + from;
            }
            foreach (var to in toList) {
                route = route + "&to=" + to;
            }

            object[] body = new object[] { new { Text = text } };
            var requestBody = JsonConvert.SerializeObject(body);

            using (var request = new HttpRequestMessage()) {
                try {
                    // Build the request.
                    request.Method = HttpMethod.Post;
                    request.RequestUri = new Uri(endpoint + route);
                    request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                    request.Headers.Add("Ocp-Apim-Subscription-Key", this.keyTextBox.Text);
                    request.Headers.Add("Ocp-Apim-Subscription-Region", this.regionTextBox.Text);

                    // Send the request and get response.
                    HttpResponseMessage response = await this.HttpClient.SendAsync(request).ConfigureAwait(false);
                    /*
                                    if (response.StatusCode < 200 || response.StatusCode >= 300) {
                                        return result;
                                    }
                    */
                    // Read response.
                    string responseText = await response.Content.ReadAsStringAsync();
                    AzureTranslationResult[]? azureResults = JsonConvert.DeserializeObject<AzureTranslationResult[]>(responseText);

                    if (azureResults == null)
                        return result;
                    foreach (var azureResult in azureResults) {
                        result.DetectLanguage = azureResult.detectedLanguage?.language;

                        if (azureResult.translations == null)
                            continue;

                        foreach (var translation in azureResult.translations) {
                            var t = new TranslationResult.Translation();
                            t.Text = translation.text != null ? translation.text : "";
                            t.Language = translation.to;
                            result.Translations.Add(t);
                        }
                    }
                } catch (Exception ex) {
                    Debug.WriteLine(ex.Message);
                    result.Error = true;
                    result.Translations.Add(new TranslationResult.Translation() { Text = ex.Message });
                }

                return result;
            }
        }

        private void AzureTranslationPage_Load(object sender, EventArgs e) {
            foreach (var lang in this.Languages) {
                this.fromComboBox.Items.Add(lang.Item2);
                this.to1ComboBox.Items.Add(lang.Item2);
                this.to2ComboBox.Items.Add(lang.Item2);
            }
        }

        // 対応している言語のリスト
        Tuple<string, string>[] Languages = {
            new Tuple<string, string>("af", "アフリカーンス語"),
            new Tuple<string, string>("am", "アムハラ語"),
            new Tuple<string, string>("ar", "アラビア語"),
            new Tuple<string, string>("as", "アッサム語"),
            new Tuple<string, string>("az", "アゼルバイジャン語"),
            new Tuple<string, string>("ba", "バシキール語"),
            new Tuple<string, string>("bg", "ブルガリア語"),
            new Tuple<string, string>("bn", "ベンガル語"),
            new Tuple<string, string>("bo", "チベット語"),
            new Tuple<string, string>("bs", "ボスニア語"),
            new Tuple<string, string>("ca", "カタロニア語"),
            new Tuple<string, string>("cs", "チェコ語"),
            new Tuple<string, string>("cy", "ウェールズ語"),
            new Tuple<string, string>("da", "デンマーク語"),
            new Tuple<string, string>("de", "ドイツ語"),
            new Tuple<string, string>("dv", "ディベヒ語"),
            new Tuple<string, string>("el", "ギリシャ語"),
            new Tuple<string, string>("en", "英語"),
            new Tuple<string, string>("es", "スペイン語"),
            new Tuple<string, string>("et", "エストニア語"),
            new Tuple<string, string>("eu", "バスク語"),
            new Tuple<string, string>("fa", "ペルシア語"),
            new Tuple<string, string>("fi", "フィンランド語"),
            new Tuple<string, string>("fil", "フィリピノ語"),
            new Tuple<string, string>("fj", "フィジー語"),
            new Tuple<string, string>("fo", "フェロー語"),
            new Tuple<string, string>("fr", "フランス語"),
            new Tuple<string, string>("fr-CA", "フランス語（カナダ）"),
            new Tuple<string, string>("ga", "アイルランド語"),
            new Tuple<string, string>("gl", "ガリシア語"),
            new Tuple<string, string>("gu", "グジャラート語"),
            new Tuple<string, string>("he", "ヘブライ語"),
            new Tuple<string, string>("hi", "ヒンディー語"),
            new Tuple<string, string>("hr", "クロアチア語"),
            new Tuple<string, string>("hsb", "高地ソルブ語"),
            new Tuple<string, string>("ht", "クレオール語（ハイチ）"),
            new Tuple<string, string>("hu", "ハンガリー語"),
            new Tuple<string, string>("hy", "アルメニア語"),
            new Tuple<string, string>("id", "インドネシア語"),
            new Tuple<string, string>("ikt", "Inuinnaqtun"),
            new Tuple<string, string>("is", "アイスランド語"),
            new Tuple<string, string>("it", "イタリア語"),
            new Tuple<string, string>("iu", "イヌクティトット語"),
            new Tuple<string, string>("iu-Latn", "イヌクティトット語（ラテン文字）"),
            new Tuple<string, string>("ja", "日本語"),
            new Tuple<string, string>("ka", "ジョージア語"),
            new Tuple<string, string>("kk", "カザフ語"),
            new Tuple<string, string>("km", "クメール語"),
            new Tuple<string, string>("kmr", "クルド語 (北)"),
            new Tuple<string, string>("kn", "カンナダ語"),
            new Tuple<string, string>("ko", "韓国語"),
            new Tuple<string, string>("ku", "クルド語 (中央)"),
            new Tuple<string, string>("ky", "キルギス語"),
            new Tuple<string, string>("lo", "ラオ語"),
            new Tuple<string, string>("lt", "リトアニア語"),
            new Tuple<string, string>("lv", "ラトビア語"),
            new Tuple<string, string>("lzh", "Chinese (Literary)"),
            new Tuple<string, string>("mg", "マダガスカル語"),
            new Tuple<string, string>("mi", "マオリ語"),
            new Tuple<string, string>("mk", "マケドニア語"),
            new Tuple<string, string>("ml", "マラヤーラム語"),
            new Tuple<string, string>("mn-Cyrl", "モンゴル語（キリル文字）"),
            new Tuple<string, string>("mn-Mong", "モンゴル語（モンゴル文字）"),
            new Tuple<string, string>("mr", "マラーティー語"),
            new Tuple<string, string>("ms", "マレー語"),
            new Tuple<string, string>("mt", "マルタ語"),
            new Tuple<string, string>("mww", "フモン語"),
            new Tuple<string, string>("my", "ミャンマー語"),
            new Tuple<string, string>("nb", "ノルウェー語(ブークモール)"),
            new Tuple<string, string>("ne", "ネパール語"),
            new Tuple<string, string>("nl", "オランダ語"),
            new Tuple<string, string>("or", "オディア語"),
            new Tuple<string, string>("otq", "ケレタロ オトミ語"),
            new Tuple<string, string>("pa", "パンジャブ語"),
            new Tuple<string, string>("pl", "ポーランド語"),
            new Tuple<string, string>("prs", "ダリー語"),
            new Tuple<string, string>("ps", "パシュトー語"),
            new Tuple<string, string>("pt", "ポルトガル語 (ブラジル)"),
            new Tuple<string, string>("pt-PT", "ポルトガル語 (ポルトガル)"),
            new Tuple<string, string>("ro", "ルーマニア語"),
            new Tuple<string, string>("ru", "ロシア語"),
            new Tuple<string, string>("sk", "スロバキア語"),
            new Tuple<string, string>("sl", "スロベニア語"),
            new Tuple<string, string>("sm", "サモア語"),
            new Tuple<string, string>("so", "ソマリ語"),
            new Tuple<string, string>("sq", "アルバニア語"),
            new Tuple<string, string>("sr-Cyrl", "セルビア語 (キリル文字)"),
            new Tuple<string, string>("sr-Latn", "セルビア語 (ラテン文字)"),
            new Tuple<string, string>("sv", "スウェーデン語"),
            new Tuple<string, string>("sw", "スワヒリ語"),
            new Tuple<string, string>("ta", "タミル語"),
            new Tuple<string, string>("te", "テルグ語"),
            new Tuple<string, string>("th", "タイ語"),
            new Tuple<string, string>("ti", "ティグリニア語"),
            new Tuple<string, string>("tk", "トルクメン語"),
            new Tuple<string, string>("tlh-Latn", "クリンゴン語 (ラテン文字)"),
            new Tuple<string, string>("tlh-Piqd", "クリンゴン語 (pIqaD)"),
            new Tuple<string, string>("to", "トンガ語"),
            new Tuple<string, string>("tr", "トルコ語"),
            new Tuple<string, string>("tt", "タタール語"),
            new Tuple<string, string>("ty", "タヒチ語"),
            new Tuple<string, string>("ug", "ウイグル語"),
            new Tuple<string, string>("uk", "ウクライナ語"),
            new Tuple<string, string>("ur", "ウルドゥー語"),
            new Tuple<string, string>("uz", "ウズベク語"),
            new Tuple<string, string>("vi", "ベトナム語"),
            new Tuple<string, string>("yua", "ユカテコ語"),
            new Tuple<string, string>("yue", "広東語 (繁体字)"),
            new Tuple<string, string>("zu", "ズールー語"),
            new Tuple<string, string>("zh-Hans", "中国語 (簡体字)"),
            new Tuple<string, string>("zh-Hant", "中国語 (繁体字)"),
        };
    }
}
