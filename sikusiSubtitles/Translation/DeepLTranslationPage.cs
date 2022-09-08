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
    public partial class DeepLTranslationPage : SettingPage {
        private DeepLTranslationService service;
        private List<Tuple<string, string>> languages;

        public DeepLTranslationPage(Service.ServiceManager serviceManager) : base (serviceManager) {
            this.service = new DeepLTranslationService(serviceManager);

            InitializeComponent();
        }

        private void keyTextBox_TextChanged(object sender, EventArgs e) {
            this.service.Key = keyTextBox.Text;
        }
    }
}
