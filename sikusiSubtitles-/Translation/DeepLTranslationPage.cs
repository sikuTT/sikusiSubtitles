using DeepL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sikusiSubtitles.Translation {
    public partial class DeepLTranslationPage : UserControl {
        private ServiceManager serviceManager;
        private DeepLTranslationService service;

        public DeepLTranslationPage(ServiceManager serviceManager, DeepLTranslationService service) {
            this.serviceManager = serviceManager;
            this.service = service;

            InitializeComponent();
        }

        private void DeepLTranslationPage_Load(object sender, EventArgs e) {
            keyTextBox.Text = service.Key;
        }

        private void keyTextBox_TextChanged(object sender, EventArgs e) {
            this.service.Key = keyTextBox.Text;
        }
    }
}
