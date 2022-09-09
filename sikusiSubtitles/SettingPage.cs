using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sikusiSubtitles {
    public partial class SettingPage : UserControl {
        protected Service.ServiceManager serviceManager;

        private SettingPage() {
            this.serviceManager = new Service.ServiceManager();
        }

        public SettingPage(Service.ServiceManager serviceManager) {
            this.serviceManager = serviceManager;

            InitializeComponent();
        }

        virtual public void SaveSettings() { }
        virtual public void LoadSettings() { }
        virtual public void Unload() { }


        byte[] entropy = { 4, 9, 4, 9, 152, 61, 13, 213 };
    }
}
