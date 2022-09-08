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
    public partial class TranslationPage : SettingPage {
        private TranslationServiceManager service;

        public TranslationPage(Service.ServiceManager serviceManager) : base(serviceManager) {
            this.service = new TranslationServiceManager(serviceManager);
            InitializeComponent();
        }

        private void TranslationPage_Load(object sender, EventArgs e) {
        }
    }
}
