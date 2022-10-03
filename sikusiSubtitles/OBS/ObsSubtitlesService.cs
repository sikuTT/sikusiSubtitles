using Newtonsoft.Json.Linq;
using ObsWebSocket5.Message.Data.InputSettings;
using sikusiSubtitles.SpeechRecognition;
using sikusiSubtitles.Subtitles;
using sikusiSubtitles.Translation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.OBS {
    public class ObsSubtitlesService : sikusiSubtitles.Service {
        public string VoiceTarget { get; set; } = "";
        public List<string> TranslateTargetList { get; set; } = new List<string>();

        public ObsSubtitlesService(ServiceManager serviceManager) : base(serviceManager, ObsServiceManager.ServiceName, "ObsSubtitles", "字幕", 200) {
        }

        // Services
        private ObsService? obsService;
        private SubtitlesService? subtitlesService;

        private Dictionary<string, string> previousTexts = new Dictionary<string, string>();

        public override UserControl? GetSettingPage() {
            return new ObsSubtitlesPage(ServiceManager, this);
        }

        public override void Init() {
            obsService = ServiceManager.GetService<ObsService>();
            if (obsService != null) {
                obsService.ConnectionChanged += ObsConnectionChanged;
            }

            subtitlesService = ServiceManager.GetService<SubtitlesService>();
            if (subtitlesService != null) {
                subtitlesService.SubtitlesChanged += SubtitlesChanged;
            }
        }

        public override void Load(JToken token) {
            VoiceTarget = token.Value<string>("VoiceTarget") ?? "";
            var targetList = token.Value<JArray>("TranslateTargetList");
            if (targetList != null) {
                foreach (var to in targetList) {
                    TranslateTargetList.Add(to.ToString());
                }
            }
        }

        public override JObject Save() {
            return new JObject {
                new JProperty("VoiceTarget", VoiceTarget),
                new JProperty("TranslateTargetList", TranslateTargetList),
            };
        }

        async public Task SetTextAsync(string sourceName, string text) {
            if (obsService != null && sourceName != "") {
                var voiceSettings = await obsService.ObsSocket.GetInputSettingsAsync(sourceName);
                var voiceTextSource = voiceSettings?.inputSettings as TextGdiplusV2;
                if (voiceTextSource != null) {
                    voiceTextSource.text = text;
                    await obsService.ObsSocket.SetInputSettingsAsync(sourceName, voiceTextSource);
                }
            }
        }

        private void ObsConnectionChanged(Object? sender, bool connected) {
        }

        private async void SubtitlesChanged(Object? sender, List<SubtitlesText> subtitlesTexts) {
            if (subtitlesService != null && obsService != null && obsService?.IsConnected == true) {
                Dictionary<string, StringBuilder> texts = new Dictionary<string, StringBuilder>();
                texts.Add("", new StringBuilder());
                foreach (var to in subtitlesService.TranslationLanguageToList) {
                    if (to != "") {
                        texts.Add(to, new StringBuilder());
                    }
                }

                foreach (SubtitlesText subtitleText in subtitlesTexts) {
                    var voiceText = texts[""];
                    if (voiceText.Length > 0) voiceText.Append(' ');
                    if (subtitleText.Recognized) {
                        voiceText.Append(subtitleText.VoiceText);
                    } else {
                        voiceText.Append($"（{subtitleText.VoiceText}）");
                    }

                    foreach (var translationText in subtitleText.TranslationTexts) {
                        if (translationText.Language != "") {
                            var text = texts.GetValueOrDefault(translationText.Language);
                            if (text == null) {
                                texts[translationText.Language] = text = new StringBuilder();
                            }
                            if (text.Length > 0) text.Append(' ');
                            text.Append(translationText.Text);
                        }
                    }
                }

                foreach (var text in texts) {
                    try {
                        string? sourceName = null;
                        if(text.Key == "") {
                            sourceName = VoiceTarget;
                        } else {
                            var i = subtitlesService.TranslationLanguageToList.FindIndex(to => to == text.Key);
                            if (i != -1 && i < TranslateTargetList.Count) {
                                sourceName = TranslateTargetList[i];
                            }
                        }
                        if (sourceName != null) {
                            var newText = text.Value.ToString();
                            string? previousText;
                            if (previousTexts.TryGetValue(sourceName, out previousText) == false || previousText != newText) {
                                previousTexts[sourceName] = newText;
                                await SetTextAsync(sourceName, newText);
                            }
                        }
                    }  catch (Exception ex) {
                        Debug.WriteLine("ObsSubtitlesService: " + ex.Message);
                    }
                }
            }
        }
    }
}
