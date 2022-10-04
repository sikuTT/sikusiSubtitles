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
    /// AmiVoiceSpeechRecognitionPage.xaml の相互作用ロジック
    /// </summary>
    public partial class AmiVoiceSpeechRecognitionPage : UserControl {
        ServiceManager serviceManager;
        AmiVoiceSpeechRecognitionService service;

        public AmiVoiceSpeechRecognitionPage(ServiceManager serviceManager, AmiVoiceSpeechRecognitionService service) {
            this.serviceManager = serviceManager;
            this.service = service;

            InitializeComponent();

            // キー
            keyPasswordBox.Password = service.Key;

            // ログ保存
            logComboBox.SelectedIndex = service.Log ? 0 : 1;
        }

        /** パスワードが変更された */
        private void keyPasswordBox_PasswordChanged(object sender, RoutedEventArgs e) {
            service.Key = keyPasswordBox.Password;
        }

        /** ログ保存が変更された */
        private void logComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            service.Log = logComboBox.SelectedIndex == 0;
        }
    }
}
