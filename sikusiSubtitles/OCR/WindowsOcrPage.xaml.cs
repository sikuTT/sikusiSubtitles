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

namespace sikusiSubtitles.OCR {
    /// <summary>
    /// WindowsOcrPage.xaml の相互作用ロジック
    /// </summary>
    public partial class WindowsOcrPage : UserControl {
        ServiceManager serviceManager;
        WindowsOcrService service;

        public WindowsOcrPage(ServiceManager serviceManager, WindowsOcrService service) {
            InitializeComponent();

            this.serviceManager = serviceManager;
            this.service = service;

            languageListBox.ItemsSource = service.GetLanguages();
            languageListBox.DisplayMemberPath = "Name";
        }

        private void regionLanguageLink_RequestNavigate(object sender, RequestNavigateEventArgs e) {
            ProcessStartInfo pi = new ProcessStartInfo() {
                FileName = e.Uri.AbsoluteUri,
                UseShellExecute = true,
            };
            System.Diagnostics.Process.Start(pi);
            e.Handled = true;
        }
    }
}
