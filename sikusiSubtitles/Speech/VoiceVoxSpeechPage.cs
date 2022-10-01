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

namespace sikusiSubtitles.Speech {
    public partial class VoiceVoxSpeechPage : UserControl {
        ServiceManager serviceManager;
        VoiceVoxSpeechService service;

        public VoiceVoxSpeechPage(ServiceManager serviceManager, VoiceVoxSpeechService service) {
            this.serviceManager = serviceManager;
            this.service = service;

            InitializeComponent();
        }

        private async void VoiceVoxSpeechPage_Load(object sender, EventArgs e) {
            await Task.Run(() => {
                while (service.VoiceListInitialized == false) {
                    Thread.Sleep(100);
                }
            });
            voiceComboBox.Items.Clear();
            service.GetVoices().ForEach(voice => this.voiceComboBox.Items.Add(voice.Item2));
        }

        private async void refreshButton_Click(object sender, EventArgs e) {
            await VoiceRefresh(true);
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

        private async Task VoiceRefresh(bool refreshService = false) {
            await service.GetSpeakers();
            voiceComboBox.Items.Clear();
            service.GetVoices().ForEach(voice => this.voiceComboBox.Items.Add(voice.Item2));
        }
    }
}
