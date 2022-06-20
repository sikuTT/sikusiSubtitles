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
        Tuple<string, string>[] Languages = new GoogleTranslationLanguages().Languages;

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

        public GoogleBasicTranslationPage() {
            InitializeComponent();
            Array.Sort(Languages, (a, b) => a.Item2.CompareTo(b.Item2));
        }

        public override void LoadSettings() {
            this.keyTextBox.Text = this.Decrypt(Properties.Settings.Default.GoogleTranslationBasicKey);
            this.to1CheckBox.Checked = Properties.Settings.Default.GoogleTranslationBasicTo1Run;
            this.to2CheckBox.Checked = Properties.Settings.Default.GoogleTranslationBasicTo2Run;

            for (var i = 0; i < this.Languages.Length; i++) {
                if (this.Languages[i].Item1 == Properties.Settings.Default.GoogleTranslationBasicFrom) {
                    this.fromComboBox.SelectedIndex = i;
                } else if (this.Languages[i].Item1 == Properties.Settings.Default.GoogleTranslationBasicTo1) {
                    this.to1ComboBox.SelectedIndex = i;
                } else if (this.Languages[i].Item1 == Properties.Settings.Default.GoogleTranslationBasicTo2) {
                    this.to2ComboBox.SelectedIndex = i;
                }
            }
        }

        public override void SaveSettings() {
            Properties.Settings.Default.GoogleTranslationBasicKey = this.Encrypt(this.keyTextBox.Text);
            Properties.Settings.Default.GoogleTranslationBasicTo1Run = this.to1CheckBox.Checked;
            Properties.Settings.Default.GoogleTranslationBasicTo2Run = this.to2CheckBox.Checked;

            if (this.fromComboBox.SelectedIndex >= 0) {
                Properties.Settings.Default.GoogleTranslationBasicFrom = this.Languages[this.fromComboBox.SelectedIndex].Item1;
            } else {
                Properties.Settings.Default.GoogleTranslationBasicFrom = "";
            }

            if (this.to1ComboBox.SelectedIndex >= 0) {
                Properties.Settings.Default.GoogleTranslationBasicTo1 = this.Languages[this.to1ComboBox.SelectedIndex].Item1;
            } else {
                Properties.Settings.Default.GoogleTranslationBasicTo1 = "";
            }

            if (this.to2ComboBox.SelectedIndex >= 0) {
                Properties.Settings.Default.GoogleTranslationBasicTo2 = this.Languages[this.to2ComboBox.SelectedIndex].Item1;
            } else {
                Properties.Settings.Default.GoogleTranslationBasicTo2 = "";
            }
        }

        public async Task<TranslationResult> TranslateAsync(string text, string? from, string[] toList) {
            TranslationResult result = new TranslationResult();
            if (toList.Length == 0)
                return result;

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
                this.fromComboBox.Items.Add(lang.Item2);
                this.to1ComboBox.Items.Add(lang.Item2);
                this.to2ComboBox.Items.Add(lang.Item2);
            }
        }
    }
}
