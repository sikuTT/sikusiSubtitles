﻿using System;
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
    public class OcrProcess {
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

        ObservableCollection<OcrProcess> processList = new ObservableCollection<OcrProcess>();

        DispatcherTimer? refreshTimer;

        List<Window> ocrWindows = new List<Window>();

        public OcrPage(ServiceManager serviceManager, OcrServiceManager ocrManager) {
            this.serviceManager = serviceManager;
            this.ocrManager = ocrManager;

            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            processListView.DataContext = processList;
            RefreshProcessList();
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
        private void refreshButton_Click(object sender, RoutedEventArgs e) {
            RefreshProcessList();
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
        private void RefreshProcessList() {
            var selectedItem = processListView.SelectedItem as OcrProcess;

            this.processList.Clear();

            // プロセス一覧を取得する
            var processList = Process.GetProcesses();
            foreach (var process in processList) {
                if (process.Id != Process.GetCurrentProcess().Id && process.MainWindowTitle != "") {
                    // プロセスを追加1
                    this.processList.Add(new OcrProcess() {
                        ProcessId = process.Id,
                        ProcessName = process.ProcessName,
                        WindowTitle = process.MainWindowTitle,
                    });
                }
            }

            // リスト更新前に選択されていたプロセスを再選択する
            if (selectedItem != null) {
                var item  = this.processList.Where(process => process.ProcessId == selectedItem.ProcessId).FirstOrDefault();
                if (item != null) processListView.SelectedItem = item;
            }
        }

        /** プロセス一覧の更新タイマー */
        private void RefreshTimerTick(object? sender, EventArgs args) {
            RefreshProcessList();
        }
    }
}