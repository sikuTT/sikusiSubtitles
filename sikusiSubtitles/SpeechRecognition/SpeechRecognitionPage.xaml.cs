using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace sikusiSubtitles.SpeechRecognition {
    /// <summary>
    /// SpeechRecognitionPage.xaml の相互作用ロジック
    /// </summary>
    public partial class SpeechRecognitionPage : UserControl {
        ServiceManager serviceManager;
        SpeechRecognitionServiceManager speechRecognitionServiceManager;

        /** 音声認識サービス一覧 */
        List<SpeechRecognitionService> services = new List<SpeechRecognitionService>();

        /** 選択されている音声認識サービスがサポートする言語一覧 */
        List<Tuple<string, string>> languages = new List<Tuple<string, string>>();

        /** マイク一覧 */
        MMDeviceCollection micList;

        public SpeechRecognitionPage(ServiceManager serviceManager, SpeechRecognitionServiceManager speechRecognitionServiceManager) {
            this.serviceManager = serviceManager;
            this.speechRecognitionServiceManager = speechRecognitionServiceManager;

            InitializeComponent();

            // マイク一覧
            var enumerator = new MMDeviceEnumerator();
            micList = enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active);
            
            InitializeUI();
        }
        private void InitializeUI() {
            // マイク一覧
            foreach (var mic in this.micList) {
                var i = this.micComboBox.Items.Add(mic.FriendlyName);
                if (mic.ID == speechRecognitionServiceManager.Device?.ID) this.micComboBox.SelectedIndex = i;
            }

            // 音声認識サービス一覧
            services = this.serviceManager.GetServices<SpeechRecognitionService>();
            services.ForEach(service => {
                var i = speechRecognitionServiceComboBox.Items.Add(service.DisplayName);
                if (service.Name == speechRecognitionServiceManager.Engine) speechRecognitionServiceComboBox.SelectedIndex = i;
            });
        }

        /** 使用するマイクが変更された */
        private void micComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }

        /** 使用する音声認識サービスが変更された */
        private void speechRecognitionServiceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            languageComboBox.Items.Clear();
            if (speechRecognitionServiceComboBox.SelectedIndex != -1) {
                var service = this.services[this.speechRecognitionServiceComboBox.SelectedIndex];
                speechRecognitionServiceManager.Engine = service.Name;

                languages = service.GetLanguages();
                foreach (var lang in languages) {
                    var i = languageComboBox.Items.Add(lang.Item2);
                    if (lang.Item1 == speechRecognitionServiceManager.Language) languageComboBox.SelectedIndex = i;
                }
            }
        }

        /** 認識する言語が変更された */
        private void languageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (languageComboBox.SelectedIndex != -1) {

                // speechRecognitionServiceManager.Language =
            }
        }
    }
}
