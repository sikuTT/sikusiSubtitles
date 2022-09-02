using DeepL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sikusiSubtitles.Translation {
    public partial class DeepLTranslationPage : SettingPage {
        private DeepLTranslationService service;
        private List<Tuple<string, string>> languages;

        public override void LoadSettings() {
            this.keyTextBox.Text = this.Decrypt(Properties.Settings.Default.DeepLTranslationKey);
            this.to1CheckBox.Checked = Properties.Settings.Default.DeepLTranslationTo1Run;
            this.to2CheckBox.Checked = Properties.Settings.Default.DeepLTranslationTo2Run;

            for (var i = 0; i < this.languages.Count; i++) {
                if (this.languages[i].Item1 == Properties.Settings.Default.DeepLTranslationFrom) {
                    this.fromComboBox.SelectedIndex = i;
                } else if (this.languages[i].Item1 == Properties.Settings.Default.DeepLTranslationTo1) {
                    this.to1ComboBox.SelectedIndex = i;
                } else if (this.languages[i].Item1 == Properties.Settings.Default.DeepLTranslationTo2) {
                    this.to2ComboBox.SelectedIndex = i;
                }
            }
        }

        public override void SaveSettings() {
            Properties.Settings.Default.DeepLTranslationKey= this.Encrypt(this.keyTextBox.Text);
            Properties.Settings.Default.DeepLTranslationTo1Run = this.to1CheckBox.Checked;
            Properties.Settings.Default.DeepLTranslationTo2Run = this.to2CheckBox.Checked;

            if (this.fromComboBox.SelectedIndex >= 0) {
                Properties.Settings.Default.DeepLTranslationFrom = this.languages[this.fromComboBox.SelectedIndex].Item1;
            } else {
                Properties.Settings.Default.DeepLTranslationFrom = "";
            }

            if (this.to1ComboBox.SelectedIndex >= 0) {
                Properties.Settings.Default.DeepLTranslationTo1 = this.languages[this.to1ComboBox.SelectedIndex].Item1;
            } else {
                Properties.Settings.Default.DeepLTranslationTo1 = "";
            }

            if (this.to2ComboBox.SelectedIndex >= 0) {
                Properties.Settings.Default.DeepLTranslationTo2 = this.languages[this.to2ComboBox.SelectedIndex].Item1;
            } else {
                Properties.Settings.Default.DeepLTranslationTo2 = "";
            }
        }

        public DeepLTranslationPage(Service.ServiceManager serviceManager) : base (serviceManager) {
            this.service = new DeepLTranslationService(serviceManager);
            this.languages = this.service.GetLanguages();

            InitializeComponent();
        }


        private void DeepLTranslationPage_Load(object sender, EventArgs e) {
            foreach (var lang in languages) {
                this.fromComboBox.Items.Add(lang.Item2);
                this.to1ComboBox.Items.Add(lang.Item2);
                this.to2ComboBox.Items.Add(lang.Item2);
            }
        }

        private void keyTextBox_TextChanged(object sender, EventArgs e) {
            this.service.Key = keyTextBox.Text;
        }

        private void fromComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            this.service.From = this.languages[fromComboBox.SelectedIndex].Item1;
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
            if (to1CheckBox.Checked && to1ComboBox.SelectedIndex != -1) {
                this.service.To1 = this.languages[to1ComboBox.SelectedIndex].Item1;
            } else {
                this.service.To1 = null;
            }
        }

        private void SetTo2() {
            if (to2CheckBox.Checked && to2ComboBox.SelectedIndex != -1) {
                this.service.To2 = this.languages[to2ComboBox.SelectedIndex].Item1;
            } else {
                this.service.To2 = null;
            }
        }
    }
}
