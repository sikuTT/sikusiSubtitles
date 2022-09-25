using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sikusiSubtitles.Speech {
    public partial class SystemSpeechPage : UserControl {
        ServiceManager serviceManager;
        SystemSpeechService service;

        public SystemSpeechPage(ServiceManager serviceManager, SystemSpeechService service) {
            this.serviceManager = serviceManager;
            this.service = service;

            InitializeComponent();

            service.GetVoices().ForEach(voice => {
                this.voiceComboBox.Items.Add(voice.Item2);
            });
        }

        private void voiceComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            speakButton.Enabled = voiceComboBox.SelectedIndex != -1;
        }

        private async void speakButton_Click(object sender, EventArgs e) {
            try {
                speakButton.Visible = false;
                cancelButton.Visible = true;

                var voices = this.service.GetVoices();
                var voice = voices[voiceComboBox.SelectedIndex];
                await service.SpeakAsync(voice.Item1, textBox1.Text);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            } finally {
                speakButton.Visible = true;
                cancelButton.Visible = false;
            }
        }

        private async void cancelButton_Click(object sender, EventArgs e) {
            await service.CancelSpeakAsync();
        }
    }
}
