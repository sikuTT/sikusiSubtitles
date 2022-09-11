using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sikusiSubtitles.Translation {
    public partial class GoogleAppsScriptTranslationPage : SettingPage {
        GoogleAppsScriptTranslationService service;
        List<Tuple<string, string>> languages = new GoogleTranslationLanguages().Languages;

        public GoogleAppsScriptTranslationPage(ServiceManager serviceManager, GoogleAppsScriptTranslationService service) : base(serviceManager) {
            this.service = service;

            InitializeComponent();
        }

        private void GoogleAppsScriptTranslationPage_Load(object sender, EventArgs e) {
            keyTextBox.Text = service.Key;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            ProcessStartInfo pi = new ProcessStartInfo() {
                FileName = "https://script.google.com/home",
                UseShellExecute = true,
            };
            System.Diagnostics.Process.Start(pi);
        }

        private void keyTextBox_TextChanged(object sender, EventArgs e) {
            this.service.Key = keyTextBox.Text;
        }
    }
}
