using NAudio.CoreAudioApi;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace sikusiSubtitles.SpeechRecognition {
    public class SpeechRecognitionServiceManager : sikusiSubtitles.Service {
        class Properties {
            public string Device { get; set; } = "";
            public string Engine { get; set; } = "";
            public string Language { get; set; } = "";
        }

        private CheckBox speechRecognitionCheckBox;
        private SpeechRecognitionService? speechRecognitionService;

        public static new string ServiceName = "SpeechRecognition";

        public MMDevice? Device { get; set; }
        public string Engine { get; set; } = "";
        public string Language { get; set; } = "";

        public SpeechRecognitionServiceManager(ServiceManager serviceManager) : base(serviceManager, ServiceName, "SpeechRecognitionServiceManager", "音声認識", 200, true) {
            speechRecognitionCheckBox = new CheckBox();
            speechRecognitionCheckBox.Text = "音声認識";
            speechRecognitionCheckBox.Appearance = Appearance.Button;
            speechRecognitionCheckBox.CheckedChanged += speechRecognitionCheckBox_CheckedChanged;
        }

        public override UserControl GetSettingPage() {
            return new SpeechRecognitionPage(ServiceManager, this);
        }

        public override Control GetTopPanelControl() {
            return speechRecognitionCheckBox;
        }

        public override void Load(JToken token) {
            var props = token.ToObject<Properties>();
            if (props != null) {
                this.Engine = props.Engine;
                this.Language = props.Language;

                // マイクを取得する
                var enumerator = new MMDeviceEnumerator();
                var micList = enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active);
                Device = micList.Where(mic => mic.ID == props.Device).FirstOrDefault();
                if (Device == null) {
                    Device = enumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Console);
                }
            }
        }

        public override JObject Save() {
            var props = new Properties() {
                Device = Device?.ID ?? "",
                Engine = Engine,
                Language = Language
            };
            return new JObject(Name, props);
        }

        public SpeechRecognitionService? GetEngine() {
            return this.ServiceManager.GetServices<SpeechRecognitionService>(ServiceName).Where(service => service.Name == this.Engine).First();
        }

        private void speechRecognitionCheckBox_CheckedChanged(object? sender, EventArgs e) {
            SetCheckBoxButtonColor(this.speechRecognitionCheckBox);

            if (speechRecognitionCheckBox.Checked) {
                SpeechRecognitionStart();
            } else {
                SpeechRecognitionStop();
            }
        }

        /**
         * 音声認識を開始する
         */
        private void SpeechRecognitionStart() {
            var recognitionStarted = false;

            if (Device == null) {
                MessageBox.Show("マイクを設定してください。");
            } else {
                var service = GetEngine();
                if (service != null) {
                    if (service.Start()) {
                        speechRecognitionService = service;
                        service.Recognizing += Recognizing;
                        service.Recognized += Recognized;
                        recognitionStarted = true;
                    }
                }
            }

            // 音声認識を開始できなかった場合、音声認識ボタンのチェックを外す。
            if (recognitionStarted == false) {
                this.speechRecognitionCheckBox.Checked = false;
            }
        }

        /**
         * 音声認識を終了する
         */
        private void SpeechRecognitionStop() {
            if (speechRecognitionService != null) {
                speechRecognitionService.Recognizing -= Recognizing;
                speechRecognitionService.Recognized -= Recognized;
                speechRecognitionService.Stop();
                speechRecognitionService = null;
            }
        }

        private void Recognizing(Object? sender, SpeechRecognitionEventArgs args) {
            AddInformationText(args.Text);
        }

        private void Recognized(Object? sender, SpeechRecognitionEventArgs args) {
            AddInformationText(args.Text);
        }
    }
}
