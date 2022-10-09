using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace sikusiSubtitles {
    /// <summary>
    /// このカスタム コントロールを XAML ファイルで使用するには、手順 1a または 1b の後、手順 2 に従います。
    ///
    /// 手順 1a) 現在のプロジェクトに存在する XAML ファイルでこのカスタム コントロールを使用する場合
    /// この XmlNamespace 属性を使用場所であるマークアップ ファイルのルート要素に
    /// 追加します:
    ///
    ///     xmlns:MyNamespace="clr-namespace:sikusiSubtitles"
    ///
    ///
    /// 手順 1b) 異なるプロジェクトに存在する XAML ファイルでこのカスタム コントロールを使用する場合
    /// この XmlNamespace 属性を使用場所であるマークアップ ファイルのルート要素に
    /// 追加します:
    ///
    ///     xmlns:MyNamespace="clr-namespace:sikusiSubtitles;assembly=sikusiSubtitles"
    ///
    /// また、XAML ファイルのあるプロジェクトからこのプロジェクトへのプロジェクト参照を追加し、
    /// リビルドして、コンパイル エラーを防ぐ必要があります:
    ///
    ///     ソリューション エクスプローラーで対象のプロジェクトを右クリックし、
    ///     [参照の追加] の [プロジェクト] を選択してから、このプロジェクトを参照し、選択します。
    ///
    ///
    /// 手順 2)
    /// コントロールを XAML ファイルで使用します。
    ///
    ///     <MyNamespace:NumericEditBox/>
    ///
    /// </summary>
    public class NumericEditBox : TextBox {
        // public static readonly DependencyProperty MinValueProperty = DependencyProperty.Register("MinValue", typeof(int), typeof(NumericEditBox));
        // public static readonly DependencyProperty MaxvalueProperty = DependencyProperty.Register("MaxValue", typeof(int), typeof(NumericEditBox));
        
        static NumericEditBox() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NumericEditBox), new FrameworkPropertyMetadata(typeof(NumericEditBox)));
        }

        public int Value {
            get { return int.Parse(Text); }
            set { Text = value.ToString(); }
        }

        public int MinValue { get; set; } = Int32.MinValue;
        public int MaxValue { get; set; } = Int32.MaxValue;

        protected override void OnLostFocus(RoutedEventArgs e) {
            base.OnLostFocus(e);

            var value = int.Parse(Text);
            if (value < MinValue) {
                Text = MinValue.ToString();
            } else if (value > MaxValue) {
                Text = MaxValue.ToString();
            }
        }

        protected override void OnPreviewTextInput(TextCompositionEventArgs e) {
            base.OnPreviewTextInput(e);

            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e) {
            base.OnPreviewKeyDown(e);

            if (e.Key == Key.Space) {
                e.Handled = true;
            } else if (e.Key == Key.Up || e.Key == Key.Down) {
                int num = 0;
                Int32.TryParse(Text, out num);

                if (e.Key == Key.Up && num < MaxValue) {
                    Text = (++num).ToString();
                } else if (e.Key == Key.Down && num > MinValue) {
                    Text = (--num).ToString();
                }
            }
        }
    }
}
