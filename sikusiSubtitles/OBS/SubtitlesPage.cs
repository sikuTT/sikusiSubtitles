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
        /**
         * 音声字幕のソース名
         */
        public string Voice {
            get { return this.voiceTextBox.Text; }
        }

        /**
         * 翻訳１のソース名
         */
        public string Translation1 {
            get { return this.translation1TextBox.Text; }
        }

        /**
         * 翻訳２のソース名
         */
        public string Translation2 {
            get { return this.translation2TextBox.Text; }
        }

        public int? ClearInterval {
            get {
                if (this.clearCheckBox.Checked) {
                    return (int)this.clearIntervalNumericUpDown.Value;
                } else {
                    return null;
                }
            }
        }

        public int? AdditionalTime {
            get {
                if (this.additionalCheckBox.Checked) {
                    return this.additionalTrackBar.Value;
                } else {
                    return null;
                }
            }
        }

        public SubtitlesPage() {
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
    }
}
