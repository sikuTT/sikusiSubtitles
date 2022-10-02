using System;
using System.Collections.Generic;

namespace sikusiSubtitles.SpeechRecognition {
    public abstract class SpeechRecognitionService : sikusiSubtitles.Service {
        public event EventHandler<SpeechRecognitionEventArgs>? Recognizing;
        public event EventHandler<SpeechRecognitionEventArgs>? Recognized;

        public abstract List<Tuple<string, string>> GetLanguages();

        public SpeechRecognitionService(ServiceManager serviceManager, string name, string displayName, int index) : base(serviceManager, SpeechRecognitionServiceManager.ServiceName, name, displayName, index) {
        }

        public override void Finish() {
            Stop();
        }

        public abstract bool Start();
        public abstract void Stop();

        protected void InvokeRecognizing(string text) {
            this.Recognizing?.Invoke(this, new SpeechRecognitionEventArgs(text));
        }

        protected void InvokeRecognized(string text) {
            this.Recognized?.Invoke(this, new SpeechRecognitionEventArgs(text));
        }
    }
}
