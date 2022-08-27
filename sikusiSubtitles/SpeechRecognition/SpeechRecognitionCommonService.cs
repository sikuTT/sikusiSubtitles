using NAudio.CoreAudioApi;
using sikusiSubtitles.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.SpeechRecognition {
    public class SpeechRecognitionCommonService : Service.Service {
        public MMDevice? Device { get; set; }

        public SpeechRecognitionCommonService(ServiceManager serviceManager) : base(serviceManager, SpeechRecognitionService.SERVICE_NAME, "Common", "音声認識", 100) {
        }
    }
}
