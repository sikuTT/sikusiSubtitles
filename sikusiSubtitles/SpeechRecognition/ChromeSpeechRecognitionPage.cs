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
        private ChromeSpeechRecognitionService service;

        public ChromeSpeechRecognitionPage(ServiceManager serviceManager) : base(serviceManager) {
            this.service = new ChromeSpeechRecognitionService(serviceManager);

            InitializeComponent();
        }

        public override void Unload() {
            try {
                service.Stop();
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message);
            }
        }

        private void ChromeSpeechRecognitionPage_Load(object sender, EventArgs e) {
            portNumericUpDown.Value = service.Port;
        }

        private void portNumericUpDown_ValueChanged(object sender, EventArgs e) {
            service.Port = (int)portNumericUpDown.Value;
        }
    }
}
