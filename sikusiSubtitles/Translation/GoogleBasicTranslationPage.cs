using Google.Apis.Services;
using Google.Apis.Translate.v2;
using Google.Apis.Translate.v2.Data;
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
    public partial class GoogleBasicTranslationPage : SettingPage {

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

        public GoogleBasicTranslationPage() {
            InitializeComponent();
            Array.Sort(Languages, (a, b) => a.Name.CompareTo(b.Name));
        }

        public override void LoadSettings() {
            this.keyTextBox.Text = this.Decrypt(Properties.Settings.Default.GoogleTranslationBasicKey);
            this.to1CheckBox.Checked = Properties.Settings.Default.GoogleTranslationBasicTo1Run;
            this.to2CheckBox.Checked = Properties.Settings.Default.GoogleTranslationBasicTo2Run;

            for (var i = 0; i < this.Languages.Length; i++) {
                if (this.Languages[i].Key == Properties.Settings.Default.GoogleTranslationBasicFrom) {
                    this.fromComboBox.SelectedIndex = i;
                } else if (this.Languages[i].Key == Properties.Settings.Default.GoogleTranslationBasicTo1) {
                    this.to1ComboBox.SelectedIndex = i;
                } else if (this.Languages[i].Key == Properties.Settings.Default.GoogleTranslationBasicTo2) {
                    this.to2ComboBox.SelectedIndex = i;
                }
            }
        }

        public override void SaveSettings() {
            Properties.Settings.Default.GoogleTranslationBasicKey = this.Encrypt(this.keyTextBox.Text);
            Properties.Settings.Default.GoogleTranslationBasicTo1Run = this.to1CheckBox.Checked;
            Properties.Settings.Default.GoogleTranslationBasicTo2Run = this.to2CheckBox.Checked;

            if (this.fromComboBox.SelectedIndex >= 0) {
                Properties.Settings.Default.GoogleTranslationBasicFrom = this.Languages[this.fromComboBox.SelectedIndex].Key;
            } else {
                Properties.Settings.Default.GoogleTranslationBasicFrom = "";
            }

            if (this.to1ComboBox.SelectedIndex >= 0) {
                Properties.Settings.Default.GoogleTranslationBasicTo1 = this.Languages[this.to1ComboBox.SelectedIndex].Key;
            } else {
                Properties.Settings.Default.GoogleTranslationBasicTo1 = "";
            }

            if (this.to2ComboBox.SelectedIndex >= 0) {
                Properties.Settings.Default.GoogleTranslationBasicTo2 = this.Languages[this.to2ComboBox.SelectedIndex].Key;
            } else {
                Properties.Settings.Default.GoogleTranslationBasicTo2 = "";
            }
        }

        public async Task<TranslationResult> TranslateAsync(string text, string? from, string[] toList) {
            TranslationResult result = new TranslationResult();

            try {
                var service = new TranslateService(new BaseClientService.Initializer() { ApiKey = this.keyTextBox.Text });

                string[] srcText = new[] { text };
                foreach (var to in toList) {
                    TranslationsListResponse response = await service.Translations.List(srcText, to).ExecuteAsync();

                    // We need to change this code...
                    // currently this code
                    foreach (var translation in response.Translations) {
                        result.DetectLanguage = translation.DetectedSourceLanguage;
                        var translatedText = translation.TranslatedText.Replace("&#39;", "'").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&quot;", "\"").Replace("&amp;", "&");
                        result.Translations.Add(new TranslationResult.Translation() { Text = translatedText });
                    }
                }
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message);
                result.Translations.Add(new TranslationResult.Translation() { Text = ex.Message });
                result.Error = true;
            }

            return result;
        }

        private void GoogleTranslationPage_Load(object sender, EventArgs e) {
            foreach (var lang in this.Languages) {
                this.fromComboBox.Items.Add(lang.Name);
                this.to1ComboBox.Items.Add(lang.Name);
                this.to2ComboBox.Items.Add(lang.Name);
            }
        }

        Language[] Languages = new Language[] {
            new Language { Key = "af", Name = "アフリカーンス語" },
            new Language { Key = "sq", Name = "アルバニア語" },
            new Language { Key = "am", Name = "アムハラ語" },
            new Language { Key = "ar", Name = "アラビア文字" },
            new Language { Key = "hy", Name = "アルメニア語" },
            new Language { Key = "az", Name = "アゼルバイジャン語" },
            new Language { Key = "eu", Name = "バスク語" },
            new Language { Key = "be", Name = "ベラルーシ語" },
            new Language { Key = "bn", Name = "ベンガル文字" },
            new Language { Key = "bs", Name = "ボスニア語" },
            new Language { Key = "bg", Name = "ブルガリア語" },
            new Language { Key = "ca", Name = "カタロニア語" },
            new Language { Key = "ceb", Name = "セブ語" },
            new Language { Key = "zh-CN", Name = "中国語（簡体）" },
            new Language { Key = "zh-TW", Name = "中国語（繁体）" },
            new Language { Key = "co", Name = "コルシカ語" },
            new Language { Key = "hr", Name = "クロアチア語" },
            new Language { Key = "cs", Name = "チェコ語" },
            new Language { Key = "da", Name = "デンマーク語" },
            new Language { Key = "nl", Name = "オランダ語" },
            new Language { Key = "en", Name = "英語" },
            new Language { Key = "eo", Name = "エスペラント語" },
            new Language { Key = "et", Name = "エストニア語" },
            new Language { Key = "fi", Name = "フィンランド語" },
            new Language { Key = "fr", Name = "フランス語" },
            new Language { Key = "fy", Name = "フリジア語" },
            new Language { Key = "gl", Name = "ガリシア語" },
            new Language { Key = "ka", Name = "ジョージア語" },
            new Language { Key = "de", Name = "ドイツ語" },
            new Language { Key = "el", Name = "ギリシャ語" },
            new Language { Key = "gu", Name = "グジャラート語" },
            new Language { Key = "ht", Name = "クレオール語（ハイチ）" },
            new Language { Key = "ha", Name = "ハウサ語" },
            new Language { Key = "haw", Name = "ハワイ語" },
            new Language { Key = "he", Name = "ヘブライ語" },
            new Language { Key = "hi", Name = "ヒンディー語" },
            new Language { Key = "hmn", Name = "モン語" },
            new Language { Key = "hu", Name = "ハンガリー語" },
            new Language { Key = "is", Name = "アイスランド語" },
            new Language { Key = "ig", Name = "イボ語" },
            new Language { Key = "id", Name = "インドネシア語" },
            new Language { Key = "ga", Name = "アイルランド語" },
            new Language { Key = "it", Name = "イタリア語" },
            new Language { Key = "ja", Name = "日本語" },
            new Language { Key = "jv", Name = "ジャワ語" },
            new Language { Key = "kn", Name = "カンナダ語" },
            new Language { Key = "kk", Name = "カザフ語" },
            new Language { Key = "km", Name = "クメール語" },
            new Language { Key = "rw", Name = "キニヤルワンダ語" },
            new Language { Key = "ko", Name = "韓国語" },
            new Language { Key = "ku", Name = "クルド語" },
            new Language { Key = "ky", Name = "キルギス語" },
            new Language { Key = "lo", Name = "ラオ語" },
            new Language { Key = "la", Name = "ラテン語" },
            new Language { Key = "lv", Name = "ラトビア語" },
            new Language { Key = "lt", Name = "リトアニア語" },
            new Language { Key = "lb", Name = "ルクセンブルク語" },
            new Language { Key = "mk", Name = "マケドニア語" },
            new Language { Key = "mg", Name = "マダガスカル語" },
            new Language { Key = "ms", Name = "マレー語" },
            new Language { Key = "ml", Name = "マラヤーラム語" },
            new Language { Key = "mt", Name = "マルタ語" },
            new Language { Key = "mi", Name = "マオリ語" },
            new Language { Key = "mr", Name = "マラーティー語" },
            new Language { Key = "mn", Name = "モンゴル語" },
            new Language { Key = "my", Name = "ミャンマー語（ビルマ語）" },
            new Language { Key = "ne", Name = "ネパール語" },
            new Language { Key = "no", Name = "ノルウェー語" },
            new Language { Key = "ny", Name = "ニャンジャ語（チェワ語）" },
            new Language { Key = "or", Name = "オリヤ語" },
            new Language { Key = "ps", Name = "パシュトー語" },
            new Language { Key = "fa", Name = "ペルシャ語" },
            new Language { Key = "pl", Name = "ポーランド語" },
            new Language { Key = "pt", Name = "ポルトガル語（ポルトガル、ブラジル）" },
            new Language { Key = "pa", Name = "パンジャブ語" },
            new Language { Key = "ro", Name = "ルーマニア語" },
            new Language { Key = "ru", Name = "ロシア語" },
            new Language { Key = "sm", Name = "サモア語" },
            new Language { Key = "gd", Name = "スコットランド ゲール語" },
            new Language { Key = "sr", Name = "セルビア語" },
            new Language { Key = "st", Name = "セソト語" },
            new Language { Key = "sn", Name = "ショナ語" },
            new Language { Key = "sd", Name = "シンド語" },
            new Language { Key = "si", Name = "シンハラ語" },
            new Language { Key = "sk", Name = "スロバキア語" },
            new Language { Key = "sl", Name = "スロベニア語" },
            new Language { Key = "so", Name = "ソマリ語" },
            new Language { Key = "es", Name = "スペイン語" },
            new Language { Key = "su", Name = "スンダ語" },
            new Language { Key = "sw", Name = "スワヒリ語" },
            new Language { Key = "sv", Name = "スウェーデン語" },
            new Language { Key = "tl", Name = "タガログ語（フィリピン語）" },
            new Language { Key = "tg", Name = "タジク語" },
            new Language { Key = "ta", Name = "タミル語" },
            new Language { Key = "tt", Name = "タタール語" },
            new Language { Key = "te", Name = "テルグ語" },
            new Language { Key = "th", Name = "タイ語" },
            new Language { Key = "tr", Name = "トルコ語" },
            new Language { Key = "tk", Name = "トルクメン語" },
            new Language { Key = "uk", Name = "ウクライナ語" },
            new Language { Key = "ur", Name = "ウルドゥー語" },
            new Language { Key = "ug", Name = "ウイグル語" },
            new Language { Key = "uz", Name = "ウズベク語" },
            new Language { Key = "vi", Name = "ベトナム語" },
            new Language { Key = "cy", Name = "ウェールズ語" },
            new Language { Key = "xh", Name = "コーサ語" },
            new Language { Key = "yi", Name = "イディッシュ語" },
            new Language { Key = "yo", Name = "ヨルバ語" },
            new Language { Key = "zu", Name = "ズールー語" },
        };
    }
}
