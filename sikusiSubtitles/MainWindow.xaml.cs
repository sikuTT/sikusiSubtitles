using sikusiSubtitles.SpeechRecognition;
using sikusiSubtitles.Translation;
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

namespace sikusiSubtitles {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        ServiceManager serviceManager = new ServiceManager();

        public MainWindow() {
            InitializeComponent();

            // SpeechRecognition Service
            new SpeechRecognitionServiceManager(serviceManager);
            new ChromeSpeechRecognitionService(serviceManager);
            new AzureSpeechRecognitionService(serviceManager);
            new AmiVoiceSpeechRecognitionService(serviceManager);

            // Translation Service
            new TranslationServiceManager(serviceManager);
            new GoogleAppsScriptTranslationService(serviceManager);

            // ServiceManager
            serviceManager.Init();

            // Controls
            foreach (var control in serviceManager.TopFlowControls) {
                this.topPanel.Children.Add(control);
            }

            var addPage = (Service service) => {
                var page = service.GetSettingPage();
                if (page != null) {
                    page.Name = service.Name;
                    Grid.SetRow(page, 0);
                    Grid.SetColumn(page, 0);
                    page.Visibility = Visibility.Collapsed;
                    settingsGrid.Children.Add(page);
                }
            };

            foreach (var manager in serviceManager.Managers) {
                addPage(manager);

                var item = new TreeViewItem();
                item.Name = manager.Name;
                item.Header = manager.DisplayName;
                menuTreeView.Items.Add(item);
                item.IsExpanded = true;
            }

            foreach (var service in serviceManager.Services) {
                addPage(service);

                var item = new TreeViewItem();
                item.Name = service.Name;
                item.Header = service.DisplayName;
                foreach (var parentItem in menuTreeView.Items) {
                    var parentTreeViewItem = parentItem as TreeViewItem;
                    if (parentTreeViewItem != null && parentTreeViewItem.Name == service.ServiceName) {
                        parentTreeViewItem.Items.Add(item);
                        break;
                    }
                }
            }

            if (menuTreeView.Items.Count > 0) {
                var item = menuTreeView.Items.GetItemAt(0) as TreeViewItem;
                if (item != null) {
                    item.IsSelected = true;
                }
            }
        }

        private void menuTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e) {
            var item = e.NewValue as TreeViewItem;
            if (item != null) {
                UserControl? newPage = null;
                foreach (var page in settingsGrid.Children) {
                    var settingsPage = page as UserControl;
                    if (settingsPage != null) {
                        settingsPage.Visibility = Visibility.Collapsed;
                        if (settingsPage.Name == item.Name) {
                            newPage = settingsPage;
                        }
                    }
                }

                if (newPage != null) {
                    newPage.Visibility = Visibility.Visible;
                }
            }
        }

        private void menuTreeViewItem_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e) {
            e.Handled = true;
        }
    }
}
