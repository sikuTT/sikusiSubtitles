using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.SpeechRecognition {
    public abstract class SpeechRecognitionService : Service.Service {
        public static string SERVICE_NAME = "SpeechRecognition";

        public event EventHandler<SpeechRecognitionEventArgs>? Recognizing;
        public event EventHandler<SpeechRecognitionEventArgs>? Recognized;

        public SpeechRecognitionService(string name, string displayName, int index) : base(SERVICE_NAME, name, displayName, index) {
        }

        protected void InvokeRecognizing(string text) {
            this.Recognizing?.Invoke(this, new SpeechRecognitionEventArgs(text));
        }

        protected void InvokeRecognized(string text) {
            this.Recognized?.Invoke(this, new SpeechRecognitionEventArgs(text));
        }
    }
}
