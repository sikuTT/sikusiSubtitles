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

        public string? From { get { return this.fromComboBox.SelectedIndex >= 0 ? this.Languages[this.fromComboBox.SelectedIndex].Key : null; } }

        /**
         * 翻訳先の言語一覧
         */
        public List<string> To {
            get {
                var to = new List<String>();
                if (this.to1CheckBox.Checked && this.to1ComboBox.SelectedIndex != -1) {
                    to.Add(this.Languages[this.to1ComboBox.SelectedIndex].Key);
                }
                if (this.to2CheckBox.Checked && this.to2ComboBox.SelectedIndex != -1) {
                    to.Add(this.Languages[this.to2ComboBox.SelectedIndex].Key);
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
                if (this.Languages[i].Key == Properties.Settings.Default.AzureTranslationFrom) {
                    this.fromComboBox.SelectedIndex = i;
                } else if (this.Languages[i].Key == Properties.Settings.Default.AzureTranslationTo1) {
                    this.to1ComboBox.SelectedIndex = i;
                } else if (this.Languages[i].Key == Properties.Settings.Default.AzureTranslationTo2) {
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
                Properties.Settings.Default.AzureTranslationFrom = this.Languages[this.fromComboBox.SelectedIndex].Key;
            } else {
                Properties.Settings.Default.AzureTranslationFrom = "";
            }

            if (this.to1ComboBox.SelectedIndex >= 0) {
                Properties.Settings.Default.AzureTranslationTo1 = this.Languages[this.to1ComboBox.SelectedIndex].Key;
            } else {
                Properties.Settings.Default.AzureTranslationTo1 = "";
            }
            Properties.Settings.Default.AzureTranslationTo1Run = this.to1CheckBox.Checked;

            if (this.to2ComboBox.SelectedIndex >= 0) {
                Properties.Settings.Default.AzureTranslationTo2 = this.Languages[this.to2ComboBox.SelectedIndex].Key;
            } else {
                Properties.Settings.Default.AzureTranslationTo2 = "";
            }
            Properties.Settings.Default.AzureTranslationTo2Run = this.to2CheckBox.Checked;
        }

        public AzureTranslationPage() {
            InitializeComponent();
            Array.Sort(this.Languages, (a, b) => a.Name.CompareTo(b.Name));
        }

        public async Task<TranslationResult> TranslateAsync(string text, string? from, string[] toList) {
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
                var result = new TranslationResult();

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
                this.fromComboBox.Items.Add(lang.Name);
                this.to1ComboBox.Items.Add(lang.Name);
                this.to2ComboBox.Items.Add(lang.Name);
            }
        }

        // 対応している言語のリスト
        Language[] Languages = new Language[] {
            new Language { Key = "af", Name = "アフリカーンス語" },
            new Language { Key = "am", Name = "アムハラ語" },
            new Language { Key = "ar", Name = "アラビア語" },
            new Language { Key = "as", Name = "アッサム語" },
            new Language { Key = "az", Name = "アゼルバイジャン語" },
            new Language { Key = "ba", Name = "バシキール語" },
            new Language { Key = "bg", Name = "ブルガリア語" },
            new Language { Key = "bn", Name = "ベンガル語" },
            new Language { Key = "bo", Name = "チベット語" },
            new Language { Key = "bs", Name = "ボスニア語" },
            new Language { Key = "ca", Name = "カタロニア語" },
            new Language { Key = "cs", Name = "チェコ語" },
            new Language { Key = "cy", Name = "ウェールズ語" },
            new Language { Key = "da", Name = "デンマーク語" },
            new Language { Key = "de", Name = "ドイツ語" },
            new Language { Key = "dv", Name = "ディベヒ語" },
            new Language { Key = "el", Name = "ギリシャ語" },
            new Language { Key = "en", Name = "英語" },
            new Language { Key = "es", Name = "スペイン語" },
            new Language { Key = "et", Name = "エストニア語" },
            new Language { Key = "eu", Name = "バスク語" },
            new Language { Key = "fa", Name = "ペルシア語" },
            new Language { Key = "fi", Name = "フィンランド語" },
            new Language { Key = "fil", Name = "フィリピノ語" },
            new Language { Key = "fj", Name = "フィジー語" },
            new Language { Key = "fo", Name = "フェロー語" },
            new Language { Key = "fr", Name = "フランス語" },
            new Language { Key = "fr-CA", Name = "フランス語（カナダ）" },
            new Language { Key = "ga", Name = "アイルランド語" },
            new Language { Key = "gl", Name = "ガリシア語" },
            new Language { Key = "gu", Name = "グジャラート語" },
            new Language { Key = "he", Name = "ヘブライ語" },
            new Language { Key = "hi", Name = "ヒンディー語" },
            new Language { Key = "hr", Name = "クロアチア語" },
            new Language { Key = "hsb", Name = "高地ソルブ語" },
            new Language { Key = "ht", Name = "クレオール語（ハイチ）" },
            new Language { Key = "hu", Name = "ハンガリー語" },
            new Language { Key = "hy", Name = "アルメニア語" },
            new Language { Key = "id", Name = "インドネシア語" },
            new Language { Key = "ikt", Name = "Inuinnaqtun" },
            new Language { Key = "is", Name = "アイスランド語" },
            new Language { Key = "it", Name = "イタリア語" },
            new Language { Key = "iu", Name = "イヌクティトット語" },
            new Language { Key = "iu-Latn", Name = "イヌクティトット語（ラテン文字）" },
            new Language { Key = "ja", Name = "日本語" },
            new Language { Key = "ka", Name = "ジョージア語" },
            new Language { Key = "kk", Name = "カザフ語" },
            new Language { Key = "km", Name = "クメール語" },
            new Language { Key = "kmr", Name = "クルド語 (北)" },
            new Language { Key = "kn", Name = "カンナダ語" },
            new Language { Key = "ko", Name = "韓国語" },
            new Language { Key = "ku", Name = "クルド語 (中央)" },
            new Language { Key = "ky", Name = "キルギス語" },
            new Language { Key = "lo", Name = "ラオ語" },
            new Language { Key = "lt", Name = "リトアニア語" },
            new Language { Key = "lv", Name = "ラトビア語" },
            new Language { Key = "lzh", Name = "Chinese (Literary)" },
            new Language { Key = "mg", Name = "マダガスカル語" },
            new Language { Key = "mi", Name = "マオリ語" },
            new Language { Key = "mk", Name = "マケドニア語" },
            new Language { Key = "ml", Name = "マラヤーラム語" },
            new Language { Key = "mn-Cyrl", Name = "モンゴル語（キリル文字）" },
            new Language { Key = "mn-Mong", Name = "モンゴル語（モンゴル文字）" },
            new Language { Key = "mr", Name = "マラーティー語" },
            new Language { Key = "ms", Name = "マレー語" },
            new Language { Key = "mt", Name = "マルタ語" },
            new Language { Key = "mww", Name = "フモン語" },
            new Language { Key = "my", Name = "ミャンマー語" },
            new Language { Key = "nb", Name = "ノルウェー語(ブークモール)" },
            new Language { Key = "ne", Name = "ネパール語" },
            new Language { Key = "nl", Name = "オランダ語" },
            new Language { Key = "or", Name = "オディア語" },
            new Language { Key = "otq", Name = "ケレタロ オトミ語" },
            new Language { Key = "pa", Name = "パンジャブ語" },
            new Language { Key = "pl", Name = "ポーランド語" },
            new Language { Key = "prs", Name = "ダリー語" },
            new Language { Key = "ps", Name = "パシュトー語" },
            new Language { Key = "pt", Name = "ポルトガル語 (ブラジル)" },
            new Language { Key = "pt-PT", Name = "ポルトガル語 (ポルトガル)" },
            new Language { Key = "ro", Name = "ルーマニア語" },
            new Language { Key = "ru", Name = "ロシア語" },
            new Language { Key = "sk", Name = "スロバキア語" },
            new Language { Key = "sl", Name = "スロベニア語" },
            new Language { Key = "sm", Name = "サモア語" },
            new Language { Key = "so", Name = "ソマリ語" },
            new Language { Key = "sq", Name = "アルバニア語" },
            new Language { Key = "sr-Cyrl", Name = "セルビア語 (キリル文字)" },
            new Language { Key = "sr-Latn", Name = "セルビア語 (ラテン文字)" },
            new Language { Key = "sv", Name = "スウェーデン語" },
            new Language { Key = "sw", Name = "スワヒリ語" },
            new Language { Key = "ta", Name = "タミル語" },
            new Language { Key = "te", Name = "テルグ語" },
            new Language { Key = "th", Name = "タイ語" },
            new Language { Key = "ti", Name = "ティグリニア語" },
            new Language { Key = "tk", Name = "トルクメン語" },
            new Language { Key = "tlh-Latn", Name = "クリンゴン語 (ラテン文字)" },
            new Language { Key = "tlh-Piqd", Name = "クリンゴン語 (pIqaD)" },
            new Language { Key = "to", Name = "トンガ語" },
            new Language { Key = "tr", Name = "トルコ語" },
            new Language { Key = "tt", Name = "タタール語" },
            new Language { Key = "ty", Name = "タヒチ語" },
            new Language { Key = "ug", Name = "ウイグル語" },
            new Language { Key = "uk", Name = "ウクライナ語" },
            new Language { Key = "ur", Name = "ウルドゥー語" },
            new Language { Key = "uz", Name = "ウズベク語" },
            new Language { Key = "vi", Name = "ベトナム語" },
            new Language { Key = "yua", Name = "ユカテコ語" },
            new Language { Key = "yue", Name = "広東語 (繁体字)" },
            new Language { Key = "zu", Name = "ズールー語" },
            new Language { Key = "zh-Hans", Name = "中国語 (簡体字)" },
            new Language { Key = "zh-Hant", Name = "中国語 (繁体字)" },
        };
    }
}
