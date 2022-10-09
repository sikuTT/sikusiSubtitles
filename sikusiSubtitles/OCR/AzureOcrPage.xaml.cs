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

namespace sikusiSubtitles.OCR
{
    /// <summary>
    /// AzureOcrPage.xaml の相互作用ロジック
    /// </summary>
    public partial class AzureOcrPage : UserControl
    {
        ServiceManager serviceManager;
        AzureOcrService service;

        public AzureOcrPage(ServiceManager serviceManager, AzureOcrService service)
        {
            this.serviceManager = serviceManager;
            this.service = service;

            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            keyPasswordBox.Password = service.Key;
            endpointTextBox.Text = service.Endpoint;

        }

        /** APIキーが入力された */
        private void keyPasswordBox_PasswordChanged(object sender, RoutedEventArgs e) {
            service.Key = keyPasswordBox.Password;
        }

        /** endpointが入力された */
        private void endpointTextBox_TextChanged(object sender, TextChangedEventArgs e) {
            service.Endpoint = endpointTextBox.Text;
        }
    }
}
