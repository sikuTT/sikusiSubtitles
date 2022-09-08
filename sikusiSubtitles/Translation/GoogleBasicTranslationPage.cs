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
    public partial class GoogleBasicTranslationPage : SettingPage {
        private GoogleBasicTranslationService service;

        public GoogleBasicTranslationPage(Service.ServiceManager serviceManager) : base(serviceManager) {
            this.service = new GoogleBasicTranslationService(serviceManager);

            InitializeComponent();
        }

        private void keyTextBox_TextChanged(object sender, EventArgs e) {
            this.service.Key = keyTextBox.Text;
        }
    }
}
