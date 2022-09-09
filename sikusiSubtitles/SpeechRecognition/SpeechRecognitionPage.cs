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
        private SpeechRecognitionServiceManager service;

        /** 音声認識サービス一覧 */
        private List<SpeechRecognitionService> services = new List<SpeechRecognitionService>();

        /** マイク一覧 */
        private MMDeviceCollection micList;

        private List<Tuple<string, string>> languages = new List<Tuple<string, string>>();

        public SpeechRecognitionPage(Service.ServiceManager serviceManager) : base(serviceManager) {
            this.service = new SpeechRecognitionServiceManager(serviceManager);

            var enumerator = new MMDeviceEnumerator();
            this.micList = enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active);

            InitializeComponent();
        }

        /**
         * フォームがロードされた
         */
        private void SpeechRecognitionPage_Load(object sender, EventArgs e) {
            //　マイク
            foreach (var mic in this.micList) {
                var i = this.micComboBox.Items.Add(mic.FriendlyName);
                if (mic.ID == service.Device?.ID) this.micComboBox.SelectedIndex = i;
            }

            // 音声認識エンジン
            services = this.serviceManager.GetServices<SpeechRecognitionService>();
            services.ForEach(service => {
                var i = serviceComboBox.Items.Add(service.DisplayName);
                if (service.Name == this.service.Engine) serviceComboBox.SelectedIndex = i;
            });
        }

        /**
         * 使用するマイクが変更された
         */
        private void micComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.micComboBox.SelectedIndex != -1) {
                this.service.Device = this.micList[this.micComboBox.SelectedIndex];
            }
        }

        /**
         * 使用する音声認識サービスが変更された
         */
        private void serviceComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.serviceComboBox.SelectedIndex != -1) {
                var service = this.services[this.serviceComboBox.SelectedIndex];
                this.service.Engine = service.Name;

                languages = service.GetLanguages();
                LanguageComboBox.Items.Clear();
                languages.ForEach(lang => {
                    var i = LanguageComboBox.Items.Add(lang.Item2);
                    if (lang.Item1 == this.service.Language) LanguageComboBox.SelectedIndex = i;
                });

            }
        }

        private void LanguageComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.LanguageComboBox.SelectedIndex != -1) {
                service.Language = languages[LanguageComboBox.SelectedIndex].Item1;
            }
        }
    }
}
