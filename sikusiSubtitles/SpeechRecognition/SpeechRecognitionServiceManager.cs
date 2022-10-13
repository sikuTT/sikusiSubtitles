using NAudio.CoreAudioApi;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace sikusiSubtitles.SpeechRecognition {
    public class SpeechRecognitionServiceManager : sikusiSubtitles.Service {
        public static new string ServiceName = "SpeechRecognition";

        // Events
        public event EventHandler<SpeechRecognitionEventArgs>? Recognizing;
        public event EventHandler<SpeechRecognitionEventArgs>? Recognized;

        // Properties
        public MMDevice? Device { get; set; }
        public string Engine { 
            get { return engine; }
            set {
                engine = value;
                selectingService = speechRecognitionServices.Find(service => service.Name == engine);
                SetStatusBarText();
            }
        }
        string engine = "ChromeSpeechRecognition";

        public string Language { get; set; } = "ja-JP";

        ToggleButton speechRecognitionButton= new ToggleButton();
        Label engineNameBox = new Label();
        List<SpeechRecognitionService> speechRecognitionServices = new List<SpeechRecognitionService>();
        SpeechRecognitionService? selectingService;
        SpeechRecognitionService? runningService;

        public SpeechRecognitionServiceManager(ServiceManager serviceManager) : base(serviceManager, ServiceName, ServiceName, "音声認識", 100, true) {
            // 音声認識開始ボタンを作成
            speechRecognitionButton.Content = "音声認識";
            speechRecognitionButton.Width = 70;
            speechRecognitionButton.Checked += speechRecognitionButton_Checked;
            speechRecognitionButton.Unchecked += speechRecognitionButton_Unchecked;
            serviceManager.AddTopFlowControl(speechRecognitionButton, 100);

            // ステータスバーに音声認識エンジンを表示する
            var stackPanel = new StackPanel { Orientation = Orientation.Horizontal };
            stackPanel.Children.Add(new Image { Source = new BitmapImage(new Uri("pack://application:,,,/Resources/mic.png")) });
            stackPanel.Children.Add(engineNameBox);
            engineNameBox.VerticalContentAlignment = VerticalAlignment.Center;
            serviceManager.AddStatusBarControl(stackPanel, 100);

            // マイク設定
            var enumerator = new MMDeviceEnumerator();
            Device = enumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Console);

            // 設定ページ
            settingsPage = new SpeechRecognitionPage(ServiceManager, this);
        }

        public override void Init()
        {
            speechRecognitionServices = ServiceManager.GetServices<SpeechRecognitionService>();
        }

        public override void Load(JToken token) {
            var device = token.Value<string>("Device") ?? "";

            // マイク設定
            var enumerator = new MMDeviceEnumerator();
            var micList = enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active);
            Device = micList.Where(mic => mic.ID == device).FirstOrDefault() ?? Device;

            // 音声認識エンジン
            Engine = token.Value<string>("Engine") ?? Engine;
            Language = token.Value<string>("Language") ?? Language;
        }

        public override JObject Save() {
            return new JObject {
                new JProperty("Device", Device?.ID ?? ""),
                new JProperty("Engine", Engine),
                new JProperty("Language", Language)
            };
        }

        public SpeechRecognitionService? GetEngine() {
            return selectingService;
        }

        /** 音声認識の開始 */
        private void speechRecognitionButton_Checked(object? sender, RoutedEventArgs e) {
            this.SpeechRecognitionStart();
        }

        /** 音声認識の狩猟 */
        private void speechRecognitionButton_Unchecked(object? sender, RoutedEventArgs e) {
            this.SpeechRecognitionStop();
        }

        /**
         * 音声認識を開始する
         */
        private void SpeechRecognitionStart() {
            if (runningService == null) {
                if (Device == null) {
                    MessageBox.Show("マイクを設定してください。");
                } else {
                    if (selectingService != null) {
                        if (selectingService.Start()) {
                            runningService = selectingService;
                            selectingService.Recognizing += RecognizingHandler;
                            selectingService.Recognized += RecognizedHandler;
                            selectingService.ServiceStopped += ServiceStoppedHandler;
                        }
                    } else {
                        MessageBox.Show("使用する音声認識サービスを指定してください。");
                    }
                }

                // 音声認識を開始できなかった場合、音声認識ボタンのチェックを外す。
                if (runningService == null) {
                    this.speechRecognitionButton.IsChecked = false;
                }
            } else {
                this.speechRecognitionButton.IsChecked = false;
            }
        }

        /**
         * 音声認識を終了する
         */
        private void SpeechRecognitionStop() {
            speechRecognitionButton.Dispatcher.Invoke(() => speechRecognitionButton.IsChecked = false);
            if (runningService != null) {
                runningService.Recognizing -= RecognizingHandler;
                runningService.Recognized -= RecognizedHandler;
                runningService.Stop();
                runningService = null;

                // 音声認識中に使用するエンジンが変更されても音声認識中はエンジンを変更しないので、音声認識終了後に表示を更新する
                engineNameBox.Dispatcher.Invoke(() => SetStatusBarText());
            }
        }

        private void RecognizingHandler(Object? sender, SpeechRecognitionEventArgs args) {
            this.Recognizing?.Invoke(sender, args);
        }

        private void RecognizedHandler(Object? sender, SpeechRecognitionEventArgs args) {
            this.Recognized?.Invoke(sender, args);
        }

        private void ServiceStoppedHandler(Object? sender, object? args) {
            SpeechRecognitionStop();
        }

        private void SetStatusBarText() {
            engineNameBox.Content = runningService != null ? runningService.DisplayName : selectingService != null ? selectingService.DisplayName : "";
        }
    }
}
