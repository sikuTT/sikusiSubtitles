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

namespace sikusiSubtitles.OBS {
    /// <summary>
    /// ObsPage.xaml の相互作用ロジック
    /// </summary>
    public partial class ObsPage : UserControl {
        ServiceManager serviceManager;
        ObsService service;

        public ObsPage(ServiceManager serviceManager, ObsService service) {
            this.serviceManager = serviceManager;
            this.service = service;

            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            ipTextBox.Text = service.IP;
            portNumericEditBox.Value = service.Port;
            passwordBox.Password = service.Password;
        }

        /** IPが入力された */
        private void ipTextBox_TextInput(object sender, TextCompositionEventArgs e) {
            service.IP = ipTextBox.Text;
        }

        /** ポート番号が入力された */
        private void portNumericEditBox_TextInput(object sender, TextCompositionEventArgs e) {
            service.Port = portNumericEditBox.Value;
        }

        /** パスワードが入力された */
        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e) {
            service.Password = passwordBox.Password;
        }
    }
}
