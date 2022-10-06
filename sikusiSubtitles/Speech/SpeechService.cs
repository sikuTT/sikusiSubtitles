using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace sikusiSubtitles.Speech {
    public abstract class SpeechService : Service {
        WaveOut? waveOut;

        public SpeechService(ServiceManager serviceManager, string name, string displayName, int index) : base(serviceManager, SpeechServiceManager.ServiceName, name, displayName, index, false) {
        }

        public abstract List<Voice> GetVoices();
        public abstract Task SpeakAsync(Voice voice, string text);
        public abstract Task CancelSpeakAsync();

        protected async Task SpeakFromStreamAsync(Stream stream) {
            var reader = new WaveFileReader(stream);
            waveOut = new WaveOut();
            waveOut.Init(reader);
            waveOut.Play();
            await Task.Run(() => {
                while (waveOut.PlaybackState == PlaybackState.Playing) {
                    Thread.Sleep(100);
                }
            });
        }

        protected async Task CancelSpeakFromStreamAsync() {
            await Task.Run(() => {
                if (waveOut != null) {
                    waveOut.Stop();
                }
            });
        }
    }
}
