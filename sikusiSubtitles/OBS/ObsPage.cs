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
    public partial class ObsPage : UserControl {
        private ServiceManager serviceManager;
        private ObsService service;

        public ObsPage(ServiceManager serviceManager, ObsService service) {
            this.serviceManager = serviceManager;
            this.service = service;

            InitializeComponent();
        }

        private void ObsPage_Load(object sender, EventArgs e) {
            this.ipTextBox.Text = service.IP;
            portNumericUpDown.Value = service.Port;
            passwordTextBox.Text = service.Password;
        }

        private void ipTextBox_TextChanged(object sender, EventArgs e) {
            service.IP = this.ipTextBox.Text;
        }

        private void portNumericUpDown_ValueChanged(object sender, EventArgs e) {
            service.Port = (int)portNumericUpDown.Value;
        }

        private void passwordTextBox_TextChanged(object sender, EventArgs e) {
            service.Password = passwordTextBox.Text;
        }
    }
}
