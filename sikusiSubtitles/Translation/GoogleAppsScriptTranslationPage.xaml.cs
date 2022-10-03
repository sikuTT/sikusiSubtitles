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

namespace sikusiSubtitles.Translation {
    /// <summary>
    /// GoogleAppsScriptTranslationPage.xaml の相互作用ロジック
    /// </summary>
    public partial class GoogleAppsScriptTranslationPage : UserControl {
        ServiceManager serviceManager;
        GoogleAppsScriptTranslationService service;

        public GoogleAppsScriptTranslationPage(ServiceManager serviceManager, GoogleAppsScriptTranslationService service) {
            this.serviceManager = serviceManager;
            this.service = service;

            InitializeComponent();

            codeTextBox.Text =
                "function doGet(e) {\r\n" +
                "    LanguageApp.translate(e.parameter.text, e.parameter.source, e.parameter.target);\r\n" +
                "    const output = ContentService.createTextOutput();\r\n" +
                "    output.setMimeType(ContentService.MimeType.TEXT);\r\n" +
                "    output.setContent(translatedText);\r\n" +
                "    return output;\r\n" +
                "}";
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
