using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// VoiceVoxSpeechPage.xaml の相互作用ロジック
    /// </summary>
    public partial class VoiceVoxSpeechPage : UserControl {
        ServiceManager serviceManager;
        VoiceVoxSpeechService service;

        public VoiceVoxSpeechPage(ServiceManager serviceManager, VoiceVoxSpeechService service) {
            this.serviceManager = serviceManager;
            this.service = service;

            InitializeComponent();
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e) {
            await Task.Run(() => {
                while (service.VoiceListInitialized == false) {
                    Thread.Sleep(100);
                }
            });
            service.GetVoices().ForEach(voice => this.voiceComboBox.Items.Add(voice.DisplayName));
        }

        /** 音声が変更された */
        private void voiceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            speechButton.IsEnabled = cancelButton.IsEnabled = CanSpeak();
        }

        /** 音声一覧の更新ボタンが押された */
        private async void refreshVoiceButton_Click(object sender, RoutedEventArgs e) {
            await RefreshVoice(true);
        }

        /** 読み上げ文章が変更された */
        private void textBox_TextChanged(object sender, TextChangedEventArgs e) {
            speechButton.IsEnabled = cancelButton.IsEnabled = CanSpeak();
        }

        /** 読み上げ開始ボタンが押された */
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

        /** 読み上げキャンセルボタンが押された */
        private async void cancelButton_Click(object sender, RoutedEventArgs e) {
            await service.CancelSpeakAsync();
            speechButton.Visibility = Visibility.Visible;
            cancelButton.Visibility = Visibility.Collapsed;
        }

        /** 音声一覧の再取得 */
        private async Task RefreshVoice(bool refreshService = false) {
            await service.GetSpeakers();
            voiceComboBox.Items.Clear();
            service.GetVoices().ForEach(voice => this.voiceComboBox.Items.Add(voice.DisplayName));
        }

        /** 読み上げ可能かどうか */
        private bool CanSpeak() {
            return voiceComboBox.SelectedIndex != -1 && textBox.Text.Length > 0;
        }
    }
}
