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

        public override void LoadSettings() {
            this.keyTextBox.Text = this.Decrypt(Properties.Settings.Default.DeepLTranslationKey);
            this.to1CheckBox.Checked = Properties.Settings.Default.DeepLTranslationTo1Run;
            this.to2CheckBox.Checked = Properties.Settings.Default.DeepLTranslationTo2Run;

            for (var i = 0; i < this.Languages.Length; i++) {
                if (this.Languages[i].Item1 == Properties.Settings.Default.DeepLTranslationFrom) {
                    this.fromComboBox.SelectedIndex = i;
                } else if (this.Languages[i].Item1 == Properties.Settings.Default.DeepLTranslationTo1) {
                    this.to1ComboBox.SelectedIndex = i;
                } else if (this.Languages[i].Item1 == Properties.Settings.Default.DeepLTranslationTo2) {
                    this.to2ComboBox.SelectedIndex = i;
                }
            }
        }

        public override void SaveSettings() {
            Properties.Settings.Default.DeepLTranslationKey= this.Encrypt(this.keyTextBox.Text);
            Properties.Settings.Default.DeepLTranslationTo1Run = this.to1CheckBox.Checked;
            Properties.Settings.Default.DeepLTranslationTo2Run = this.to2CheckBox.Checked;

            if (this.fromComboBox.SelectedIndex >= 0) {
                Properties.Settings.Default.DeepLTranslationFrom = this.Languages[this.fromComboBox.SelectedIndex].Item1;
            } else {
                Properties.Settings.Default.DeepLTranslationFrom = "";
            }

            if (this.to1ComboBox.SelectedIndex >= 0) {
                Properties.Settings.Default.DeepLTranslationTo1 = this.Languages[this.to1ComboBox.SelectedIndex].Item1;
            } else {
                Properties.Settings.Default.DeepLTranslationTo1 = "";
            }

            if (this.to2ComboBox.SelectedIndex >= 0) {
                Properties.Settings.Default.DeepLTranslationTo2 = this.Languages[this.to2ComboBox.SelectedIndex].Item1;
            } else {
                Properties.Settings.Default.DeepLTranslationTo2 = "";
            }
        }

        public DeepLTranslationPage(Service.ServiceManager serviceManager) : base (serviceManager) {
            this.service = new DeepLTranslationService(serviceManager);

            InitializeComponent();
        }


        private void DeepLTranslationPage_Load(object sender, EventArgs e) {
            foreach (var lang in Languages) {
                this.fromComboBox.Items.Add(lang.Item2);
                this.to1ComboBox.Items.Add(lang.Item2);
                this.to2ComboBox.Items.Add(lang.Item2);
            }
        }

        private void keyTextBox_TextChanged(object sender, EventArgs e) {
            this.service.Key = keyTextBox.Text;
        }

        private void fromComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            this.service.From = this.Languages[fromComboBox.SelectedIndex].Item1;
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
                this.service.To1 = this.Languages[to1ComboBox.SelectedIndex].Item1;
            } else {
                this.service.To1 = null;
            }
        }

        private void SetTo2() {
            if (to2CheckBox.Checked && to2ComboBox.SelectedIndex != -1) {
                this.service.To2 = this.Languages[to2ComboBox.SelectedIndex].Item1;
            } else {
                this.service.To2 = null;
            }
        }

        Tuple<string, string>[] Languages = {
            new Tuple<string, string>("bg", "Bulgarian"),
            new Tuple<string, string>("cs", "Czech"),
            new Tuple<string, string>("da", "Danish"),
            new Tuple<string, string>("de", "German"),
            new Tuple<string, string>("el", "Greek"),
            new Tuple<string, string>("en", "English"),
            new Tuple<string, string>("en-GB", "English (British)"),
            new Tuple<string, string>("en-US", "English (American)"),
            new Tuple<string, string>("es", "Spanish"),
            new Tuple<string, string>("et", "Estonian"),
            new Tuple<string, string>("fi", "Finnish"),
            new Tuple<string, string>("fr", "French"),
            new Tuple<string, string>("hu", "Hungarian"),
            new Tuple<string, string>("id", "Indonesian"),
            new Tuple<string, string>("it", "Italian"),
            new Tuple<string, string>("ja", "Japanese"),
            new Tuple<string, string>("lt", "Lithuanian"),
            new Tuple<string, string>("lv", "Latvian"),
            new Tuple<string, string>("nl", "Dutch"),
            new Tuple<string, string>("pl", "Polish"),
            new Tuple<string, string>("pt", "Portuguese"),
            new Tuple<string, string>("pt-BR", "Portuguese (Brazilian)"),
            new Tuple<string, string>("pt-PT", "Portuguese (European)"),
            new Tuple<string, string>("ro", "Romanian"),
            new Tuple<string, string>("ru", "Russian"),
            new Tuple<string, string>("sk", "Slovak"),
            new Tuple<string, string>("sl", "Slovenian"),
            new Tuple<string, string>("sv", "Swedish"),
            new Tuple<string, string>("tr", "Turkish"),
            new Tuple<string, string>("zh", "Chinese"),
        };
    }
}
