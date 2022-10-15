using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace sikusiSubtitles.Controls {
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
        //
        // ValueChanged Event
        //
        public static readonly RoutedEvent ValueChangedEvent = EventManager.RegisterRoutedEvent(
            name: "ValueChanged",
            routingStrategy: RoutingStrategy.Bubble,
            handlerType: typeof(RoutedPropertyChangedEventHandler<int>),
            ownerType: typeof(NumericEditBox)
        );

        public event RoutedEventHandler ValueChanged {
            add { AddHandler(ValueChangedEvent, value); }
            remove { RemoveHandler(ValueChangedEvent, value); }
        }

        void RaiseValueChangedEvent(int newValue, int oldValue) {
            RoutedPropertyChangedEventArgs<int> routedEventArgs = new (newValue, oldValue, ValueChangedEvent);
            RaiseEvent(routedEventArgs);
        }

        //
        // Value Property
        //
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value",
            typeof(int),
            typeof(NumericEditBox),
            new PropertyMetadata(0, ValuePropertyChanged)
        );

        static void ValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            NumericEditBox control = (NumericEditBox)d;
            int value = (int)e.NewValue;
            if (value < control.MinValue) {
                value = control.MinValue;
            } else if (value  > control.MaxValue) {
                value  = control.MaxValue;
            }

            control.Text = value.ToString();
            control.RaiseValueChangedEvent(value, (int)e.OldValue);
        }

        public int Value {
            get => (int)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        //
        // MinValue Property
        //
        public static readonly DependencyProperty MinValueProperty = DependencyProperty.Register(
            "MinValue",
            typeof(int),
            typeof(NumericEditBox),
            new PropertyMetadata(int.MinValue, MinValuePropertyChanged)
        );

        static void MinValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            NumericEditBox control = (NumericEditBox)d;
            if (control.Value < (int)e.NewValue) {
                control.Value = (int)e.NewValue;
            }
        }

        public int MinValue {
            get => (int)GetValue(MinValueProperty);
            set => SetValue(MinValueProperty, value);
        }

        //
        // MaxValue Property
        //
        public static readonly DependencyProperty MaxValueProperty = DependencyProperty.Register(
            "MaxValue",
            typeof(int),
            typeof(NumericEditBox),
            new PropertyMetadata(int.MaxValue, MaxValuePropertyChanged)
        );

        static void MaxValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            NumericEditBox control = (NumericEditBox)d;
            if (control.Value > (int)e.NewValue) {
                control.Value = (int)e.NewValue;
            }
        }

        public int MaxValue {
            get => (int)GetValue(MaxValueProperty);
            set => SetValue(MaxValueProperty, value);
        }

        /**
         * Constructor
         */
        static NumericEditBox() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NumericEditBox), new FrameworkPropertyMetadata(typeof(NumericEditBox)));
        }

        /**
         * フォーカスが外れた時、値に問題がある場合、問題のない値に変更する
         */
        protected override void OnLostFocus(RoutedEventArgs e) {
            base.OnLostFocus(e);

            int num;
            if (int.TryParse(Text, out num)) {
                if (num < MinValue) {
                    num = MinValue;
                } else if (num > MaxValue) {
                    num = MaxValue;
                }
                Value = num;
            } else {
                Text = Value.ToString();
            }
        }

        /**
         * 文字入力時に呼ばれる
         * 数値のみ入力できるようにする
         */
        protected override void OnPreviewTextInput(TextCompositionEventArgs e) {
            base.OnPreviewTextInput(e);

            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        /**
         * キーが押された時に呼ばれる
         * 上下のキーが押された場合、値をプラスマイナス１する
         */
        protected override void OnPreviewKeyDown(KeyEventArgs e) {
            base.OnPreviewKeyDown(e);

            if (e.Key == Key.Space) {
                e.Handled = true;
            } else if (e.Key == Key.Up || e.Key == Key.Down) {
                if (e.Key == Key.Up && Value < MaxValue) {
                    Value++;
                } else if (e.Key == Key.Down && Value > MinValue) {
                    Value--;
                }
            }
        }

        /**
         * 文字が入力されたときに呼ばれる
         * 入力された値が数値なら、値を更新する
         */
        protected override void OnTextChanged(TextChangedEventArgs e) {
            int num;
            if (int.TryParse(Text, out num) && IsValid(num)) {
                Value = num;
            }
        }

        /** 入力チェック */
        private bool IsValid(int number) {
            int num;
            if (int.TryParse(Text, out num)) {
                return num >= MinValue && num <= MaxValue;
            } else {
                return false;
            }
        }
    }
}
