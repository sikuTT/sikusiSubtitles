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

namespace sikusiSubtitles.Translation {
    public partial class AzureTranslationPage : SettingPage {
        public AzureTranslationService service;

        public AzureTranslationPage(Service.ServiceManager serviceManager) : base(serviceManager) {
            this.service = new AzureTranslationService(serviceManager);

            InitializeComponent();
        }

        private void AzureTranslationPage_Load(object sender, EventArgs e) {
            this.keyTextBox.Text = service.Key;
            this.regionTextBox.Text = service.Region;
        }

        private void keyTextBox_TextChanged(object sender, EventArgs e) {
            this.service.Key = this.keyTextBox.Text;
        }

        private void regionTextBox_TextChanged(object sender, EventArgs e) {
            this.service.Region = this.regionTextBox.Text;
        }
    }
}
