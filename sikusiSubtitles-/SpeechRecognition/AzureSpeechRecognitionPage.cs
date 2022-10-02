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
    public partial class AzureSpeechRecognitionPage : UserControl {
        ServiceManager serviceManager;
        AzureSpeechRecognitionService service;

        public AzureSpeechRecognitionPage(ServiceManager serviceManager, AzureSpeechRecognitionService service) {
            this.serviceManager = serviceManager;
            this.service = service;

            InitializeComponent();
        }

        private void AzureSpeechRecognition_Load(object sender, EventArgs e) {
            keyTextBox.Text = service.Key;
            regionTextBox.Text = service.Region;
        }

        private void keyTextBox_TextChanged(object sender, EventArgs e) {
            this.service.Key = keyTextBox.Text;
        }

        private void regionTextBox_TextChanged(object sender, EventArgs e) {
            this.service.Region = regionTextBox.Text;
        }
    }
}
