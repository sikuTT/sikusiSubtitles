using NAudio.CoreAudioApi;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
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

namespace sikusiSubtitles.SpeechRecognition {
    public partial class AmiVoiceSpeechRecognitionPage : SettingPage {
        private AmiVoiceSpeechRecognitionServie service;

        public AmiVoiceSpeechRecognitionPage(Service.ServiceManager serviceManager) : base(serviceManager) {
            this.service = new AmiVoiceSpeechRecognitionServie(serviceManager);

            InitializeComponent();
        }

        public override void Unload() {
            try {
                this.service.Stop();
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message);
            }
        }

        public override void LoadSettings() {
            this.keyTextBox.Text = this.Decrypt(Properties.Settings.Default.AmiVoiceKey);

            this.engineComboBox.SelectedIndex = 0;
            for (int i = 0; i < this.engines.Length; i++) {
                if (this.engines[i].Item1 == Properties.Settings.Default.AmiVoiceEngine) {
                    this.engineComboBox.SelectedIndex = i;
                }
            }

            this.logComboBox.SelectedIndex = 0;
            for (int i = 0; i < this.logs.Length; i++) {
                if (this.logs[i].Item1 == Properties.Settings.Default.AmiVoiceLog) {
                    this.logComboBox.SelectedIndex = i;
                }
            }
        }

        public override void SaveSettings() {
            Properties.Settings.Default.AmiVoiceKey = this.Encrypt(this.keyTextBox.Text);

            int engineIndex = this.engineComboBox.SelectedIndex > 0 ? this.engineComboBox.SelectedIndex : 0;
            Properties.Settings.Default.AmiVoiceEngine = this.engines[engineIndex].Item1;

            int logIndex = this.logComboBox.SelectedIndex > 0 ? this.logComboBox.SelectedIndex : 0;
            Properties.Settings.Default.AmiVoiceLog= this.logs[logIndex].Item1;
        }

        private void AmiVoiceSpeechRecognitionPage_Load(object sender, EventArgs e) {
            foreach (var engine in engines)
                this.engineComboBox.Items.Add(engine.Item2);
            foreach (var log in logs)
                this.logComboBox.Items.Add(log.Item2);
        }

        private void keyTextBox_TextChanged(object sender, EventArgs e) {
            this.service.Key = this.keyTextBox.Text;
        }

        private void engineComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            this.service.Engine = this.engines[this.engineComboBox.SelectedIndex].Item1;
        }

        private void logComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            this.service.SaveLog = this.logComboBox.SelectedIndex == 0;
        }

        Tuple<string, string>[] logs =  {
            new Tuple<string, string>("log", "あり"),
            new Tuple<string, string>("nolog", "なし"),
        };
        Tuple<string, string>[] engines = {
            new Tuple<string, string>("-a-general-input", "音声入力_汎用"),
            // new Tuple<string, string>("-a-medgeneral-input", "音声入力_医療"),
            // new Tuple<string, string>("-a-bizmrreport-input", "音声入力_製薬"),
            // new Tuple<string, string>("-a-medkarte-input", "音声入力_電子カルテ"),
            // new Tuple<string, string>("-a-bizinsurance-input", "音声入力_保険"),
            // new Tuple<string, string>("-a-bizfinance-input", "音声入力_金融"),
            new Tuple<string, string>("-a-general", "会話_汎用"),
            // new Tuple<string, string>("-a-medgeneral", "会話_医療"),
            // new Tuple<string, string>("-a-bizmrreport", "会話_製薬"),
            // new Tuple<string, string>("-a-bizfinance", "会話_金融"),
            // new Tuple<string, string>("-a-bizinsurance", "会話_保険"),
            new Tuple<string, string>("-a-general-en", "英語_汎用"),
            new Tuple<string, string>("-a-general-zh", "中国語_汎用"),
        };
    }
}
