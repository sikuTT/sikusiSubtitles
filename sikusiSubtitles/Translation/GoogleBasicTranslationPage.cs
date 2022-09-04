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
        private GoogleBasicTranslationService service;
        private List<Tuple<string, string>> languages = new GoogleTranslationLanguages().Languages;

        public GoogleBasicTranslationPage(Service.ServiceManager serviceManager) : base(serviceManager) {
            this.service = new GoogleBasicTranslationService(serviceManager);

            InitializeComponent();

            languages.Sort((a, b) => a.Item2.CompareTo(b.Item2));
        }

        public override void LoadSettings() {
            this.keyTextBox.Text = this.Decrypt(Properties.Settings.Default.GoogleTranslationBasicKey);
            this.to1CheckBox.Checked = Properties.Settings.Default.GoogleTranslationBasicTo1Run;
            this.to2CheckBox.Checked = Properties.Settings.Default.GoogleTranslationBasicTo2Run;

            for (var i = 0; i < this.languages.Count; i++) {
                if (this.languages[i].Item1 == Properties.Settings.Default.GoogleTranslationBasicFrom) {
                    this.fromComboBox.SelectedIndex = i;
                } else if (this.languages[i].Item1 == Properties.Settings.Default.GoogleTranslationBasicTo1) {
                    this.to1ComboBox.SelectedIndex = i;
                } else if (this.languages[i].Item1 == Properties.Settings.Default.GoogleTranslationBasicTo2) {
                    this.to2ComboBox.SelectedIndex = i;
                }
            }
        }

        public override void SaveSettings() {
            Properties.Settings.Default.GoogleTranslationBasicKey = this.Encrypt(this.keyTextBox.Text);
            Properties.Settings.Default.GoogleTranslationBasicTo1Run = this.to1CheckBox.Checked;
            Properties.Settings.Default.GoogleTranslationBasicTo2Run = this.to2CheckBox.Checked;

            if (this.fromComboBox.SelectedIndex >= 0) {
                Properties.Settings.Default.GoogleTranslationBasicFrom = this.languages[this.fromComboBox.SelectedIndex].Item1;
            } else {
                Properties.Settings.Default.GoogleTranslationBasicFrom = "";
            }

            if (this.to1ComboBox.SelectedIndex >= 0) {
                Properties.Settings.Default.GoogleTranslationBasicTo1 = this.languages[this.to1ComboBox.SelectedIndex].Item1;
            } else {
                Properties.Settings.Default.GoogleTranslationBasicTo1 = "";
            }

            if (this.to2ComboBox.SelectedIndex >= 0) {
                Properties.Settings.Default.GoogleTranslationBasicTo2 = this.languages[this.to2ComboBox.SelectedIndex].Item1;
            } else {
                Properties.Settings.Default.GoogleTranslationBasicTo2 = "";
            }
        }

        private void GoogleTranslationPage_Load(object sender, EventArgs e) {
            foreach (var lang in this.languages) {
                this.fromComboBox.Items.Add(lang.Item2);
                this.to1ComboBox.Items.Add(lang.Item2);
                this.to2ComboBox.Items.Add(lang.Item2);
            }
        }

        private void keyTextBox_TextChanged(object sender, EventArgs e) {
            this.service.Key = keyTextBox.Text;
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
