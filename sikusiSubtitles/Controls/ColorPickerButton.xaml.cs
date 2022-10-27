using System;
using System.Collections.Generic;
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

namespace sikusiSubtitles.Controls {
    class ColorPickerButtonViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler? PropertyChanged;

        public Brush BackgroundBrush {
            get => backgroundBrush;
            set {
                backgroundBrush = value;
                OnPropertyChanged();
            }
        }
        Brush backgroundBrush = Brushes.White;

        protected void OnPropertyChanged([CallerMemberName] string name = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    /// <summary>
    /// ColorPickerButton.xaml の相互作用ロジック
    /// </summary>
    public partial class ColorPickerButton : UserControl {
        ColorPickerButtonViewModel viewModel = new ColorPickerButtonViewModel();

        public EventHandler<Color>? ColorChanged;

        private static void OnColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var button = (ColorPickerButton)d;
            var c = (Color)e.NewValue;
            button.Background = new SolidColorBrush(c);
            button.ColorChanged?.Invoke(button, c);
        }

        public Color Color {
            get => (Color)GetValue(ColorProperty);
            set {
                SetValue(ColorProperty, value);
            }
        }

        /// <summary>
        /// Identifies the Color dependency property.
        /// </summary>
        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(
            "Color",
            typeof(Color),
            typeof(ColorPickerButton),
            new FrameworkPropertyMetadata(
                Colors.White,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.AffectsRender,
                new PropertyChangedCallback(OnColorChanged)
            )
        );

        public ColorPickerButton() {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            var dialog = new System.Windows.Forms.ColorDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                // var hexString = "#" + dialog.Color.R.ToString("X2") + dialog.Color.G.ToString("X2") + dialog.Color.B.ToString("X2");
                // viewModel.Color = (Color)ColorConverter.ConvertFromString(hexString);
                Color = new Color {
                    R = dialog.Color.R,
                    G = dialog.Color.G,
                    B = dialog.Color.B,
                    A = 255,
                };
                ColorChanged?.Invoke(this, Color);
            }
        }
    }
}
