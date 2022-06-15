using NAudio.CoreAudioApi;
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

namespace sikusiSubtitles.SpeechRecognition {
    public partial class SpeechRecognitionPage : SettingPage {
        /** 音声認識サービスの一覧 */
        public enum ServiceType {
            None,
            Chrome,
            Azure,
            AmiVoice,
        }

        /** マイク一覧 */
        private MMDeviceCollection MicList;

        /**
         * 選択されているマイク
         */
        public MMDevice? Mic {
            get {
                if (this.micComboBox.SelectedIndex < 0) {
                    return null;
                } else {
                    return this.MicList[this.micComboBox.SelectedIndex];
                }
            }
        }

        /**
         * 選択されている音声認識サービス
         */
        public ServiceType Service {
            get {
                if (this.chromeSpeechRecognitionRadioButton.Checked) {
                    return ServiceType.Chrome;
                }  else if (this.azureSpeechRecognitionRadioButton.Checked) {
                    return ServiceType.Azure;
                } else if (this.amiVoiceSpeechRecognitionRadioButton.Checked) {
                    return ServiceType.AmiVoice;
                } else {
                    return ServiceType.None;
                }
            }
        }

        public SpeechRecognitionPage() {
            var enumerator = new MMDeviceEnumerator();
            this.MicList = enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active);

            InitializeComponent();
        }

        /**
         * 設定を保存する
         */
        public override void SaveSettings() {
            Properties.Settings.Default.MicID = Mic != null ? Mic.ID : "";
            Properties.Settings.Default.RecognitionEngine = Service.ToString();
        }

        /**
         * 設定をロードする
         */
        public override void LoadSettings() {
            // マイク設定
            var enumerator = new MMDeviceEnumerator();
            MMDevice defaultDevice = enumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Console);
            bool setMic = false;
            for (int i = 0; i < this.MicList.Count; ++i) {
                var endpoint = this.MicList[i];
                if (setMic == false && endpoint.ID == defaultDevice.ID) {
                    this.micComboBox.SelectedIndex = i;
                } else if (endpoint.ID == Properties.Settings.Default.MicID) {
                    this.micComboBox.SelectedIndex = i;
                    setMic = true;
                }
            }

            // 音声認識エンジン
            this.chromeSpeechRecognitionRadioButton.Checked = Properties.Settings.Default.RecognitionEngine == ServiceType.Chrome.ToString();
            this.azureSpeechRecognitionRadioButton.Checked = Properties.Settings.Default.RecognitionEngine == ServiceType.Azure.ToString();
            this.amiVoiceSpeechRecognitionRadioButton.Checked = Properties.Settings.Default.RecognitionEngine == ServiceType.AmiVoice.ToString();
        }

        private void SpeechRecognitionPage_Load(object sender, EventArgs e) {
            this.micComboBox.Items.Clear();
            foreach (var endpoint in this.MicList) {
                this.micComboBox.Items.Add(endpoint.FriendlyName);
            }
        }
    }
}
