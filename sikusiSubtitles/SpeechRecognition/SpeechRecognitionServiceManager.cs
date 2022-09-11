using NAudio.CoreAudioApi;

namespace sikusiSubtitles.SpeechRecognition {
    public class SpeechRecognitionServiceManager : sikusiSubtitles.Service {
        public static new string ServiceName = "SpeechRecognition";

        public MMDevice? Device { get; set; }
        public string Engine { get; set; } = "";
        public string Language { get; set; } = "";

        public SpeechRecognitionServiceManager(ServiceManager serviceManager) : base(serviceManager, ServiceName, ServiceName, "音声認識", 200, true) {
            SettingPage = new SpeechRecognitionPage(serviceManager, this);
        }

        public override void Load() {
            // マイク設定
            var enumerator = new MMDeviceEnumerator();
            var micList = enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active);
            Device = micList.Where(mic => mic.ID == Properties.Settings.Default.MicID).FirstOrDefault();
            if (Device == null) {
                Device = enumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Console);
            }

            // 音声認識エンジン
            Engine = Properties.Settings.Default.SpeechRecognitionEngine;
            Language = Properties.Settings.Default.SpeechRecognitionLanguage;
        }

        public override void Save() {
            Properties.Settings.Default.MicID = Device?.ID ?? "";
            Properties.Settings.Default.SpeechRecognitionEngine = Engine;
            Properties.Settings.Default.SpeechRecognitionLanguage = Language;
        }

        public SpeechRecognitionService? GetEngine() {
            return this.ServiceManager.GetServices<SpeechRecognitionService>(ServiceName).Where(service => service.Name == Engine).First();
        }
    }
}
