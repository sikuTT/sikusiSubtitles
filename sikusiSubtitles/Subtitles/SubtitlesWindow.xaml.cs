using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
    class SubtitlesWindowViewModel {
        public ReactivePropertySlim<string> VoiceText { get; set; } = new("");
        public ReactivePropertySlim<Brush> BackgroundBrush{ get; set; } = new();
    }

    /// <summary>
    /// SubtitlesWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class SubtitlesWindow : Window {
        SubtitlesWindowViewModel viewModel = new();

        ServiceManager serviceManager;
        SubtitlesWindowService service;
        SubtitlesService? subtitlesService;

        public SubtitlesWindow(ServiceManager serviceManager, SubtitlesWindowService service) {
            InitializeComponent();
            this.DataContext = viewModel;
            this.serviceManager = serviceManager;
            this.service = service;

            try {
                var color = (Color)ColorConverter.ConvertFromString(service.BackgroundColor);
                viewModel.BackgroundBrush.Value = new SolidColorBrush(color);
            } catch (Exception ex) {
                Debug.WriteLine("SubtitlesWindow: " + ex.Message);
            }

            subtitlesService = serviceManager.GetService<SubtitlesService>();
            if (subtitlesService != null) {
                subtitlesService.SubtitlesChanged += SubtitlesChangedHandler;
            }
        }

        private void Window_Closed(object sender , EventArgs e) {
            if (subtitlesService != null) {
                subtitlesService.SubtitlesChanged -= SubtitlesChangedHandler;
            }
        }

        private void SubtitlesChangedHandler(object? sender , List<SubtitlesText> e) {
            var voiceText = "";
            foreach (var text in e) {
                voiceText += text.VoiceText;
            }

            viewModel.VoiceText.Value = voiceText;
        }
    }
}
