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
    public partial class ChromeSpeechRecognitionPage : UserControl {
        private ServiceManager serviceManager;
        private ChromeSpeechRecognitionService service;

        public ChromeSpeechRecognitionPage(ServiceManager serviceManager, ChromeSpeechRecognitionService service) {
            this.serviceManager = serviceManager;
            this.service = service;

            InitializeComponent();
        }

        private void ChromeSpeechRecognitionPage_Load(object sender, EventArgs e) {
            httpServerPortUpDown.Value = service.HttpServerPort;
            webSocketServerPortUpDown.Value = service.WebSocketPort;
        }

        private void httpServerPortUpDown_ValueChanged(object sender, EventArgs e) {
            service.HttpServerPort = (int)httpServerPortUpDown.Value;
        }

        private void webSocketServerPortUpDown_ValueChanged(object sender, EventArgs e) {
            service.WebSocketPort = (int)webSocketServerPortUpDown.Value;
        }
    }
}
