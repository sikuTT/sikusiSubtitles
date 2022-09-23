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
    public partial class AzureTranslationPage : UserControl {
        private ServiceManager serviceManager;
        private AzureTranslationService service;

        public AzureTranslationPage(ServiceManager serviceManager, AzureTranslationService service) {
            this.serviceManager = serviceManager;
            this.service = service;

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
