using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace sikusiSubtitles.Shortcut {
    /// <summary>
    /// ShortcutPage.xaml の相互作用ロジック
    /// </summary>
    public class ShortcutModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler? PropertyChanged;

        public string Name { get; set; } = "";
        public string Source { get; set; } = "";
        public string Command { get; set; } = "";
        public string ShortcutKey {
            get { return shortcutKey; }
            set {
                shortcutKey = value;
                NotifyPropertyChanged();
            }
        }
        string shortcutKey = "";

        void NotifyPropertyChanged([CallerMemberName] String propertyName = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public partial class ShortcutPage : UserControl {
        ServiceManager serviceManager;
        ShortcutService service;

        ObservableCollection<ShortcutModel> shortcutList = new ObservableCollection<ShortcutModel>();


        public ShortcutPage(ServiceManager serviceManager, ShortcutService Service) {
            this.serviceManager = serviceManager;
            this.service = Service;

            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            service.Shortcuts.ForEach(shortcut => {
                shortcutList.Add(new ShortcutModel {
                    Name = shortcut.Name,
                    Source = shortcut.Source,
                    Command = shortcut.Command,
                    ShortcutKey = shortcut.ShortcutKey,
                });
            });
            shortcutListView.DataContext = shortcutList;

            // ショートカットイベントを取得
            var shortcutService = this.serviceManager.GetService<ShortcutService>();
            if (shortcutService != null) {
                shortcutService.ShortcutRun += ShortcutRun;
            }
        }

        /** ショートカットのリストビューで項目が選択された */
        private void shortcutListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (shortcutListView.SelectedItem != null) {
                shortcutTextBox.Text = "";
                shortcutTextBox.IsEnabled = true;
                setShortcutButton.IsEnabled = true;
            } else {
                shortcutTextBox.Text = "";
                shortcutTextBox.IsEnabled = false;
                setShortcutButton.IsEnabled = false;
            }
        }

        /** ショートカットの設定ボタンが押された */
        private void setShortcutButton_Click(object sender, RoutedEventArgs e) {
            // ショートカットをリストビューに反映
            shortcutList[shortcutListView.SelectedIndex].ShortcutKey = shortcutTextBox.Text;

            // ショートカットをショートカットサービスに反映
            service.Shortcuts[shortcutListView.SelectedIndex].ShortcutKey = this.shortcutTextBox.Text;
        }

        /** ショートカットのクリアボタンが押された */
        private void clearShortcutButton_Click(object sender, RoutedEventArgs e) {
            shortcutList[shortcutListView.SelectedIndex].ShortcutKey = "";

            // ショートカットをショートカットサービスに反映
            service.Shortcuts[shortcutListView.SelectedIndex].ShortcutKey = "";
        }

        private void ShortcutRun(object? sender, Shortcut shortcut) {
            if (this.shortcutTextBox.IsFocused) {
                this.shortcutTextBox.Text = shortcut.ShortcutKey;
            }
        }

    }
}
