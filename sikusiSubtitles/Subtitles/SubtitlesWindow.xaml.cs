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
        public ReactivePropertySlim<Brush> BackgroundBrush { get; set; } = new(Brushes.LimeGreen);
        public ReactivePropertySlim<string> Text { get; set; } = new("");
        public ReactivePropertySlim<FontFamily> FontFamily { get; set; } = new(SystemFonts.MessageFontFamily);
        public ReactivePropertySlim<int> FontSize { get; set; } = new(12);
        public ReactivePropertySlim<bool> Bold { get; set; } = new(false);
        public ReactivePropertySlim<bool> Italic { get; set; } = new(false);
        public ReactivePropertySlim<Brush> TextBrush { get; set; } = new(Brushes.White);
        public ReactivePropertySlim<bool> Stroke { get; set; } = new(true);
        public ReactivePropertySlim<int> StrokeWidth { get; set; } = new(2);
        public ReactivePropertySlim<Brush> StrokeBrush { get; set; } = new(Brushes.Black);
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

                viewModel.FontFamily.Value = Fonts.SystemFontFamilies.Where(value => value.Source == service.FontFamily).FirstOrDefault() ?? SystemFonts.MessageFontFamily;
                viewModel.FontSize.Value = service.FontSize;
                viewModel.Bold.Value = service.Bold;
                viewModel.Italic.Value = service.Italic;
                viewModel.TextBrush.Value = new SolidColorBrush((Color)ColorConverter.ConvertFromString(service.FontColor));
                viewModel.Stroke.Value = service.Stroke;
                viewModel.StrokeWidth.Value = service.StrokeWidth;
                viewModel.StrokeBrush.Value = new SolidColorBrush((Color)ColorConverter.ConvertFromString(service.StrokeColor));
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
            var translatedTexts = new List<StringBuilder>();
            foreach (var text in e) {
                voiceText += text.VoiceText;
                for (var i = 0; i < text.TranslationTexts.Count; i++) {
                    StringBuilder text2;
                    if (translatedTexts.Count <= i) {
                        text2 = new StringBuilder();
                        translatedTexts.Add(text2);
                    } else {
                        text2 = translatedTexts[i];
                    }
                    text2.Append(text.TranslationTexts[i].Text);
                }
            }

            string subtitlesText = voiceText;
            foreach (var text2 in translatedTexts) {
                subtitlesText += "\n" + text2.ToString();
            }
            viewModel.Text.Value = subtitlesText;
        }
    }
}
