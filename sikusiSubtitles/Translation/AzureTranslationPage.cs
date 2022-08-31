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
        private Tuple<string, string>[] languages;

        public AzureTranslationService service;

        public AzureTranslationPage(Service.ServiceManager serviceManager) : base(serviceManager) {
            this.service = new AzureTranslationService(serviceManager);

            InitializeComponent();

            this.languages = this.service.GetLanguages();
        }

        /**
         * 設定の読み込み
         */
        public override void LoadSettings() {
            this.keyTextBox.Text = this.Decrypt(Properties.Settings.Default.AzureTranslationKey);
            this.regionTextBox.Text = Properties.Settings.Default.AzureTranslationRegion;
            this.to1CheckBox.Checked = Properties.Settings.Default.AzureTranslationTo1Run;
            this.to2CheckBox.Checked = Properties.Settings.Default.AzureTranslationTo2Run;

            for (var i = 0; i < this.languages.Length; i++) {
                if (this.languages[i].Item1 == Properties.Settings.Default.AzureTranslationFrom) {
                    this.fromComboBox.SelectedIndex = i;
                } else if (this.languages[i].Item1 == Properties.Settings.Default.AzureTranslationTo1) {
                    this.to1ComboBox.SelectedIndex = i;
                } else if (this.languages[i].Item1 == Properties.Settings.Default.AzureTranslationTo2) {
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
                Properties.Settings.Default.AzureTranslationFrom = this.languages[this.fromComboBox.SelectedIndex].Item1;
            } else {
                Properties.Settings.Default.AzureTranslationFrom = "";
            }

            if (this.to1ComboBox.SelectedIndex >= 0) {
                Properties.Settings.Default.AzureTranslationTo1 = this.languages[this.to1ComboBox.SelectedIndex].Item1;
            } else {
                Properties.Settings.Default.AzureTranslationTo1 = "";
            }
            Properties.Settings.Default.AzureTranslationTo1Run = this.to1CheckBox.Checked;

            if (this.to2ComboBox.SelectedIndex >= 0) {
                Properties.Settings.Default.AzureTranslationTo2 = this.languages[this.to2ComboBox.SelectedIndex].Item1;
            } else {
                Properties.Settings.Default.AzureTranslationTo2 = "";
            }
            Properties.Settings.Default.AzureTranslationTo2Run = this.to2CheckBox.Checked;
        }

        private void AzureTranslationPage_Load(object sender, EventArgs e) {
            foreach (var lang in this.languages) {
                this.fromComboBox.Items.Add(lang.Item2);
                this.to1ComboBox.Items.Add(lang.Item2);
                this.to2ComboBox.Items.Add(lang.Item2);
            }
        }


        private void keyTextBox_TextChanged(object sender, EventArgs e) {
            this.service.Key = this.keyTextBox.Text;
        }

        private void regionTextBox_TextChanged(object sender, EventArgs e) {
            this.service.Region = this.regionTextBox.Text;
        }

        private void fromComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            this.service.From = this.languages[this.fromComboBox.SelectedIndex].Item1;
        }

        private void to1CheckBox_CheckedChanged(object sender, EventArgs e) {
            SetTo1();
        }

        private void to1ComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            SetTo1();
        }

        private void to2CheckBox_CheckedChanged(object sender, EventArgs e) {
            SetTo2();
        }

        private void to2ComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            SetTo2();
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
