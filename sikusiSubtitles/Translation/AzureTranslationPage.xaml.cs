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

namespace sikusiSubtitles.Translation {
    /// <summary>
    /// AzureTranslationPage.xaml の相互作用ロジック
    /// </summary>
    public partial class AzureTranslationPage : UserControl {
        ServiceManager serviceManager;
        AzureTranslationService service;

        public AzureTranslationPage(ServiceManager serviceManager, AzureTranslationService service) {
            this.serviceManager = serviceManager;
            this.service = service;

            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            keyPasswordBox.Password = service.Key;
            regionTextBox.Text = service.Region;
        }

        /** パスワードが変更された */
        private void keyPasswordBox_PasswordChanged(object sender, RoutedEventArgs e) {
            service.Key = keyPasswordBox.Password;
        }

        /** 地域が変更された */
        private void regionTextBox_TextChanged(object sender, TextChangedEventArgs e) {
            service.Region = regionTextBox.Text;
        }
    }
}
