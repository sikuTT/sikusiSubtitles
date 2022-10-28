using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace sikusiSubtitles.Subtitles {
    class WindowPageViewModel {
        // ウィンドウ設定
        public ReactivePropertySlim<bool> CaptionBarVisibled { get; set; } = new(false);
        public ReactivePropertySlim<Color> BackgroundColor { get; set; } = new(Colors.LimeGreen);

        // 文字設定
        public ReactivePropertySlim<string> FontFamily { get; set; } = new(SystemFonts.MessageFontFamily.Source);
        public ReactivePropertySlim<int> FontSize { get; set; } = new(12);
        public ReactivePropertySlim<bool> Bold { get; set; } = new(false);
        public ReactivePropertySlim<bool> Italic { get; set; } = new(false);
        public ReactivePropertySlim<Color> FontColor { get; set; } = new(Colors.White);
        public ReactivePropertySlim<bool> Stroke { get; set; } = new(true);
        public ReactivePropertySlim<int> StrokeWidth { get; set; } = new(2);
        public ReactivePropertySlim<Color> StrokeColor { get; set; } = new(Colors.Black);

        public WindowPageViewModel() {}
    }

    class ViewFont {
        public string Name { get; set; } = "";
        public FontFamily FontFamily { get; set; } = SystemFonts.MessageFontFamily;

        public ViewFont() {
            Name = GetFontName();
        }
        public ViewFont(FontFamily fontFamily) {
            this.FontFamily = fontFamily;
            Name = GetFontName();
        }

        private string GetFontName() {
            var culture = Thread.CurrentThread.CurrentCulture;
            var key = XmlLanguage.GetLanguage(culture.IetfLanguageTag);
            string name;
            if (FontFamily.FamilyNames.TryGetValue(key, out name)) {
                return name;
            } else {
                return FontFamily.FamilyNames.FirstOrDefault().Value;
            }
        }
    }

    /// <summary>
    /// SubtitlesWindowPage.xaml の相互作用ロジック
    /// </summary>
    public partial class SubtitlesWindowPage : UserControl {
        WindowPageViewModel viewModel = new();
        ServiceManager serviceManager;
        SubtitlesWindowService service;

        public SubtitlesWindowPage(ServiceManager serviceManager, SubtitlesWindowService service) {
            InitializeComponent();

            this.DataContext = viewModel;
            this.serviceManager = serviceManager;
            this.service = service;

            // Modelの値が変わった時、Serviceにも繁栄する
            viewModel.BackgroundColor.Subscribe(value => service.BackgroundColor = ColorToString(value));
            viewModel.FontFamily.Subscribe(value => service.FontFamily = value);
            viewModel.FontSize.Subscribe(value => service.FontSize = value);
            viewModel.Bold.Subscribe(value => service.Bold = value);
            viewModel.Italic.Subscribe(value => service.Italic = value);
            viewModel.FontColor.Subscribe(value => service.FontColor = ColorToString(value));
            viewModel.Stroke.Subscribe(value => service.Stroke = value);
            viewModel.StrokeWidth.Subscribe(value => service.StrokeWidth = value);
            viewModel.StrokeColor.Subscribe(value => service.StrokeColor = ColorToString(value));
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            viewModel.BackgroundColor.Value = (Color)ColorConverter.ConvertFromString(service.BackgroundColor);
            var fontFamily = Fonts.SystemFontFamilies.Where(value => value.Source == service.FontFamily).Select(value => value.Source).FirstOrDefault();
            viewModel.FontFamily.Value = fontFamily ?? SystemFonts.MessageFontFamily.Source;
            viewModel.FontSize.Value = service.FontSize;
            viewModel.Bold.Value = service.Bold;
            viewModel.Italic.Value = service.Italic;
            viewModel.FontColor.Value = (Color)ColorConverter.ConvertFromString(service.FontColor);
            viewModel.Stroke.Value = service.Stroke;
            viewModel.StrokeWidth.Value = service.StrokeWidth;
            viewModel.StrokeColor.Value = (Color)ColorConverter.ConvertFromString(service.StrokeColor);

            // フォント一覧を設定
            fontComboBox.ItemsSource = Fonts.SystemFontFamilies.Select(f => new ViewFont(f)).ToList();
        }

        private string ColorToString(Color c) {
            return string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}" , c.A , c.R , c.G , c.B);
        }
    }
}
