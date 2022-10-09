using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        List<Language> languages = new List<Language>();

        /** マイク一覧 */
        MMDeviceCollection micList;

        public SpeechRecognitionPage(ServiceManager serviceManager, SpeechRecognitionServiceManager speechRecognitionServiceManager) {
            this.serviceManager = serviceManager;
            this.speechRecognitionServiceManager = speechRecognitionServiceManager;

            InitializeComponent();

            // マイク一覧
            var enumerator = new MMDeviceEnumerator();
            micList = enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            // マイク一覧
            foreach (var mic in this.micList) {
                var i = this.micComboBox.Items.Add(mic.FriendlyName);
                if (mic.ID == speechRecognitionServiceManager.Device?.ID) this.micComboBox.SelectedIndex = i;
            }

            // 音声認識サービス一覧
            services = this.serviceManager.GetServices<SpeechRecognitionService>();
            speechRecognitionServiceComboBox.ItemsSource = services;
            speechRecognitionServiceComboBox.SelectedItem = services.Find(service => service.Name == speechRecognitionServiceManager.Engine);
        }

        /** 使用するマイクが変更された */
        private void micComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (micComboBox.SelectedIndex != -1) {
                speechRecognitionServiceManager.Device = micList[micComboBox.SelectedIndex];
            }
        }

        /** 使用する音声認識サービスが変更された */
        private void speechRecognitionServiceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var service = speechRecognitionServiceComboBox.SelectedItem as SpeechRecognitionService;
            if (service != null) {
                speechRecognitionServiceManager.Engine = service.Name;

                languages = service.GetLanguages();
                languageComboBox.ItemsSource = languages;
                languageComboBox.SelectedItem = languages.Find(lang => lang.Code == speechRecognitionServiceManager.Language);
            } else {
                languageComboBox.ItemsSource = null;
            }
        }

        /** 認識する言語が変更された */
        private void languageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var lang = languageComboBox.SelectedItem as Language;
            if (lang != null) {
                speechRecognitionServiceManager.Language = languages[languageComboBox.SelectedIndex].Code;
            }
        }
    }
}
