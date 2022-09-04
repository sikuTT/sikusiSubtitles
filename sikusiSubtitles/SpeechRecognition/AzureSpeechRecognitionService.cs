using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using NAudio.CoreAudioApi;
using sikusiSubtitles.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.SpeechRecognition {
    public class AzureSpeechRecognitionService : SpeechRecognitionService {
        private SpeechRecognizer? Recognizer;

        public string? Key { get; set; }
        public string? Region { get; set; }
        public string? Language { get; set; }

        public AzureSpeechRecognitionService(ServiceManager serviceManager) : base(serviceManager, "Azure", "Azure Cognitive Service", 200) {
        }

        public override bool Start() {
            var commonService = this.ServiceManager.GetService<SpeechRecognitionCommonService>();
            if (commonService != null && commonService.Device != null) {
                if (this.Key != null && this.Region != null) {
                    var speechConfig = SpeechConfig.FromSubscription(this.Key, this.Region);
                    this.RecognizeFromMic(commonService.Device, speechConfig);
                    return true;
                }
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

        private void RecognizeFromMic(MMDevice device, SpeechConfig speechConfig) {
            var audioConfig = AudioConfig.FromMicrophoneInput(device.ID);
            this.Recognizer = new SpeechRecognizer(speechConfig, this.Language, audioConfig);

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
    }
}
