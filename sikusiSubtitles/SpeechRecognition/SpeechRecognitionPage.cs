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
        /** Service Manager */
        private SpeechRecognitionServiceManager serviceManager = new SpeechRecognitionServiceManager();

        /** マイク一覧 */
        private MMDeviceCollection micList;

        /**
         * 選択されているマイク
         */
        public MMDevice? Mic {
            get {
                if (this.micComboBox.SelectedIndex < 0) {
                    return null;
                } else {
                    return this.micList[this.micComboBox.SelectedIndex];
                }
            }
        }

        public SpeechRecognitionPage(Service.ServiceManager serviceManager) {
            serviceManager.AddServiceManager(this.serviceManager);

            var enumerator = new MMDeviceEnumerator();
            this.micList = enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active);

            InitializeComponent();
        }

        /**
         * 設定を保存する
         */
        public override void SaveSettings() {
            Properties.Settings.Default.MicID = Mic != null ? Mic.ID : "";
            Properties.Settings.Default.RecognitionEngine = this.serviceManager.ActiveService != null ? this.serviceManager.ActiveService.DisplayName : "";
        }

        /**
         * 設定をロードする
         */
        public override void LoadSettings() {
            // マイク設定
            var enumerator = new MMDeviceEnumerator();
            MMDevice defaultDevice = enumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Console);
            for (int i = 0; i < this.micList.Count; ++i) {
                var mic = this.micList[i];
                if (mic.ID == defaultDevice.ID) {
                    this.micComboBox.SelectedIndex = i;
                } else if (mic.ID == Properties.Settings.Default.MicID) {
                    this.micComboBox.SelectedIndex = i;
                    break;
                }
            }

            // 音声認識エンジン
            this.serviceComboBox.SelectedIndex = 0;
            for (int i = 0; i < this.serviceManager.Services.Count; ++i) {
                var service = this.serviceManager.Services[i];
                if (Properties.Settings.Default.RecognitionEngine == service.Name) {
                    this.serviceComboBox.SelectedIndex = i;
                    break;
                }
            }
            var currentService = this.serviceManager.Services[this.serviceComboBox.SelectedIndex];
            this.serviceManager.ActiveService = currentService;
        }

        /**
         * フォームがロードされた
         */
        private void SpeechRecognitionPage_Load(object sender, EventArgs e) {
            foreach (var mic in this.micList) {
                this.micComboBox.Items.Add(mic.FriendlyName);
            }

            foreach (var service in this.serviceManager.Services) {
                this.serviceComboBox.Items.Add(service.DisplayName);
            }
        }

        /**
         * 使用する音声認識サービスが変更された
         */
        private void serviceComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            var service = this.serviceManager.Services[this.serviceComboBox.SelectedIndex];
            this.serviceManager.ActiveService = service;
        }
    }
}
