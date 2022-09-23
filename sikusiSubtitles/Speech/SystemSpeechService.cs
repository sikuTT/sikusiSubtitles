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
            var synth = new SpeechSynthesizer();

            synth.SetOutputToDefaultAudioDevice();
            synth.SelectVoice(voice);
            await Task.Run(() => {
                synth.Speak(text);
            });
        }
    }
}
