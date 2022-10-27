using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
using System.Windows.Shapes;

namespace sikusiSubtitles.Subtitles {
    class WindowViewModel : INotifyPropertyChanged {
        public string Text {
            get => text;
            set {
                text = value;
                OnPropertyChanged();
            }
        }
        string text = "";

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    /// <summary>
    /// SubtitlesWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class SubtitlesWindow : Window {
        SubtitlesWindowService service;

        public SubtitlesWindow(SubtitlesWindowService service) {
            InitializeComponent();

            this.service = service;

            var bgColor = (Color?)ColorConverter.ConvertFromString(service.BackgroundColor);
            if (bgColor != null) background.Fill = new SolidColorBrush((Color)bgColor);
        }
    }
}
