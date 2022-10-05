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
    /// AzureSpeechRecognitionPage.xaml の相互作用ロジック
    /// </summary>
    public partial class AzureSpeechRecognitionPage : UserControl {
        readonly ServiceManager serviceManager;
        readonly AzureSpeechRecognitionService service;

        public AzureSpeechRecognitionPage(ServiceManager serviceManager, AzureSpeechRecognitionService service) {
            this.serviceManager = serviceManager;
            this.service = service;

            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            // コントロールの初期値
            keyPasswordBox.Password = service.Key;
            regionTextBox.Text = service.Region;
        }

        private void keyPasswordBox_PasswordChanged(object sender, RoutedEventArgs e) {
            service.Key = keyPasswordBox.Password;
        }

        private void regionTextBox_TextInput(object sender, TextCompositionEventArgs e) {
            service.Region = regionTextBox.Text;
        }
    }
}
