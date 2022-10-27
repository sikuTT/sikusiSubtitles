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
    public class OcrProcessModel {
        public string ProcessName { get; set; } = "";
        public string WindowTitle { get; set; } = "";
        public int ProcessId { get; set; }
    }

    /// <summary>
    /// OcrPage.xaml の相互作用ロジック
    /// </summary>
    public partial class OcrPage : UserControl {
        ServiceManager serviceManager;
        OcrServiceManager ocrManager;

        List<OcrProcessModel> processList = new List<OcrProcessModel>();

        DispatcherTimer? refreshTimer;

        public OcrPage(ServiceManager serviceManager, OcrServiceManager ocrManager) {
            this.serviceManager = serviceManager;
            this.ocrManager = ocrManager;

            InitializeComponent();
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
                var process = processList[processListView.SelectedIndex];
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
                    var foundProcess = this.processList.Find(p => p.ProcessId == process.Id);
                    modelUpdated = foundProcess == null || (foundProcess.ProcessId == process.Id && foundProcess.WindowTitle != process.MainWindowTitle);
                    if (modelUpdated) break;
                }

                // 削除されたプロセスがあるかを検索
                foreach (var process in this.processList) {
                    var foundProcess = newProcessList.Find(p => p.Id == process.ProcessId);
                    if (foundProcess == null) {
                        modelUpdated = true;
                        break;
                    }
                }

                /** プロセス一覧が更新された場合、モデルを作り直す */
                if (modelUpdated == true) {
                    this.processList = newProcessList.Select(p => new OcrProcessModel {
                        ProcessId = p.Id,
                        ProcessName = p.ProcessName,
                        WindowTitle = p.MainWindowTitle,
                    }).ToList();
                }
            });

            if (modelUpdated == true) {
                var selectedItem = processListView.SelectedItem as OcrProcessModel;

                this.processListView.DataContext = null;
                this.processListView.DataContext = this.processList;

                // リスト更新前に選択されていたプロセスを再選択する
                if (selectedItem != null) {
                    var item = this.processList.Where(process => process.ProcessId == selectedItem.ProcessId).FirstOrDefault();
                    if (item != null) processListView.SelectedItem = item;
                }
            }
        }

        /** プロセス一覧の更新タイマー */
        private async void RefreshTimerTick(object? sender, EventArgs args) {
            await RefreshProcessList();
        }
    }
}
