using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sikusiSubtitles.Translation {
    public partial class GoogleAppsScriptTranslationPage : SettingPage {
        Tuple<string, string>[] Languages = new GoogleTranslationLanguages().Languages;
        HttpClient HttpClient = new HttpClient();

        public GoogleAppsScriptTranslationPage() {
            InitializeComponent();
        }

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

        public override void LoadSettings() {
            this.keyTextBox.Text = this.Decrypt(Properties.Settings.Default.GoogleAppsScriptTranslationKey);
            this.to1CheckBox.Checked = Properties.Settings.Default.GoogleAppsScriptTranslationTo1Run;
            this.to2CheckBox.Checked = Properties.Settings.Default.GoogleAppsScriptTranslationTo2Run;

            for (var i = 0; i < this.Languages.Length; i++) {
                if (this.Languages[i].Item1 == Properties.Settings.Default.GoogleAppsScriptTranslationFrom) {
                    this.fromComboBox.SelectedIndex = i;
                } else if (this.Languages[i].Item1 == Properties.Settings.Default.GoogleAppsScriptTranslationTo1) {
                    this.to1ComboBox.SelectedIndex = i;
                } else if (this.Languages[i].Item1 == Properties.Settings.Default.GoogleAppsScriptTranslationTo2) {
                    this.to2ComboBox.SelectedIndex = i;
                }
            }
        }

        public override void SaveSettings() {
            Properties.Settings.Default.GoogleAppsScriptTranslationKey = this.Encrypt(this.keyTextBox.Text);
            Properties.Settings.Default.GoogleAppsScriptTranslationTo1Run = this.to1CheckBox.Checked;
            Properties.Settings.Default.GoogleAppsScriptTranslationTo2Run = this.to2CheckBox.Checked;

            if (this.fromComboBox.SelectedIndex >= 0) {
                Properties.Settings.Default.GoogleAppsScriptTranslationFrom = this.Languages[this.fromComboBox.SelectedIndex].Item1;
            } else {
                Properties.Settings.Default.GoogleAppsScriptTranslationFrom = "";
            }

            if (this.to1ComboBox.SelectedIndex >= 0) {
                Properties.Settings.Default.GoogleAppsScriptTranslationTo1 = this.Languages[this.to1ComboBox.SelectedIndex].Item1;
            } else {
                Properties.Settings.Default.GoogleAppsScriptTranslationTo1 = "";
            }

            if (this.to2ComboBox.SelectedIndex >= 0) {
                Properties.Settings.Default.GoogleAppsScriptTranslationTo2 = this.Languages[this.to2ComboBox.SelectedIndex].Item1;
            } else {
                Properties.Settings.Default.GoogleAppsScriptTranslationTo2 = "";
            }
        }

        public async Task<TranslationResult> TranslateAsync(string text, string? from, string[] toList) {
            TranslationResult result = new TranslationResult();
            if (toList.Length == 0)
                return result;

            try {
                foreach (var to in toList) {
                    var url = "https://script.google.com/macros/s/";
                    url += this.keyTextBox.Text + "/exec?text=" + text;
                    if (from != null)
                        url += "&source=" + from;
                    url += "&target=" + to;
                    using var request = new HttpRequestMessage();
                    request.Method = HttpMethod.Get;
                    request.RequestUri = new Uri(url);
                    HttpResponseMessage response = await this.HttpClient.SendAsync(request).ConfigureAwait(false);
                    string responseText = await response.Content.ReadAsStringAsync();
                    result.Translations.Add(new TranslationResult.Translation() { Text = responseText });
                }
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message);
                result.Translations.Add(new TranslationResult.Translation() { Text = ex.Message });
                result.Error = true;
            }

            return result;
        }

        private void GoogleAppsScriptTranslationPage_Load(object sender, EventArgs e) {
            foreach (var lang in this.Languages) {
                this.fromComboBox.Items.Add(lang.Item2);
                this.to1ComboBox.Items.Add(lang.Item2);
                this.to2ComboBox.Items.Add(lang.Item2);
            }
        }
    }
}
