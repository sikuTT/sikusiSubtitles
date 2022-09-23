using Google.Apis.Services;
using Google.Apis.Translate.v2;
using Google.Apis.Translate.v2.Data;
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

namespace sikusiSubtitles.Translation {
    public partial class GoogleBasicTranslationPage : UserControl {
        private ServiceManager serviceManager;
        private GoogleBasicTranslationService service;

        public GoogleBasicTranslationPage(ServiceManager serviceManager, GoogleBasicTranslationService service) {
            this.serviceManager = serviceManager;
            this.service = service;

            InitializeComponent();
        }

        private void GoogleBasicTranslationPage_Load(object sender, EventArgs e) {
            keyTextBox.Text = service.Key;
        }

        private void keyTextBox_TextChanged(object sender, EventArgs e) {
            this.service.Key = keyTextBox.Text;
        }
    }
}
