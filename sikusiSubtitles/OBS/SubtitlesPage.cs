using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sikusiSubtitles.OBS {
    public partial class SubtitlesPage : SettingPage {
        private SubtitlesService service;

        public SubtitlesPage(Service.ServiceManager serviceManager) : base(serviceManager) {
            service = new SubtitlesService(serviceManager);

            InitializeComponent();
        }

        public override void LoadSettings() {
            this.voiceTextBox.Text = Properties.Settings.Default.SubtitlesVoice;
            this.translation1TextBox.Text = Properties.Settings.Default.SubtitlesTranslation1;
            this.translation2TextBox.Text = Properties.Settings.Default.SubtitlesTranslation2;
            this.clearCheckBox.Checked = Properties.Settings.Default.SubtitlesClear;
            this.clearIntervalNumericUpDown.Value = Properties.Settings.Default.SubtitlesClearInterval;
            this.additionalCheckBox.Checked = Properties.Settings.Default.SubtitlesAddtional;
            this.additionalTrackBar.Value = Properties.Settings.Default.SubtitlesAdditionalTime;
        }

        public override void SaveSettings() {
            Properties.Settings.Default.SubtitlesVoice = this.voiceTextBox.Text;
            Properties.Settings.Default.SubtitlesTranslation1 = this.translation1TextBox.Text;
            Properties.Settings.Default.SubtitlesTranslation2 = this.translation2TextBox.Text;
            Properties.Settings.Default.SubtitlesClear = this.clearCheckBox.Checked;
            Properties.Settings.Default.SubtitlesClearInterval = (int)this.clearIntervalNumericUpDown.Value;
            Properties.Settings.Default.SubtitlesAddtional = this.additionalCheckBox.Checked;
            Properties.Settings.Default.SubtitlesAdditionalTime = this.additionalTrackBar.Value;
        }

        private void voiceTextBox_TextChanged(object sender, EventArgs e) {
            this.service.VoiceTarget = voiceTextBox.Text;
        }

        private void translation1TextBox_TextChanged(object sender, EventArgs e) {
            this.service.Translation1Target = translation1TextBox.Text;
        }

        private void translation2TextBox_TextChanged(object sender, EventArgs e) {
            this.service.Translation2Target = translation2TextBox.Text;
        }

        private void clearCheckBox_CheckedChanged(object sender, EventArgs e) {
            if (this.clearCheckBox.Checked) {
                this.service.ClearInterval = (int)this.clearIntervalNumericUpDown.Value;
            } else {
                this.service.ClearInterval = null;
            }
        }

        private void clearIntervalNumericUpDown_ValueChanged(object sender, EventArgs e) {
            this.service.ClearInterval = (int)this.clearIntervalNumericUpDown.Value;
        }

        private void additionalCheckBox_CheckedChanged(object sender, EventArgs e) {
            if (this.additionalCheckBox.Checked) {
                this.service.AdditionalTime = this.additionalTrackBar.Value;
            } else {
                this.service.AdditionalTime = null;
            }
        }

        private void additionalTrackBar_Scroll(object sender, EventArgs e) {
            this.service.AdditionalTime = this.additionalTrackBar.Value;
        }
    }
}
