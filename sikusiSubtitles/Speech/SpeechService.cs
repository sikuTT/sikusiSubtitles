using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.Speech {
    public abstract class SpeechService : Service {

        public SpeechService(ServiceManager serviceManager, string name, string displayName, int index) : base(serviceManager, SpeechServiceManager.ServiceName, name, displayName, index, false) {
        }

        public abstract List<Tuple<string, string>> GetVoices();
        public abstract Task SpeakAsync(string voice, string text);
    }
}
