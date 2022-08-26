using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.SpeechRecognition {
    public class SpeechRecognitionServiceManager : Service.ServiceManager {
        public static new readonly string ServiceName = "SpeechRecognition";
        public override string Name { get { return ServiceName; } }
    }
}
