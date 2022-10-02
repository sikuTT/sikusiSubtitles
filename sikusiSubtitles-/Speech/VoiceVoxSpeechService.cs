using NAudio.Wave;
using Newtonsoft.Json;
using sikusiSubtitles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.Speech {
    public class Speaker {
        public string name = "";
        public string speaker_uuid = "";
        public List<Style> styles = new List<Style>();
        public string Version = "";

        public class Style {
            public int id = 0;
            public string name = "";
        }
    }
    
    public class VoiceVoxSpeechService : SpeechService {
        List<Tuple<string, string>> voices = new List<Tuple<string, string>>();
        HttpClient HttpClient = new HttpClient();
        WaveOut? waveOut;

        public string Url { get; set; } = "http://localhost";
        public int Port { get; set; } = 50021;

        public bool VoiceListInitialized { get; set; } = false;

        public VoiceVoxSpeechService(ServiceManager serviceManager) : base(serviceManager, "VOICEVOX", "VOICEVOX", 500) {
            Task task = GetSpeakers();
        }

        public override UserControl? GetSettingPage() {
            return new VoiceVoxSpeechPage(ServiceManager, this);
        }

        public override List<Tuple<string, string>> GetVoices() {
            return this.voices;
        }

        public override async Task SpeakAsync(string voice, string text) {
            var query = await CreateAudioQuery(voice, text);
            if (query != null) {
                var stream = await GetAudioData(voice, query);
                if (stream != null) {
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
            }
        }

        public override async Task CancelSpeakAsync() {
            await Task.Run(() => {
                if (waveOut != null) {
                    waveOut.Stop();
                }
            });
        }

        public async Task GetSpeakers() {
            try {
                this.voices.Clear();

                // Build the request.
                using var request = new HttpRequestMessage();
                request.Method = HttpMethod.Get;
                request.RequestUri = new Uri($"{Url}:{Port}/speakers");

                // Send the request and get response.
                HttpResponseMessage response = await this.HttpClient.SendAsync(request);

                var str = await response.Content.ReadAsStringAsync();
                if (str != null) {
                    var speakers = JsonConvert.DeserializeObject<Speaker[]>(str);
                    if (speakers != null) {
                        foreach (var speaker in speakers) {
                            speaker.styles.ForEach(style => {
                                var id = style.id.ToString();
                                var name = $"{speaker.name}（{style.name}）";
                                this.voices.Add(new Tuple<string, string>(id, name));
                            });
                        }
                    }
                }
            } catch (Exception ex) {
                Debug.WriteLine($"VoiceVoxSpeechService: {ex.Message}");
            } finally {
                VoiceListInitialized = true;
            }
        }

        private async Task<string?> CreateAudioQuery(string voice, string text) {
            try {
                // Request query
                var escapedVoice = Uri.EscapeDataString(voice);
                var escapedText= Uri.EscapeDataString(text);
                var builder = new UriBuilder($"{Url}:{Port}/audio_query");
                builder.Query = $"text={escapedText}&speaker={escapedVoice}";

                // Build the request.
                using var request = new HttpRequestMessage();
                request.Method = HttpMethod.Post;
                request.RequestUri = builder.Uri;
                request.Headers.Add("accept", "application/json");

                // Send the request and get response.
                HttpResponseMessage response = await this.HttpClient.SendAsync(request);

                var stream = await response.Content.ReadAsStreamAsync();
                var buffer = new byte[stream.Length];
                stream.Read(buffer);
                var str = Encoding.UTF8.GetString(buffer);
                return str;
                // return await response.Content.ReadAsStringAsync();
            } catch (Exception ex) {
                Debug.WriteLine($"VoiceVoxSpeechService: {ex.Message}");
            }
            return null;
        }

        private async Task<Stream?> GetAudioData(string voice, string query) {
            try {
                // Request query
                var escapedVoice = Uri.EscapeDataString(voice);
                var builder = new UriBuilder($"{Url}:{Port}/synthesis");
                builder.Query = $"speaker={escapedVoice}&enable_interrogative_upspeak=true";

                // Build the request.
                using var request = new HttpRequestMessage();
                request.Method = HttpMethod.Post;
                request.RequestUri = builder.Uri;
                request.Content = new StringContent(query, Encoding.UTF8, "application/json");

                // Send the request and get response.
                HttpResponseMessage response = await this.HttpClient.SendAsync(request);

                var stream = await response.Content.ReadAsStreamAsync();
                return stream;
            } catch (Exception ex) {
                Debug.WriteLine($"VoiceVoxSpeechService: {ex.Message}");
            }
            return null;
        }
    }
}
