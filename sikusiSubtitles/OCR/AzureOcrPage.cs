using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sikusiSubtitles.OCR {
    public partial class AzureOcrPage : UserControl {
        ServiceManager serviceManager;
        AzureOcrService service;

        public AzureOcrPage(ServiceManager serviceManager, AzureOcrService service) {
            this.serviceManager = serviceManager;
            this.service = service;

            InitializeComponent();
        }

        private void AzureOcrPage_Load(object sender, EventArgs e) {
            keyTextBox.Text = service.Key;
            endpointTextBox.Text = service.Endpoint;
        }

        private void keyTextBox_TextChanged(object sender, EventArgs e) {
            service.Key = keyTextBox.Text;
        }

        private void endpointTextBox_TextChanged(object sender, EventArgs e) {
            service.Endpoint = endpointTextBox.Text;
        }
    }
}
