using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
// using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace sikusiSubtitles.Speech {
    public class SapiSpeechService : SpeechService {
        List<Voice> voices = new List<Voice>();
        System.Speech.Synthesis.SpeechSynthesizer? sapiSynth;

        public SapiSpeechService(ServiceManager serviceManager) : base(serviceManager, "SapiSpeech", "Windows標準", 500) {
            var voices = Windows.Media.SpeechSynthesis.SpeechSynthesizer.AllVoices;
            foreach (var voice in voices) {
                var name = voice.DisplayName;
                this.voices.Add(new Voice("OneCore", voice.Id, voice.DisplayName, voice.Language, voice.Gender.ToString()));
            }

            var synth = new System.Speech.Synthesis.SpeechSynthesizer();
            var voices2 = synth.GetInstalledVoices();
            foreach (var voice in voices2) {
                this.voices.Add(new Voice("SAPI", voice.VoiceInfo.Id, voice.VoiceInfo.Name, voice.VoiceInfo.Culture.Name, voice.VoiceInfo.Gender.ToString()));
            }
        }

        public override UserControl? GetSettingPage() {
            return new SystemSpeechPage(ServiceManager, this);
        }

        public override List<Voice> GetVoices() {
            return this.voices;
        }

        public override async Task SpeakAsync(Voice voice, string text) {
            try {
                if (voice.Type == "OneCore") {
                    await SpeakOneCoreAsync(voice, text);
                } else {
                    await SpeakSapiAsync(voice, text);
                }
            } catch (Exception ex) {
                Debug.WriteLine($"SystemSpeechService: {ex.Message}");
            }
        }

        public override async Task CancelSpeakAsync() {
            if (sapiSynth != null) {
                await Task.Run(() => {
                    try {
                        sapiSynth.SpeakAsyncCancelAll();
                    } catch (Exception ex) {
                        Debug.WriteLine("CancelSpeakAsync: " + ex.Message);
                    }
                });
                sapiSynth = null;
            } else {
                await CancelSpeakFromStreamAsync();
            }
        }

        private async Task SpeakOneCoreAsync(Voice voice, string text) {
            var voices = Windows.Media.SpeechSynthesis.SpeechSynthesizer.AllVoices;
            var v = voices.Where(vo => vo.Id == voice.Id).FirstOrDefault();
            if (v != null) {
                var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();
                synth.Voice = v;
                using Windows.Media.SpeechSynthesis.SpeechSynthesisStream synthStream = await synth.SynthesizeTextToStreamAsync(text);
                using Stream stream = synthStream.AsStreamForRead();
                await SpeakFromStreamAsync(stream);
            }
        }

        private async Task SpeakSapiAsync(Voice voice, string text) {
            try {
                sapiSynth = new System.Speech.Synthesis.SpeechSynthesizer();
                sapiSynth.SetOutputToDefaultAudioDevice();
                sapiSynth.SelectVoice(voice.DisplayName);
                await Task.Run(() => {
                    try {
                        sapiSynth.Speak(text);
                    } catch (Exception ex) {
                        Debug.WriteLine($"SystemSpeechService: {ex.Message}");
                    }
                });
            } catch (Exception ex) {
                Debug.WriteLine($"SystemSpeechService: {ex.Message}");
            }
        }
    }
}
