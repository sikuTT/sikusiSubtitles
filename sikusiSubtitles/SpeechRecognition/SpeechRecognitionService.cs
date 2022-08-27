using sikusiSubtitles.Service;

namespace sikusiSubtitles.SpeechRecognition {
    public abstract class SpeechRecognitionService : Service.Service {
        public static string SERVICE_NAME = "SpeechRecognition";

        public event EventHandler<SpeechRecognitionEventArgs>? Recognizing;
        public event EventHandler<SpeechRecognitionEventArgs>? Recognized;

        public SpeechRecognitionService(ServiceManager serviceManager, string name, string displayName, int index) : base(serviceManager, SERVICE_NAME, name, displayName, index) {
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
