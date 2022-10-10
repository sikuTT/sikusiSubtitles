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

        List<Window> ocrWindows = new List<Window>();

        public OcrPage(ServiceManager serviceManager, OcrServiceManager ocrManager) {
            this.serviceManager = serviceManager;
            this.ocrManager = ocrManager;

            InitializeComponent();
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e) {
            processListView.DataContext = processList;
            await RefreshProcessList();
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e) {
            foreach (var win in ocrWindows) {
                win.Close();
            }
        }

        /**
         * コントロールの表示状態が変わった
         * （設定画面が表示されたり、他のページへ移動したりした場合）
         */
        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e) {
            if (IsVisible) {
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
                ocrWindows.Add(win);
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
            var selectedItem = processListView.SelectedItem as OcrProcessModel;
            var updatedModel = false;

            await Task.Run(() => {
                // プロセス一覧を取得する
                var processList = Process.GetProcesses().Where(p => p.Id != Process.GetCurrentProcess().Id && p.MainWindowTitle != "").ToList();

                // 新規のプロセスは追加、変更されたプロセスは更新
                foreach (var process in processList) {
                    var foundProcess = this.processList.Find(p => p.ProcessId == process.Id);
                    if (foundProcess == null) {
                        // プロセスを追加
                        this.processList.Add(new OcrProcessModel() {
                            ProcessId = process.Id,
                            ProcessName = process.ProcessName,
                            WindowTitle = process.MainWindowTitle,
                        });
                        updatedModel = true;
                    } else if (foundProcess.WindowTitle != process.MainWindowTitle) {
                        foundProcess.WindowTitle = process.MainWindowTitle;
                        updatedModel = true;
                    }
                }

                // 削除されたプロセスはモデルから削除
                for (var i = this.processList.Count -1; i >= 0; i--) {
                    var process = this.processList[i];
                    var foundProcess = processList.Where(p => p.Id == process.ProcessId).FirstOrDefault();
                    if (foundProcess == null) {
                        this.processList.RemoveAt(i);
                        updatedModel = true;
                    }
                }
            });

            if (updatedModel == true) {
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
