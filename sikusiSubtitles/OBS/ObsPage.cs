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

namespace sikusiSubtitles.OBS {
    public partial class ObsPage : SettingPage {
        private ObsService service;

        public ObsPage(Service.ServiceManager serviceManager) : base(serviceManager) {
            this.service = new ObsService(serviceManager);

            InitializeComponent();
        }

        public override void LoadSettings() {
            this.ipTextBox.Text = Properties.Settings.Default.ObsIP;
            this.service.IP = Properties.Settings.Default.ObsIP;

            this.portNumericUpDown.Value = Properties.Settings.Default.ObsPort;
            this.service.Port = Properties.Settings.Default.ObsPort;

            this.passwordTextBox.Text = Properties.Settings.Default.ObsPassword;
            this.service.Password = Properties.Settings.Default.ObsPassword;
        }

        public override void SaveSettings() {
            Properties.Settings.Default.ObsIP = this.ipTextBox.Text;
            Properties.Settings.Default.ObsPort = (int)this.portNumericUpDown.Value;
            Properties.Settings.Default.ObsPassword = this.passwordTextBox.Text;
        }
    }
}
