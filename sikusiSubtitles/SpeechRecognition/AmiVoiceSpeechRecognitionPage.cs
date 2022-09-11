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
        private List<Tuple<string, string>> engines = new List<Tuple<string, string>>();

        public AmiVoiceSpeechRecognitionPage(ServiceManager serviceManager, AmiVoiceSpeechRecognitionServie service) : base(serviceManager) {
            this.service = service;

            InitializeComponent();
        }

        public override void Unload() {
            try {
                this.service.Stop();
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message);
            }
        }

        private void AmiVoiceSpeechRecognitionPage_Load(object sender, EventArgs e) {
            this.keyTextBox.Text = service.Key;

            // ログ
            logs.ForEach(log => {
                var i = this.logComboBox.Items.Add(log.Item2);
                if (log.Item1 == service.Log) {
                    this.logComboBox.SelectedIndex = i;
                }
            });
        }

        private void keyTextBox_TextChanged(object sender, EventArgs e) {
            this.service.Key = this.keyTextBox.Text;
        }

        private void logComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            this.service.Log = logs[logComboBox.SelectedIndex].Item1;
        }

        readonly List<Tuple<bool, string>> logs = new List<Tuple<bool, string>>() {
            new Tuple<bool, string>(true, "あり"),
            new Tuple<bool, string>(false, "なし"),
        };
    }
}
