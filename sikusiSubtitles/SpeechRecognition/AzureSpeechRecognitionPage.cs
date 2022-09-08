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
         * 設定の読み込み
         */
        public override void LoadSettings() {
        }

        private void AzureSpeechRecognition_Load(object sender, EventArgs e) {
        }

        private void keyTextBox_TextChanged(object sender, EventArgs e) {
            this.service.Key = keyTextBox.Text;
        }

        private void regionTextBox_TextChanged(object sender, EventArgs e) {
            this.service.Region = regionTextBox.Text;
        }
    }
}
