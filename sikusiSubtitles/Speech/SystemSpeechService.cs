using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.Speech {
    public class SystemSpeechService : SpeechService {
        List<Tuple<string, string>> voices = new List<Tuple<string, string>>();
        SpeechSynthesizer synth = new SpeechSynthesizer();

        public SystemSpeechService(ServiceManager serviceManager) : base(serviceManager, "SystemSpeech", "システム標準", 500) {
            var synth = new SpeechSynthesizer();
            var voices = synth.GetInstalledVoices();
            foreach (var voice in voices) {
                this.voices.Add(new Tuple<string, string>(voice.VoiceInfo.Name, voice.VoiceInfo.Name));
            }
        }

        public override UserControl? GetSettingPage() {
            return new SystemSpeechPage(ServiceManager, this);
        }

        public override List<Tuple<string, string>> GetVoices() {
            return this.voices;
        }

        public override async Task SpeakAsync(string voice, string text) {
            try {
                synth.SetOutputToDefaultAudioDevice();
                synth.SelectVoice(voice);
                await Task.Run(() => {
                    try {
                        synth.Speak(text);
                    } catch (Exception ex) {
                        Debug.WriteLine($"SystemSpeechService: {ex.Message}");
                    }
                });
            } catch (Exception ex) {
                Debug.WriteLine($"SystemSpeechService: {ex.Message}");
            }
        }

        public override async Task CancelSpeakAsync() {
            await Task.Run(() => synth.SpeakAsyncCancelAll());
        }
    }
}
