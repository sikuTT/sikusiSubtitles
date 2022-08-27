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
        GoogleAppsScriptTranslationService service;
        Tuple<string, string>[] languages = new GoogleTranslationLanguages().Languages;

        public GoogleAppsScriptTranslationPage(Service.ServiceManager serviceManager) : base(serviceManager) {
            this.service = new GoogleAppsScriptTranslationService(serviceManager);

            InitializeComponent();
        }


        public override void LoadSettings() {
            this.keyTextBox.Text = this.Decrypt(Properties.Settings.Default.GoogleAppsScriptTranslationKey);
            this.to1CheckBox.Checked = Properties.Settings.Default.GoogleAppsScriptTranslationTo1Run;
            this.to2CheckBox.Checked = Properties.Settings.Default.GoogleAppsScriptTranslationTo2Run;

            for (var i = 0; i < this.languages.Length; i++) {
                if (this.languages[i].Item1 == Properties.Settings.Default.GoogleAppsScriptTranslationFrom) {
                    this.fromComboBox.SelectedIndex = i;
                } else if (this.languages[i].Item1 == Properties.Settings.Default.GoogleAppsScriptTranslationTo1) {
                    this.to1ComboBox.SelectedIndex = i;
                } else if (this.languages[i].Item1 == Properties.Settings.Default.GoogleAppsScriptTranslationTo2) {
                    this.to2ComboBox.SelectedIndex = i;
                }
            }
        }

        public override void SaveSettings() {
            Properties.Settings.Default.GoogleAppsScriptTranslationKey = this.Encrypt(this.keyTextBox.Text);
            Properties.Settings.Default.GoogleAppsScriptTranslationTo1Run = this.to1CheckBox.Checked;
            Properties.Settings.Default.GoogleAppsScriptTranslationTo2Run = this.to2CheckBox.Checked;

            if (this.fromComboBox.SelectedIndex >= 0) {
                Properties.Settings.Default.GoogleAppsScriptTranslationFrom = this.languages[this.fromComboBox.SelectedIndex].Item1;
            } else {
                Properties.Settings.Default.GoogleAppsScriptTranslationFrom = "";
            }

            if (this.to1ComboBox.SelectedIndex >= 0) {
                Properties.Settings.Default.GoogleAppsScriptTranslationTo1 = this.languages[this.to1ComboBox.SelectedIndex].Item1;
            } else {
                Properties.Settings.Default.GoogleAppsScriptTranslationTo1 = "";
            }

            if (this.to2ComboBox.SelectedIndex >= 0) {
                Properties.Settings.Default.GoogleAppsScriptTranslationTo2 = this.languages[this.to2ComboBox.SelectedIndex].Item1;
            } else {
                Properties.Settings.Default.GoogleAppsScriptTranslationTo2 = "";
            }
        }

        private void GoogleAppsScriptTranslationPage_Load(object sender, EventArgs e) {
            foreach (var lang in this.languages) {
                this.fromComboBox.Items.Add(lang.Item2);
                this.to1ComboBox.Items.Add(lang.Item2);
                this.to2ComboBox.Items.Add(lang.Item2);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            ProcessStartInfo pi = new ProcessStartInfo() {
                FileName = "https://script.google.com/home",
                UseShellExecute = true,
            };
            System.Diagnostics.Process.Start(pi);
        }

        private void keyTextBox_TextChanged(object sender, EventArgs e) {
            this.service.Key = keyTextBox.Text;
        }

        private void fromComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            this.service.From = this.languages[fromComboBox.SelectedIndex].Item1;
        }

        private void to1CheckBox_CheckedChanged(object sender, EventArgs e) {
            this.SetTo1();
        }

        private void to1ComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            this.SetTo1();
        }

        private void to2CheckBox_CheckedChanged(object sender, EventArgs e) {
            this.SetTo2();
        }

        private void to2ComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            this.SetTo2();
        }

        private void SetTo1() {
            if (this.to1CheckBox.Checked && this.to1ComboBox.SelectedIndex != -1) {
                this.service.To1 = this.languages[this.to1ComboBox.SelectedIndex].Item1;
            } else {
                this.service.To1 = null;
            }
        }

        private void SetTo2() {
            if (this.to2CheckBox.Checked && this.to2ComboBox.SelectedIndex != -1) {
                this.service.To2 = this.languages[this.to2ComboBox.SelectedIndex].Item1;
            } else {
                this.service.To2 = null;
            }
        }
    }
}
