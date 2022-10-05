using sikusiSubtitles.OBS;
using sikusiSubtitles.SpeechRecognition;
using sikusiSubtitles.Subtitles;
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

            // Subtitles Service
            new SubtitlesServiceManager(serviceManager);
            new SubtitlesService(serviceManager);

            // Translation Service
            new TranslationServiceManager(serviceManager);
            new GoogleAppsScriptTranslationService(serviceManager);
            new GoogleBasicTranslationService(serviceManager);
            new AzureTranslationService(serviceManager);
            new DeepLTranslationService(serviceManager);

            // OBS Service
            new ObsServiceManager(serviceManager);
            new ObsService(serviceManager);
            new ObsSubtitlesService(serviceManager);

            // ServiceManager
            serviceManager.Init();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            // Controls
            foreach (var control in serviceManager.TopFlowControls) {
                this.topPanel.Children.Add(control);
            }

            foreach (var manager in serviceManager.Managers) {
                AddPage(manager);
                var item = new TreeViewItem() { Name = manager.ServiceName, Header = manager.DisplayName, IsExpanded = true };
                menuTreeView.Items.Add(item);
            }

            foreach (var service in serviceManager.Services) {
                AddPage(service);
                var item = new TreeViewItem() { Name = service.Name, Header = service.DisplayName, IsExpanded = true };

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

        private void Window_Closed(object sender, EventArgs e) {

        }

        private void menuTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e) {
            var item = e.NewValue as TreeViewItem;
            if (item != null) {
                SelectTreeViewItem(item.Name);
            }
        }

        private void menuTreeViewItem_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e) {
            e.Handled = true;
        }


        private void AddPage(Service service) {
            var page = service.GetSettingPage();
            if (page != null) {
                page.Name = service.Name;
                Grid.SetRow(page, 0);
                Grid.SetColumn(page, 0);
                page.Visibility = Visibility.Collapsed;
                settingsGrid.Children.Add(page);
            }
        }

        private void SelectTreeViewItem(string name) {
            UserControl? newPage = null;
            foreach (var page in settingsGrid.Children) {
                var settingsPage = page as UserControl;
                if (settingsPage != null) {
                    settingsPage.Visibility = Visibility.Collapsed;
                    if (settingsPage.Name == name) {
                        newPage = settingsPage;
                    }
                }
            }

            if (newPage != null) {
                newPage.Visibility = Visibility.Visible;
            }
        }
    }
}
