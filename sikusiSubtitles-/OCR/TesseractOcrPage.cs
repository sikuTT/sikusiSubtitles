using sikusiSubtitles.Translation;
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

namespace sikusiSubtitles.OCR {
    public partial class TesseractOcrPage : UserControl {
        ServiceManager serviceManager;
        TesseractOcrService service;

        public TesseractOcrPage(ServiceManager serviceManager, TesseractOcrService service) {
            this.serviceManager = serviceManager;
            this.service = service;

            InitializeComponent();
        }

        private void TesseractOcrPage_Load(object sender, EventArgs e) {
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            ProcessStartInfo pi = new ProcessStartInfo() {
                FileName = "https://github.com/tesseract-ocr/tessdata",
                UseShellExecute = true,
            };
            System.Diagnostics.Process.Start(pi);
        }
    }
}
