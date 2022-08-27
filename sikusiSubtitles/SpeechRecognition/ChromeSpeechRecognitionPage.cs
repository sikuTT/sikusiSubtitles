using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using sikusiSubtitles.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sikusiSubtitles.SpeechRecognition {
    public partial class ChromeSpeechRecognitionPage : SettingPage {
        private ChromeSpeechRecognitionService service = new ChromeSpeechRecognitionService();

        public ChromeSpeechRecognitionPage(ServiceManager serviceManager) : base(serviceManager) {
            InitializeComponent();
            serviceManager.AddService(service);
        }

        public override void LoadSettings() {
            this.portNumericUpDown.Value = Properties.Settings.Default.ChromePort;
            this.service.Port = (int)this.portNumericUpDown.Value;

            for (var i = 0; i < this.Languages.Length; i++) {
                if (Properties.Settings.Default.ChromeLanguage == this.Languages[i].Item1) {
                    this.languageComboBox.SelectedIndex = i;
                    this.service.Language = this.Languages[i].Item1;
                    break;
                }
            }
        }

        public override void SaveSettings() {
            Properties.Settings.Default.ChromePort = (int)this.portNumericUpDown.Value;
            if (this.languageComboBox.SelectedIndex != -1)
                Properties.Settings.Default.ChromeLanguage = this.Languages[this.languageComboBox.SelectedIndex].Item1;
        }

        public override void Unload() {
            try {
                service.Stop();
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message);
            }
        }

        private void ChromeSpeechRecognitionPage_Load(object sender, EventArgs e) {
            Array.Sort(this.Languages, (a, b) => a.Item2.CompareTo(b.Item2));
            foreach (var language in this.Languages) {
                this.languageComboBox.Items.Add(language.Item2);
            }
        }

        private void portNumericUpDown_ValueChanged(object sender, EventArgs e) {
            this.service.Port = (int)this.portNumericUpDown.Value;
        }

        private void languageComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            var i = languageComboBox.SelectedIndex;
            this.service.Language = this.Languages[i].Item1;
        }

        Tuple<string, string>[] Languages = {
            new Tuple<string, string>("ja-JP", "日本語"),
            new Tuple<string, string>("en-US", "英語"),
            new Tuple<string, string>("es-ES", "スペイン語（スペイン）"),
            new Tuple<string, string>("pt-P", "ポルトガル語（ポルトガル）"),
            new Tuple<string, string>("pt-BR", "ポルトガル語（ブラジル）"),
            new Tuple<string, string>("ko-KR", "韓国語"),
            new Tuple<string, string>("zh", "中国語（簡体字、中国本土）"),
            new Tuple<string, string>("cmn-Hant-TW", "中国語（繁体字、台湾）"),
        };
    }
}
