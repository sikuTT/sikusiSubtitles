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

namespace sikusiSubtitles.OCR
{
    /// <summary>
    /// TesseractOcrPage.xaml の相互作用ロジック
    /// </summary>
    public partial class TesseractOcrPage : UserControl
    {
        ServiceManager serviceManager;
        TesseractOcrService service;

        public TesseractOcrPage(ServiceManager serviceManager, TesseractOcrService service)
        {
            this.serviceManager = serviceManager;
            this.service = service;

            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            installedLanguagesListBox.ItemsSource = service.GetLanguages();
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e) {
            ProcessStartInfo pi = new ProcessStartInfo() {
                FileName = e.Uri.AbsoluteUri,
                UseShellExecute = true,
            };
            System.Diagnostics.Process.Start(pi);
            e.Handled = true;
        }
    }
}
