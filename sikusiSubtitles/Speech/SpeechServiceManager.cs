using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.Speech {
    public class SpeechServiceManager : Service {
        public static new string ServiceName = "Speech";

        public SpeechServiceManager(ServiceManager serviceManager) : base(serviceManager, ServiceName, ServiceName, "読み上げ", 600, true) {
        }
    }
}
