using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using NAudio.CoreAudioApi;
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

namespace sikusiSubtitles.SpeechRecognition {
    public partial class AzureSpeechRecognitionPage : SettingPage {
        AzureSpeechRecognitionService service;

        public AzureSpeechRecognitionPage(Service.ServiceManager serviceManager) : base(serviceManager) {
            this.service = new AzureSpeechRecognitionService(serviceManager);

            InitializeComponent();
        }

        /**
         * 設定の保存
         */
        public override void SaveSettings() {
            Properties.Settings.Default.AzureSpeechRecognitionKey = this.keyTextBox.Text;
            Properties.Settings.Default.AzureSpeechRecognitionRegion = this.regionTextBox.Text;

            var index = this.languageComboBox.SelectedIndex < 0 ? 0 : this.languageComboBox.SelectedIndex;
            Properties.Settings.Default.AzureSpeechRecognitionLanguage = this.Languages[index].Item1;
        }

        /**
         * 設定の読み込み
         */
        public override void LoadSettings() {
            this.keyTextBox.Text = Properties.Settings.Default.AzureSpeechRecognitionKey;
            this.regionTextBox.Text = Properties.Settings.Default.AzureSpeechRecognitionRegion;

            var index = Array.FindIndex(this.Languages, (lang) => lang.Item1 == Properties.Settings.Default.AzureSpeechRecognitionLanguage);
            index = Math.Max(index, 0);
            this.languageComboBox.SelectedIndex = index;
        }

        private void AzureSpeechRecognition_Load(object sender, EventArgs e) {
            foreach (var language in Languages) {
                this.languageComboBox.Items.Add(language.Item2);
            }
        }

        private void keyTextBox_TextChanged(object sender, EventArgs e) {
            this.service.Key = keyTextBox.Text;
        }

        private void regionTextBox_TextChanged(object sender, EventArgs e) {
            this.service.Region = regionTextBox.Text;
        }

        private void languageComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            this.service.Language = this.Languages[languageComboBox.SelectedIndex].Item1;
        }

        Tuple<string, string>[] Languages = {
            // new Tuple<string, string>("", "Arabic (Bahrain) modern standard"),
            // new Tuple<string, string>("", "Arabic (Egypt)"),
            // new Tuple<string, string>("", "Arabic (Kuwait)"),
            // new Tuple<string, string>("", "Arabic (Qatar)"),
            // new Tuple<string, string>("", "Arabic (Saudi Arabia)"),
            // new Tuple<string, string>("", "Arabic (Syria)"),
            // new Tuple<string, string>("", "Arabic (UAE)"),
            // new Tuple<string, string>("", "Catalan"),
            // new Tuple<string, string>("", "Chinese (Cantonese Traditional)"),
            new Tuple<string, string>("zh-CN", "Chinese (Mandarin simplified)"),
            new Tuple<string, string>("zh-TW", "Chinese (Taiwanese Mandarin)"),
            // new Tuple<string, string>("", "Danish (Denmark)"),
            // new Tuple<string, string>("", "Dutch (Netherlands)"),
            // new Tuple<string, string>("", "English (Australia)"),
            // new Tuple<string, string>("", "English (Canada)"),
            // new Tuple<string, string>("", "English (India)"),
            // new Tuple<string, string>("", "English (New Zealand)"),
            // new Tuple<string, string>("", "English (United Kingdom)"),
            new Tuple<string, string>("en-US", "English (United States)"),
            // new Tuple<string, string>("", "Finnish (Finland)"),
            // new Tuple<string, string>("", "French (Canada)"),
            // new Tuple<string, string>("", "French (France)"),
            // new Tuple<string, string>("", "German (Germany)"),
            // new Tuple<string, string>("", "Gujarati (Indian)"),
            // new Tuple<string, string>("", "Hindi (India)"),
            // new Tuple<string, string>("", "Italian (Italy)"),
            new Tuple<string, string>("ja-JP", "Japanese (Japan)"),
            new Tuple<string, string>("ko-KR", "Korean (Korea)"),
            // new Tuple<string, string>("", "Marathi (India)"),
            // new Tuple<string, string>("", "Norwegian (Bokmål) (Norway)"),
            // new Tuple<string, string>("", "Polish (Poland)"),
            new Tuple<string, string>("pt-BR", "Portuguese (Brazil)"),
            new Tuple<string, string>("pt-PT", "Portuguese (Portugal)"),
            // new Tuple<string, string>("", "Russian (Russia)"),
            // new Tuple<string, string>("", "Spanish (Mexico)"),
            new Tuple<string, string>("es-ES", "Spanish (Spain)"),
            // new Tuple<string, string>("", "Swedish (Sweden)"),
            // new Tuple<string, string>("", "Tamil (India)"),
            // new Tuple<string, string>("", "Telugu (India)"),
            // new Tuple<string, string>("", "Thai (Thailand)"),
            // new Tuple<string, string>("", "Turkish (Turkey)"),
        };
    }
}
