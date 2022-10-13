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

        public AzureSpeechRecognitionService(ServiceManager serviceManager) : base(serviceManager, "AzureSpeechRecognition", "Azure Cognitive Services", 200) {
        }

        public override void Init() {
            settingsPage = new AzureSpeechRecognitionPage(ServiceManager, this);
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
            // new Language("ar-BH", "Arabic (Bahrain) modern standard"),
            // new Language("ar-EG", "Arabic (Egypt)"),
            // new Language("ar-KW", "Arabic (Kuwait)"),
            // new Language("ar-QA", "Arabic (Qatar)"),
            // new Language("ar-SA", "Arabic (Saudi Arabia)"),
            // new Language("ar-SY", "Arabic (Syria)"),
            // new Language("ar-AE", "Arabic (UAE)"),
            // new Language("ca-ES", "Catalan"),
            // new Language("zh-HK", "Chinese (Cantonese Traditional)"),
            new Language("zh-CN", "中国語(簡体字)"),
            new Language("zh-TW", "中国語(繁体字)"),
            // new Language("da-DK", "Danish (Denmark)"),
            // new Language("nl-NL", "Dutch (Netherlands)"),
            // new Language("en-AU", "English (Australia)"),
            // new Language("en-CA", "English (Canada)"),
            // new Language("en-IN", "English (India)"),
            // new Language("en-NZ", "English (New Zealand)"),
            // new Language("en-GB", "English (United Kingdom)"),
            new Language("en-US", "英語（アメリカ）"),
            // new Language("fi-FI", "Finnish (Finland)"),
            // new Language("fr-CA", "French (Canada)"),
            new Language("fr-FR", "フランス語（フランス）"),
            // new Language("de-DE", "German (Germany)"),
            // new Language("gu-IN", "Gujarati (Indian)"),
            // new Language("hi-IN", "Hindi (India)"),
            new Language("it-IT", "イタリア語（イタリア）"),
            new Language("ja-JP", "日本語"),
            new Language("ko-KR", "韓国語"),
            // new Language("mr-IN", "Marathi (India)"),
            // new Language("nb-NO", "Norwegian (Bokmål) (Norway)"),
            // new Language("pl-PL", "Polish (Poland)"),
            new Language("pt-BR", "ポルトガル語（ブラジル）"),
            new Language("pt-PT", "ポルトガル語（ポルトガル）"),
            new Language("ru-RU", "ロシア語（ロシア）"),
            // new Language("es-MX", "Spanish (Mexico)"),
            new Language("es-ES", "スペイン語（スペイン）"),
            // new Language("sv-SE", "Swedish (Sweden)"),
            // new Language("ta-IN", "Tamil (India)"),
            // new Language("te-IN", "Telugu (India)"),
            new Language("th-TH", "タイ語（タイ）"),
            // new Language("tr-TR", "Turkish (Turkey)"),
        };
    }
}
