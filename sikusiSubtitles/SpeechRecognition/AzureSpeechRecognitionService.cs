using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.SpeechRecognition {
    public class AzureSpeechRecognitionService : SpeechRecognitionService {
        private SpeechRecognizer? Recognizer;

        public string Key { get; set; } = "";
        public string Region { get; set; } = "";

        public AzureSpeechRecognitionService(ServiceManager serviceManager) : base(serviceManager, "Azure", "Azure Cognitive Service - Speech Service", 200) {
            SettingPage = new AzureSpeechRecognitionPage(serviceManager, this);
        }

        public override void Load() {
            this.Key = Properties.Settings.Default.AzureSpeechRecognitionKey;
            this.Region = Properties.Settings.Default.AzureSpeechRecognitionRegion;
        }

        public override void Save() {
            Properties.Settings.Default.AzureSpeechRecognitionKey = this.Key;
            Properties.Settings.Default.AzureSpeechRecognitionRegion = this.Region;
        }

        public override List<Tuple<string, string>> GetLanguages() {
            return this.Languages;
        }

        public override bool Start() {
            if (Key == "") {
                MessageBox.Show("APIキーが設定されていません。", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            } else if (Region == "") {
                MessageBox.Show("地域が設定されていません。", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            var manager = this.ServiceManager.GetManager<SpeechRecognitionServiceManager>();
            if (manager?.Device != null) {
                var speechConfig = SpeechConfig.FromSubscription(this.Key, this.Region);
                this.RecognizeFromMic(manager, speechConfig);
                return true;
            }
            return false;
        }

        public override async void Stop() {
            if (this.Recognizer != null) {
                await this.Recognizer.StopContinuousRecognitionAsync();
                this.Recognizer.Recognizing -= RecognizingHandler;
                this.Recognizer.Recognized -= RecognizedHandler;
                this.Recognizer.Canceled -= CanceledHandler;
                this.Recognizer = null;
            }
        }

        private void RecognizeFromMic(SpeechRecognitionServiceManager manager, SpeechConfig speechConfig) {
            var audioConfig = AudioConfig.FromMicrophoneInput(manager.Device?.ID);
            this.Recognizer = new SpeechRecognizer(speechConfig, manager.Language, audioConfig);

            this.Recognizer.Recognizing += RecognizingHandler;
            this.Recognizer.Recognized += RecognizedHandler;
            this.Recognizer.Canceled += CanceledHandler;

            //Asks user for mic input and prints transcription result on screen
            Debug.WriteLine("Speak into your microphone.");
            this.Recognizer.StartContinuousRecognitionAsync();
        }

        private void RecognizingHandler(Object? sender, Microsoft.CognitiveServices.Speech.SpeechRecognitionEventArgs args) {
            Debug.WriteLine("Recognizing: " + args.Result.Reason.ToString() + ", " + args.Result.Text);
            this.InvokeRecognizing(args.Result.Text);
        }

        private void RecognizedHandler(Object? sender, Microsoft.CognitiveServices.Speech.SpeechRecognitionEventArgs args) {
            Debug.WriteLine("Recognized: " + args.Result.Reason.ToString() + ", " + args.Result.Text);
            this.InvokeRecognized(args.Result.Text);
        }

        private void CanceledHandler(Object? sender, Microsoft.CognitiveServices.Speech.SpeechRecognitionCanceledEventArgs args) {
            Debug.WriteLine("Canceled: " + args.ErrorCode + ", " + args.ErrorDetails + ", " + args.Reason.ToString());
        }

        List<Tuple<string, string>> Languages = new List<Tuple<string, string>>() {
            // new Tuple<string, string>("", "Arabic (Bahrain) modern standard"),
            // new Tuple<string, string>("", "Arabic (Egypt)"),
            // new Tuple<string, string>("", "Arabic (Kuwait)"),
            // new Tuple<string, string>("", "Arabic (Qatar)"),
            // new Tuple<string, string>("", "Arabic (Saudi Arabia)"),
            // new Tuple<string, string>("", "Arabic (Syria)"),
            // new Tuple<string, string>("", "Arabic (UAE)"),
            // new Tuple<string, string>("", "Catalan"),
            // new Tuple<string, string>("", "Chinese (Cantonese Traditional)"),
            new Tuple<string, string>("zh-CN", "Chinese (Mandarin simplified)"),
            new Tuple<string, string>("zh-TW", "Chinese (Taiwanese Mandarin)"),
            // new Tuple<string, string>("", "Danish (Denmark)"),
            // new Tuple<string, string>("", "Dutch (Netherlands)"),
            // new Tuple<string, string>("", "English (Australia)"),
            // new Tuple<string, string>("", "English (Canada)"),
            // new Tuple<string, string>("", "English (India)"),
            // new Tuple<string, string>("", "English (New Zealand)"),
            // new Tuple<string, string>("", "English (United Kingdom)"),
            new Tuple<string, string>("en-US", "English (United States)"),
            // new Tuple<string, string>("", "Finnish (Finland)"),
            // new Tuple<string, string>("", "French (Canada)"),
            // new Tuple<string, string>("", "French (France)"),
            // new Tuple<string, string>("", "German (Germany)"),
            // new Tuple<string, string>("", "Gujarati (Indian)"),
            // new Tuple<string, string>("", "Hindi (India)"),
            // new Tuple<string, string>("", "Italian (Italy)"),
            new Tuple<string, string>("ja-JP", "Japanese (Japan)"),
            new Tuple<string, string>("ko-KR", "Korean (Korea)"),
            // new Tuple<string, string>("", "Marathi (India)"),
            // new Tuple<string, string>("", "Norwegian (Bokmål) (Norway)"),
            // new Tuple<string, string>("", "Polish (Poland)"),
            new Tuple<string, string>("pt-BR", "Portuguese (Brazil)"),
            new Tuple<string, string>("pt-PT", "Portuguese (Portugal)"),
            // new Tuple<string, string>("", "Russian (Russia)"),
            // new Tuple<string, string>("", "Spanish (Mexico)"),
            new Tuple<string, string>("es-ES", "Spanish (Spain)"),
            // new Tuple<string, string>("", "Swedish (Sweden)"),
            // new Tuple<string, string>("", "Tamil (India)"),
            // new Tuple<string, string>("", "Telugu (India)"),
            // new Tuple<string, string>("", "Thai (Thailand)"),
            // new Tuple<string, string>("", "Turkish (Turkey)"),
        };
    }
}
