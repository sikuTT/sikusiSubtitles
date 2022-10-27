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
        private SubtitlesWindowService? service;

        // ウィンドウ設定
        public ReactivePropertySlim<bool> CaptionBarVisibled { get; set; } = new(false);
        public ReactivePropertySlim<Color> BackgroundColor { get; set; } = new(Colors.LimeGreen);

        // 文字設定
        public ReactivePropertySlim<FontFamily> FontFamily { get; set; } = new(SystemFonts.MessageFontFamily);
        public ReactivePropertySlim<int> FontSize { get; set; } = new(12);
        public ReactivePropertySlim<FontWeight> Bold { get; set; } = new(FontWeights.Regular);
        public ReactivePropertySlim<FontStyle> Italic { get; set; } = new(FontStyles.Normal);
        public ReactivePropertySlim<Color> FontColor { get; set; } = new(Colors.White);
        public ReactivePropertySlim<bool> Stroke { get; set; } = new(true);
        public ReactivePropertySlim<int> StrokeWidth { get; set; } = new(2);
        public ReactivePropertySlim<Color> StrokeColor { get; set; } = new(Colors.Black);

        public WindowPageViewModel() {}

        public WindowPageViewModel(SubtitlesWindowService? service) {
            this.service = service;
        }
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
        WindowPageViewModel viewModel;
        ServiceManager serviceManager;
        SubtitlesWindowService service;

        public SubtitlesWindowPage(ServiceManager serviceManager, SubtitlesWindowService service) {
            InitializeComponent();

            this.serviceManager = serviceManager;
            this.service = service;

            this.DataContext = viewModel = new(service);
            viewModel.BackgroundColor.Subscribe(color => service.BackgroundColor = ColorToString(color));
            viewModel.FontColor.Subscribe(color => service.FontColor = ColorToString(color));
            viewModel.StrokeColor.Subscribe(color => service.StrokeColor = ColorToString(color));
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            viewModel.BackgroundColor.Value = (Color)ColorConverter.ConvertFromString(service.BackgroundColor);
            viewModel.FontColor.Value = (Color)ColorConverter.ConvertFromString(service.FontColor);
            viewModel.StrokeColor.Value = (Color)ColorConverter.ConvertFromString(service.StrokeColor);

            // フォント一覧を設定
            var fontList = Fonts.SystemFontFamilies.Select(f => new ViewFont(f)).ToList();
            this.fontComboBox.ItemsSource = fontList;
            this.fontComboBox.DisplayMemberPath = "Name";

            // デフォルトのフォントを選択
            var defaultFont = SystemFonts.MessageFontFamily;
            var font = fontList.Where(font => font.FontFamily.Source == service.FontFamily).FirstOrDefault() ?? fontList.Where(font => font.FontFamily.Source == defaultFont.Source).FirstOrDefault();
            fontComboBox.SelectedItem = font;
        }

        private string ColorToString(Color c) {
            return string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}" , c.A , c.R , c.G , c.B);
        }
    }
}
