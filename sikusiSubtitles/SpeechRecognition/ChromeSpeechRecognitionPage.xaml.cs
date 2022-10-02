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

        void NumberInputValidator(object sender, TextCompositionEventArgs e) {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void portTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e) {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void portTextBox_PreviewKeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Space) {
                e.Handled = true;
            } else if (e.Key == Key.Up || e.Key == Key.Down) {
                int num = 0;
                Int32.TryParse(portTextBox.Text, out num);

                if (e.Key == Key.Up && num < 65535) {
                    portTextBox.Text = (++num).ToString();
                } else if (e.Key == Key.Down && num > 1) {
                    portTextBox.Text = (--num).ToString();
                }
            }
        }
    }
}
