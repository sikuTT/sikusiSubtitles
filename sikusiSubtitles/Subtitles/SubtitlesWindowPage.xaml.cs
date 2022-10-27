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
    class PageViewModel : INotifyPropertyChanged {
        private SubtitlesWindowService? service;

        public Color BackgroundColor {
            get => backgroundColor;
            set {
                backgroundColor = value;
                if (service != null) service.BackgroundColor = ColorToString(value);
                OnPropertyChanged();
            }
        }
        Color backgroundColor = Colors.White;

        public Color FontColor {
            get => fontColor;
            set {
                fontColor = value;
                if (service != null) service.FontColor = ColorToString(value);
                OnPropertyChanged();
            }
        }
        Color fontColor = Colors.White;

        public Color StrokeColor {
            get => strokeColor;
            set {
                strokeColor = value;
                if (service != null) service.StrokeColor = ColorToString(value);
                OnPropertyChanged();
            }
        }
        Color strokeColor = Colors.White;

        public PageViewModel() {}

        public PageViewModel(SubtitlesWindowService? service) {
            this.service = service;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        protected string ColorToString(Color c) {
            return string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", c.A, c.R, c.G, c.B);
        }
    }

    class ViewFont {
        public string Name { get; set; }
        public FontFamily FontFamily { get; set; }

#pragma warning disable CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。Null 許容として宣言することをご検討ください。
        public ViewFont() { }
#pragma warning restore CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。Null 許容として宣言することをご検討ください。
        public ViewFont(FontFamily fontFamily) {
            this.FontFamily = fontFamily;

            var culture = Thread.CurrentThread.CurrentCulture;
            var key = XmlLanguage.GetLanguage(culture.IetfLanguageTag);
            string name;
            if (fontFamily.FamilyNames.TryGetValue(key, out name)) {
                Name = name;
            } else {
                Name = fontFamily.FamilyNames.FirstOrDefault().Value;
            }
        }
    }

    /// <summary>
    /// SubtitlesWindowPage.xaml の相互作用ロジック
    /// </summary>
    public partial class SubtitlesWindowPage : UserControl {
        PageViewModel viewModel;
        ServiceManager serviceManager;
        SubtitlesWindowService service;

        public SubtitlesWindowPage(ServiceManager serviceManager, SubtitlesWindowService service) {
            this.serviceManager = serviceManager;
            this.service = service;

            InitializeComponent();

            viewModel = new PageViewModel(service);
            this.DataContext = viewModel;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            viewModel.BackgroundColor = (Color)ColorConverter.ConvertFromString(service.BackgroundColor);
            viewModel.FontColor = (Color)ColorConverter.ConvertFromString(service.FontColor);
            viewModel.StrokeColor = (Color)ColorConverter.ConvertFromString(service.StrokeColor);

            // フォント一覧を設定
            var fontList = Fonts.SystemFontFamilies.Select(f => new ViewFont(f)).ToList();
            this.fontComboBox.ItemsSource = fontList;
            this.fontComboBox.DisplayMemberPath = "Name";

            // デフォルトのフォントを選択
            var defaultFont = SystemFonts.MessageFontFamily;
            var font = fontList.Where(font => font.FontFamily.Source == service.FontFamily).FirstOrDefault() ?? fontList.Where(font => font.FontFamily.Source == defaultFont.Source).FirstOrDefault();
            fontComboBox.SelectedItem = font;
        }
    }
}
