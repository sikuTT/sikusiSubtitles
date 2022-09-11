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
    public partial class AzureOcrPage : SettingPage {
        AzureOcrService service;

        public AzureOcrPage(Service.ServiceManager serviceManager) : base(serviceManager) {
            this.service = new AzureOcrService(serviceManager);

            InitializeComponent();
        }

        private void AzureOcrPage_Load(object sender, EventArgs e) {
            keyTextBox.Text = service.Key;
        }

        private void keyTextBox_TextChanged(object sender, EventArgs e) {
            service.Key = keyTextBox.Text;
        }
    }
}
