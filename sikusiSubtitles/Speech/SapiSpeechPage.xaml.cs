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

namespace sikusiSubtitles.Speech {
    /// <summary>
    /// SystemSpeechPage.xaml の相互作用ロジック
    /// </summary>
    public partial class SystemSpeechPage : UserControl {
        ServiceManager serviceManager;
        SapiSpeechService service;

        public SystemSpeechPage(ServiceManager serviceManager, SapiSpeechService service) {
            this.serviceManager = serviceManager;
            this.service = service;

            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            var voices = service.GetVoices();
            foreach (var voice in voices) {
                voiceComboBox.Items.Add(voice.DisplayName);
            }
        }

        /** 音声の変更 */
        private void voiceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            speechButton.IsEnabled = cancelButton.IsEnabled = CanSpeak();
        }

        /** 読み上げ文章の変更 */
        private void textBox_TextChanged(object sender, TextChangedEventArgs e) {
            speechButton.IsEnabled = cancelButton.IsEnabled = CanSpeak();
        }

        /** 読み上げ開始 */
        private async void speechButton_Click(object sender, RoutedEventArgs e) {

            try {
                speechButton.Visibility = Visibility.Collapsed;
                cancelButton.Visibility = Visibility.Visible;

                var voices = service.GetVoices();
                await service.SpeakAsync(voices[voiceComboBox.SelectedIndex], textBox.Text);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            } finally {
                speechButton.Visibility = Visibility.Visible;
                cancelButton.Visibility = Visibility.Collapsed;
            }
        }

        /** 読み上げキャンセル */
        private async void cancelButton_Click(object sender, RoutedEventArgs e) {
            await service.CancelSpeakAsync();
            speechButton.Visibility = Visibility.Visible;
            cancelButton.Visibility = Visibility.Collapsed;
        }

        /** 読み上げ可能かどうか */
        private bool CanSpeak() {
            return voiceComboBox.SelectedIndex != -1 && textBox.Text.Length > 0;
        }
    }
}
