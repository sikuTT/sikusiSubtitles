using Newtonsoft.Json.Linq;
using sikusiSubtitles.OBS;
using sikusiSubtitles.OCR;
using sikusiSubtitles.Shortcut;
using sikusiSubtitles.Speech;
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
        ServiceManager serviceManager;

        // サービス作成 → 設定ページ作成 → サービスの設定をロード → 設定ページのフォームロードの順で動かす
        public MainWindow() {
            InitializeComponent();

            serviceManager = new ServiceManager(this);

            // サービスの作成
            CreateServices();

            // メニューと設定画面の作成
            CreateMenuAndSettingPages();

            // 設定をロード
            LoadSettings();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
        }

        private void Window_Closed(object sender, EventArgs e) {
            serviceManager.Finish();

            var obj = new JObject();
            obj.Add(new JProperty("Width", Width));
            obj.Add(new JProperty("Height", Height));
            obj.Add(new JProperty("MenuPaneWidth", menuPaneColumn.Width.Value));
            this.serviceManager.Save(obj);
        }

        /**
         * 左パネルのツリービューから項目が選択された
         * 右パネルに設定画面を表示する
         */
        private void menuTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e) {
            var item = e.NewValue as TreeViewItem;
            if (item != null) {
                SelectTreeViewItem(item);
            }
        }

        private void menuTreeViewItem_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e) {
            e.Handled = true;
        }


        private UserControl? AddPage(Service service) {
            var page = service.SettingsPage;
            if (page != null) {
                page.Name = service.Name;
                Grid.SetRow(page, 0);
                Grid.SetColumn(page, 0);
                page.Visibility = Visibility.Collapsed;
                settingsGrid.Children.Add(page);
            }
            return page;
        }

        private void SelectTreeViewItem(TreeViewItem item) {
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
                // 設定ページが見つかった場合、見つかった設定ページを表示する
                newPage.Visibility = Visibility.Visible;
            } else {
                // 設定ページが見つからなかった場合、子要素の設定ページを表示する
                if (item.Items.Count > 0) {
                    SelectTreeViewItem((TreeViewItem)item.Items[0]);
                }
            }
        }

        private void CreateServices() {
            // SpeechRecognition Service
            new SpeechRecognitionServiceManager(serviceManager);
            new BrowserSpeechRecognitionPageService(serviceManager);
            new EdgeSpeechRecognitionService(serviceManager);
            new ChromeSpeechRecognitionService(serviceManager);
            new AzureSpeechRecognitionService(serviceManager);
            new AmiVoiceSpeechRecognitionService(serviceManager);

            // Subtitles Service
            new SubtitlesServiceManager(serviceManager);
            new SubtitlesService(serviceManager);
            new SubtitlesWindowService(serviceManager);

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

            // Speech Service
            new SpeechServiceManager(serviceManager);
            new SapiSpeechService(serviceManager);
            new VoiceVoxSpeechService(serviceManager);

            // Shortcut Service
            new ShortcutServiceManager(serviceManager);
            new ShortcutService(serviceManager);

            // OCR Service
            new OcrServiceManager(serviceManager);
            new WindowsOcrService(serviceManager);
            new TesseractOcrService(serviceManager);
            new AzureOcrService(serviceManager);
        }

        private void CreateMenuAndSettingPages() {
            // Controls (Top)
            foreach (var control in serviceManager.TopFlowControls) {
                control.Margin = new Thickness(5, 0, 5, 0);
                this.topFlowPanel.Children.Add(control);
            }

            // Controls (Status Bar)
            foreach (var control in serviceManager.StatusBarControls) {
                this.statusBar.Items.Add(control);
            }

            foreach (var manager in serviceManager.Managers) {
                AddPage(manager);
                var item = new TreeViewItem() { Name = manager.Name, Header = manager.DisplayName, IsExpanded = true };
                menuTreeView.Items.Add(item);

                var services = serviceManager.Services.FindAll(service => service.ServiceName == manager.ServiceName);
                foreach (var service in services) {
                    if (AddPage(service) != null) {
                        if (manager.SettingsPage != null || manager.DisplayName != service.DisplayName) {
                            var childItem = new TreeViewItem() { Name = service.Name, Header = service.DisplayName, IsExpanded = true };
                            item.Items.Add(childItem);
                        }
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

        private void LoadSettings() {
            var obj = this.serviceManager.Load();
            if (obj != null) {
                var width = obj.Value<double?>("Width");
                if (width != null) Width = (double)width;

                var height = obj.Value<double?>("Height");
                if (height != null) Height = (double)height;

                var menuPaneWidth = obj.Value<double?>("MenuPaneWidth");
                if (menuPaneWidth != null) menuPaneColumn.Width = new GridLength((double)menuPaneWidth);
            }
            serviceManager.Init();
        }
    }
}
