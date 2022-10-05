using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using NAudio.CoreAudioApi;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace sikusiSubtitles.SpeechRecognition {
    public class AzureSpeechRecognitionService : SpeechRecognitionService {
        private SpeechRecognizer? Recognizer;

        public string Key { get; set; } = "";
        public string Region { get; set; } = "";

        public AzureSpeechRecognitionService(ServiceManager serviceManager) : base(serviceManager, "AzureSpeechRecognition", "Azure Cognitive Service - Speech To Text", 200) {
        }

        public override UserControl? GetSettingPage()
        {
            return new AzureSpeechRecognitionPage(ServiceManager, this);
        }

        public override void Load(JToken token) {
            this.Key = Decrypt(token.Value<string>("Key") ?? "");
            this.Region = token.Value<string>("Region") ?? "";
        }

        public override JObject Save() {
            return new JObject {
                new JProperty("Key", Encrypt(Key)),
                new JProperty("Region", Region)
            };
        }

        public override List<Language> GetLanguages() {
            return this.Languages;
        }

        public override bool Start() {
            if (Key == "") {
                MessageBox.Show("APIキーが設定されていません。", null, MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            } else if (Region == "") {
                MessageBox.Show("地域が設定されていません。", null, MessageBoxButton.OK, MessageBoxImage.Error);
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

        List<Language> Languages = new List<Language>() {
            // new Language("", "Arabic (Bahrain) modern standard"),
            // new Language("", "Arabic (Egypt)"),
            // new Language("", "Arabic (Kuwait)"),
            // new Language("", "Arabic (Qatar)"),
            // new Language("", "Arabic (Saudi Arabia)"),
            // new Language("", "Arabic (Syria)"),
            // new Language("", "Arabic (UAE)"),
            // new Language("", "Catalan"),
            // new Language("", "Chinese (Cantonese Traditional)"),
            new Language("zh-CN", "Chinese (Mandarin simplified)"),
            new Language("zh-TW", "Chinese (Taiwanese Mandarin)"),
            // new Language("", "Danish (Denmark)"),
            // new Language("", "Dutch (Netherlands)"),
            // new Language("", "English (Australia)"),
            // new Language("", "English (Canada)"),
            // new Language("", "English (India)"),
            // new Language("", "English (New Zealand)"),
            // new Language("", "English (United Kingdom)"),
            new Language("en-US", "English (United States)"),
            // new Language("", "Finnish (Finland)"),
            // new Language("", "French (Canada)"),
            // new Language("", "French (France)"),
            // new Language("", "German (Germany)"),
            // new Language("", "Gujarati (Indian)"),
            // new Language("", "Hindi (India)"),
            // new Language("", "Italian (Italy)"),
            new Language("ja-JP", "Japanese (Japan)"),
            new Language("ko-KR", "Korean (Korea)"),
            // new Language("", "Marathi (India)"),
            // new Language("", "Norwegian (Bokmål) (Norway)"),
            // new Language("", "Polish (Poland)"),
            new Language("pt-BR", "Portuguese (Brazil)"),
            new Language("pt-PT", "Portuguese (Portugal)"),
            // new Language("", "Russian (Russia)"),
            // new Language("", "Spanish (Mexico)"),
            new Language("es-ES", "Spanish (Spain)"),
            // new Language("", "Swedish (Sweden)"),
            // new Language("", "Tamil (India)"),
            // new Language("", "Telugu (India)"),
            // new Language("", "Thai (Thailand)"),
            // new Language("", "Turkish (Turkey)"),
        };
    }
}
