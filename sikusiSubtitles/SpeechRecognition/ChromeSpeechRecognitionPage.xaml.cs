using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// ChromeSpeechRecognitionPage.xaml の相互作用ロジック
    /// </summary>
    public partial class ChromeSpeechRecognitionPage : UserControl {
        ServiceManager serviceManager;
        ChromeSpeechRecognitionService service;

        public ChromeSpeechRecognitionPage(ServiceManager serviceManager, ChromeSpeechRecognitionService service) {
            this.serviceManager = serviceManager;
            this.service = service;

            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            // コントロールの初期値
            httpPortNumericEditBox.Value = service.HttpServerPort;
            webSocketPortNumericEditBox.Value = service.WebSocketPort;
        }

        private void httpPortNumericEditBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e) {
            service.HttpServerPort = e.NewValue;
        }

        private void webSocketPortNumericEditBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e) {
            service.WebSocketPort = e.NewValue;
        }
    }
}
