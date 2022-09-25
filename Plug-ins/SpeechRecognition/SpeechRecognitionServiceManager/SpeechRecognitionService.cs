namespace sikusiSubtitles.SpeechRecognition {
    public abstract class SpeechRecognitionService : sikusiSubtitles.Service {
        public event EventHandler<SpeechRecognitionEventArgs>? Recognizing;
        public event EventHandler<SpeechRecognitionEventArgs>? Recognized;

        public abstract List<Tuple<string, string>> GetLanguages();

        public SpeechRecognitionService(ServiceManager serviceManager, string name, string displayName, int index) : base(serviceManager, SpeechRecognitionServiceManager.ServiceName, name, displayName, index) {}

        /**
         * サービスの後処理を行う。
         * 音声認識中の場合は音声認識を終了する。
         */
        public override void Finish() {
            Stop();
        }

        /**
         * 音声認識を開始する
         */
        public abstract bool Start();

        /**
         * 音声認識を終了する
         */
        public abstract void Stop();

        protected void InvokeRecognizing(string text) {
            this.Recognizing?.Invoke(this, new SpeechRecognitionEventArgs(text));
        }

        protected void InvokeRecognized(string text) {
            this.Recognized?.Invoke(this, new SpeechRecognitionEventArgs(text));
        }
    }
}
