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

        public SpeechRecognitionPage(ServiceManager serviceManager, SpeechRecognitionServiceManager speechRecognitionServiceManager) {
            this.serviceManager = serviceManager;
            this.speechRecognitionServiceManager = speechRecognitionServiceManager;

            InitializeComponent();
        }
    }
}
