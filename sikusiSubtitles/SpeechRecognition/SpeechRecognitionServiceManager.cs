﻿using NAudio.CoreAudioApi;
using Newtonsoft.Json.Linq;

namespace sikusiSubtitles.SpeechRecognition {
    public class SpeechRecognitionServiceManager : sikusiSubtitles.Service {
        public static new string ServiceName = "SpeechRecognition";

        public MMDevice? Device { get; set; }
        public string Engine { get; set; } = "";
        public string Language { get; set; } = "";

        public SpeechRecognitionServiceManager(ServiceManager serviceManager) : base(serviceManager, ServiceName, ServiceName, "音声認識", 200, true) {
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
    }
}
