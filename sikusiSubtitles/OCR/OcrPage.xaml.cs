using Newtonsoft.Json.Linq;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Threading;

namespace sikusiSubtitles.OCR {
    public class OcrPageViewModel {
        public List<OcrProcessModel> ProcessList { get; set; } = new();
        public ObservableCollection<NotionDatabase> NotionDatabaseList { get; set; } = new();
        public ObservableCollection <NotionTarget> NotionTarget { get; set; } = new() {
            new() { Name="ウィンドウタイトル" },
            new() { Name="OCRで取得した文字列" },
            new() { Name="翻訳結果" },
            new() { Name="翻訳エンジン" },
        };

        public ObservableCollection<string> NotionPageList { get; set; } = new();
    }

    public class OcrProcessModel {
        public string ProcessName { get; set; } = "";
        public string WindowTitle { get; set; } = "";
        public int ProcessId { get; set; }
    }

    public class NotionDatabase {
        public string Title { get; set; } = "";
        public string Id { get; set; } = "";
    }

    public class NotionTarget {
        public string Name { get; set; } = "";
        public ReactivePropertySlim<string> Page { get; set; } = new("");
    }

    /// <summary>
    /// OcrPage.xaml の相互作用ロジック
    /// </summary>
    public partial class OcrPage : UserControl {
        OcrPageViewModel viewModel = new();
        ServiceManager serviceManager;
        OcrServiceManager ocrManager;

        DispatcherTimer? refreshTimer;

        Dictionary<string, List<string>> notionPageList = new();

        public OcrPage(ServiceManager serviceManager, OcrServiceManager ocrManager) {
            InitializeComponent();

            this.DataContext = viewModel;
            this.serviceManager = serviceManager;
            this.ocrManager = ocrManager;
        }

        private async void UserControl_Loaded(object sender , RoutedEventArgs e) {
            this.ArchiveComboBox.SelectedIndex = (int)ocrManager.Archive;
            this.notionToken.Password = ocrManager.NotionToken;

            Task task = RefreshProcessList();
            if (ocrManager.NotionToken.Length > 0) {
                await RefreshNotionDatabaseList();
            }
        }

        /**
         * コントロールの表示状態が変わった
         * （設定画面が表示されたり、他のページへ移動したりした場合）
         */
        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e) {
            if (IsVisible) {
                Task task = RefreshProcessList();
                CreateRefreshTimer();
            } else {
                StopRefreshTimer();
            }
        }

        /** プロセスが選択された */
        private void processListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            ocrWindowButton.IsEnabled = processListView.SelectedIndex != -1;
        }

        /** OCRウィンドウを表示するボタンが押された */
        private void ocrWindowButton_Click(object sender, RoutedEventArgs e) {
            if (processListView.SelectedIndex != -1) {
                var process = viewModel.ProcessList[processListView.SelectedIndex];
                var win = new OcrWindow(serviceManager, ocrManager, process.ProcessId);
                win.Show();
            }
        }

        /** プロセス一覧を更新するボタンが押された */
        private async void refreshButton_Click(object sender, RoutedEventArgs e) {
            await RefreshProcessList();
        }

        private void CreateRefreshTimer() {
            refreshTimer = new DispatcherTimer() {
                Interval = TimeSpan.FromSeconds(5),
                IsEnabled = true,
            };
            refreshTimer.Tick += RefreshTimerTick;
        }

        private void StopRefreshTimer() {
            if (refreshTimer != null) {
                refreshTimer.Stop();
                refreshTimer.Tick -= RefreshTimerTick;
                refreshTimer = null;
            }
        }

        /** プロセス一覧を更新 */
        private async Task RefreshProcessList() {
            var modelUpdated = false;
            List<Process> newProcessList = new List<Process>();

            await Task.Run(() => {
                // プロセス一覧を取得する
                newProcessList = Process.GetProcesses().Where(p => p.Id != Process.GetCurrentProcess().Id && p.MainWindowTitle != "").ToList();

                // 追加されたプロセス、更新されたプロセスがあるかを検索
                foreach (var process in newProcessList) {
                    var foundProcess = viewModel.ProcessList.Find(p => p.ProcessId == process.Id);
                    modelUpdated = foundProcess == null || (foundProcess.ProcessId == process.Id && foundProcess.WindowTitle != process.MainWindowTitle);
                    if (modelUpdated) break;
                }

                // 削除されたプロセスがあるかを検索
                foreach (var process in viewModel.ProcessList) {
                    var foundProcess = newProcessList.Find(p => p.Id == process.ProcessId);
                    if (foundProcess == null) {
                        modelUpdated = true;
                        break;
                    }
                }

                /** プロセス一覧が更新された場合、モデルを作り直す */
                if (modelUpdated == true) {
                    viewModel.ProcessList = newProcessList.Select(p => new OcrProcessModel {
                        ProcessId = p.Id,
                        ProcessName = p.ProcessName,
                        WindowTitle = p.MainWindowTitle,
                    }).ToList();
                }
            });

            if (modelUpdated == true) {
                var selectedItem = processListView.SelectedItem as OcrProcessModel;

                this.processListView.DataContext = null;
                this.processListView.DataContext = viewModel.ProcessList;

                // リスト更新前に選択されていたプロセスを再選択する
                if (selectedItem != null) {
                    var item = viewModel.ProcessList.Where(process => process.ProcessId == selectedItem.ProcessId).FirstOrDefault();
                    if (item != null) processListView.SelectedItem = item;
                }
            }
        }

        /** プロセス一覧の更新タイマー */
        private async void RefreshTimerTick(object? sender, EventArgs args) {
            await RefreshProcessList();
        }

        private void ArchiveComboBox_SelectionChanged(object sender , SelectionChangedEventArgs e) {
            if (ArchiveComboBox.SelectedIndex <= 0) {
                ocrManager.Archive = OcrArchives.None;
            } else {
                ocrManager.Archive = (OcrArchives)ArchiveComboBox.SelectedIndex;
            }
            notionOptions.Visibility = ocrManager.Archive == OcrArchives.Notion ? Visibility.Visible : Visibility.Collapsed;
        }

        private void notionToken_PasswordChanged(object sender , RoutedEventArgs e) {
            ocrManager.NotionToken = notionToken.Password;
        }

        private async Task RefreshNotionDatabaseList() {
            viewModel.NotionDatabaseList.Clear();
            viewModel.NotionPageList.Clear();
            notionPageList.Clear();

            // NotionのDB一覧を取得
            var notion = new Notion.Notion(ocrManager.NotionToken);
            var obj = await notion.Search();
            if (obj != null) {
                var results = obj.Value<JArray>("results");
                if (results != null) {
                    foreach (var result in results) {
                        // DBの情報を取得
                        var id = result.Value<string>("id");  // DBのID
                        if (id != null) {
                            var titles = result.Value<JArray>("title");  // DBのタイトル
                            if (titles != null && titles.Count > 0) {
                                var titleText = titles[0].Value<string>("plain_text");
                                if (titleText != null) {
                                    // IDとタイトルが取得できた場合、リストに追加する
                                    notionPageList.Add(id, new());
                                    var item = new NotionDatabase { Id = id, Title = titleText };
                                    viewModel.NotionDatabaseList.Add(item);
                                    if (id == ocrManager.NotionDatabaseId) {
                                        NotionDatabase.SelectedItem = item;
                                    }
                                }
                            }

                            // DBの項目一覧を取得
                            var properties = result.Value<JObject>("properties");
                            if (properties != null) {
                                foreach (var prop in properties) {
                                    string name = prop.Key;
                                    var obj2 = prop.Value;
                                    if (obj2 != null) {
                                        var type = obj2.Value<string>("type");
                                        if (type == "title" || type == "rich_text") {
                                            notionPageList[id].Add(name);
                                        }
                                    }
                                }
                            }
                            if (id == ocrManager.NotionDatabaseId) {
                                SetNotionPageList(id);
                            }
                        }
                    }
                }
            }

        }

        private async void NotionRefresh_Click(object sender , RoutedEventArgs e) {
            await RefreshNotionDatabaseList();
        }

        private void NotionDatabase_SelectionChanged(object sender , SelectionChangedEventArgs e) {
            viewModel.NotionPageList.Clear();

            var db = NotionDatabase.SelectedItem as NotionDatabase;
            if (db != null) {
                ocrManager.NotionDatabaseId = db.Id;

                // 項目一覧
                SetNotionPageList(db.Id);
            }
        }

        private void SetNotionPageList(string id) {
            notionPageList[id].ForEach(page => viewModel.NotionPageList.Add(page));

            viewModel.NotionTarget[0].Page.Value = ocrManager.NotionTitleSaveTarget;
            viewModel.NotionTarget[1].Page.Value = ocrManager.NotionTextSaveTarget;
            viewModel.NotionTarget[2].Page.Value = ocrManager.NotionTranslatedTextSaveTarget;
            viewModel.NotionTarget[3].Page.Value = ocrManager.NotionTranslationEngineSaveTarget;
        }

        private void page_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            ocrManager.NotionTitleSaveTarget = viewModel.NotionTarget[0].Page.Value;
            ocrManager.NotionTextSaveTarget = viewModel.NotionTarget[1].Page.Value;
            ocrManager.NotionTranslatedTextSaveTarget = viewModel.NotionTarget[2].Page.Value;
            ocrManager.NotionTranslationEngineSaveTarget = viewModel.NotionTarget[3].Page.Value;
        }
    }
}
