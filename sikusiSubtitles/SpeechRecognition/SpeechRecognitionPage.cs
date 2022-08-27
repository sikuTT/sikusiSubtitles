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
        /** 音声認識サービス一覧 */
        private List<SpeechRecognitionService> services = new List<SpeechRecognitionService>();

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

        public SpeechRecognitionPage(Service.ServiceManager serviceManager) : base(serviceManager) {
            var enumerator = new MMDeviceEnumerator();
            this.micList = enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active);

            InitializeComponent();
        }

        /**
         * 設定を保存する
         */
        public override void SaveSettings() {
            var service = this.serviceManager.GetActiveService<SpeechRecognitionService>();
            Properties.Settings.Default.MicID = Mic != null ? Mic.ID : "";
            Properties.Settings.Default.RecognitionEngine = service != null ? service.DisplayName : "";
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
            for (int i = 0; i < this.services.Count; ++i) {
                if (Properties.Settings.Default.RecognitionEngine == this.services[i].Name) {
                    this.serviceComboBox.SelectedIndex = i;
                    break;
                }
            }
        }

        /**
         * フォームがロードされた
         */
        private void SpeechRecognitionPage_Load(object sender, EventArgs e) {
            foreach (var mic in this.micList) {
                this.micComboBox.Items.Add(mic.FriendlyName);
            }

            this.services = this.serviceManager.GetServices<SpeechRecognitionService>();
            foreach (var service in this.services) {
                this.serviceComboBox.Items.Add(service.DisplayName);
            }
        }

        /**
         * 使用する音声認識サービスが変更された
         */
        private void serviceComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            var service = this.services[this.serviceComboBox.SelectedIndex];
            this.serviceManager.SetActiveService(service);
        }
    }
}
