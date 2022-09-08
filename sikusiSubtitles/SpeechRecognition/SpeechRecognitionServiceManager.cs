using NAudio.CoreAudioApi;
using sikusiSubtitles.Service;

namespace sikusiSubtitles.SpeechRecognition {
    public class SpeechRecognitionServiceManager : Service.Service {
        public static new string ServiceName = "SpeechRecognition";

        public MMDevice? Device { get; set; }
        public string? Engine { get; set; }
        public string? Language { get; set; }

        public override void Load() {
            // マイク設定
            var enumerator = new MMDeviceEnumerator();
            var micList = enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active);
            Device = micList.Where(mic => mic.ID == Properties.Settings.Default.MicID).First();
            if (Device == null) {
                Device = enumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Console);
            }

            // 音声認識エンジン
            Engine = Properties.Settings.Default.RecognitionEngine;
        }

        public override void Save() {
            Properties.Settings.Default.MicID = Device?.ID ?? "";
            Properties.Settings.Default.RecognitionEngine = Engine ?? "";
        }

        public SpeechRecognitionServiceManager(ServiceManager serviceManager) : base(serviceManager, ServiceName, ServiceName, "音声認識", 100, true) {
        }

        public SpeechRecognitionService? GetEngine() {
            return this.ServiceManager.GetServices<SpeechRecognitionService>(ServiceName).Where(service => service.Name == Engine).First();
        }
    }
}
