﻿using NAudio.CoreAudioApi;
using Newtonsoft.Json.Linq;

namespace sikusiSubtitles.SpeechRecognition {
    public class SpeechRecognitionServiceManager : sikusiSubtitles.Service {
        public static new string ServiceName = "SpeechRecognition";

        // Events
        public event EventHandler<SpeechRecognitionEventArgs>? Recognizing;
        public event EventHandler<SpeechRecognitionEventArgs>? Recognized;

        // Properties
        public MMDevice? Device { get; set; }
        public string Engine { get; set; } = "";
        public string Language { get; set; } = "";

        private CheckBox speechRecognitionCheckBox;
        SpeechRecognitionService? speechRecognitionService;

        public SpeechRecognitionServiceManager(ServiceManager serviceManager) : base(serviceManager, ServiceName, ServiceName, "音声認識", 100, true) {
            speechRecognitionCheckBox = new CheckBox();
            speechRecognitionCheckBox.Appearance = Appearance.Button;
            speechRecognitionCheckBox.Text = "音声認識";
            speechRecognitionCheckBox.TextAlign = ContentAlignment.MiddleCenter;
            speechRecognitionCheckBox.Width = 70;
            speechRecognitionCheckBox.CheckedChanged += speechRecognitionCheckBox_CheckedChanged;
            serviceManager.AddTopFlowControl(speechRecognitionCheckBox, 100);
        }

        public override UserControl? GetSettingPage()
        {
            return new SpeechRecognitionPage(ServiceManager, this);
        }

        public override void Load(JToken token) {
            var device = token.Value<string>("Device") ?? "";

            // マイク設定
            var enumerator = new MMDeviceEnumerator();
            var micList = enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active);
            Device = micList.Where(mic => mic.ID == device).FirstOrDefault();
            if (Device == null) {
                Device = enumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Console);
            }

            // 音声認識エンジン
            Engine = token.Value<string>("Engine") ?? "";
            Language = token.Value<string>("Language") ?? "";
        }

        public override JObject Save() {
            return new JObject {
                new JProperty("Device", Device?.ID ?? ""),
                new JProperty("Engine", Engine),
                new JProperty("Language", Language)
            };
        }

        public SpeechRecognitionService? GetEngine() {
            return this.ServiceManager.GetServices<SpeechRecognitionService>(ServiceName).Where(service => service.Name == Engine).First();
        }

        /** 音声認識の開始終了のボタン */
        private void speechRecognitionCheckBox_CheckedChanged(object? sender, EventArgs e) {
            this.SetCheckBoxButtonColor(this.speechRecognitionCheckBox);

            if (this.speechRecognitionCheckBox.Checked) {
                this.SpeechRecognitionStart();
            } else {
                this.SpeechRecognitionStop();
            }
        }

        /**
         * 音声認識を開始する
         */
        private void SpeechRecognitionStart() {
            if (speechRecognitionService == null) {
                var manager = ServiceManager.GetManager<SpeechRecognitionServiceManager>();
                if (manager?.Device == null) {
                    MessageBox.Show("マイクを設定してください。");
                } else {
                    var service = manager.GetEngine();
                    if (service != null) {
                        if (service.Start()) {
                            speechRecognitionService = service;
                            service.Recognizing += RecognizingHandler;
                            service.Recognized += RecognizedHandler;
                        }
                    }
                }

                // 音声認識を開始できなかった場合、音声認識ボタンのチェックを外す。
                if (speechRecognitionService == null) {
                    this.speechRecognitionCheckBox.Checked = false;
                }
            } else {
                this.speechRecognitionCheckBox.Checked = false;
            }
        }

        /**
         * 音声認識を終了する
         */
        private void SpeechRecognitionStop() {
            if (speechRecognitionService != null) {
                speechRecognitionService.Recognizing -= RecognizingHandler;
                speechRecognitionService.Recognized -= RecognizedHandler;
                speechRecognitionService.Stop();
                speechRecognitionService = null;
            }
        }

        private void RecognizingHandler(Object? sender, SpeechRecognitionEventArgs args) {
            this.Recognizing?.Invoke(sender, args);
        }

        private void RecognizedHandler(Object? sender, SpeechRecognitionEventArgs args) {
            this.Recognized?.Invoke(sender, args);
        }

    }
}
