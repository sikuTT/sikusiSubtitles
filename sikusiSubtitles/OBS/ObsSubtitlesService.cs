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
using System.Windows.Controls;

namespace sikusiSubtitles.OBS {
    public class ObsSubtitlesService : sikusiSubtitles.Service {
        public string VoiceTarget { get; set; } = "";
        public List<string> TranslateTargetList { get; set; } = new List<string>();
        public bool DisplayRecognizingText { get; set; } = true;

        public ObsSubtitlesService(ServiceManager serviceManager) : base(serviceManager, SubtitlesServiceManager.ServiceName, "ObsSubtitles", "OBS", 200) {
            settingsPage = new ObsSubtitlesPage(ServiceManager, this);
        }

        // Services
        private ObsService? obsService;
        private SubtitlesService? subtitlesService;

        private Dictionary<string, TextGdiplusV2> textSettings = new Dictionary<string, TextGdiplusV2>();
        private Dictionary<string, string> previousTexts = new Dictionary<string, string>();

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

        public async Task SetTextAsync(string sourceName, string text) {
            if (obsService != null && sourceName != "") {
                TextGdiplusV2? textSettings;
                if (this.textSettings.TryGetValue(sourceName, out textSettings) == true) {
                    if (textSettings.text != text) {
                        textSettings.text = text;
                        await obsService.ObsSocket.SetInputSettingsAsync(sourceName, textSettings);
                    }
                }
            }
        }

        private async void ObsConnectionChanged(Object? sender, bool connected) {
            textSettings.Clear();
            if (connected) {
                if (obsService?.ObsSocket != null) {
                    // 音声字幕のGDIテキスト設定を取得
                    var voiceSubtitlesSettings = await obsService.ObsSocket.GetInputSettingsAsync(VoiceTarget);
                    textSettings.Add(VoiceTarget, (TextGdiplusV2)voiceSubtitlesSettings.inputSettings);

                    // 翻訳字幕のGDIテキスト設定を取得
                    foreach (var target in TranslateTargetList) {
                        var translatedSubtitlesSettings = await obsService.ObsSocket.GetInputSettingsAsync(target);
                        textSettings.Add(target, (TextGdiplusV2)translatedSubtitlesSettings.inputSettings);
                    }
                }
            }
        }

        private async void SubtitlesChanged(Object? sender, List<SubtitlesText> subtitlesTexts) {
            if (subtitlesService != null && obsService != null && obsService?.IsConnected == true) {
                // 言語ごとの字幕の作成先
                Dictionary<string, StringBuilder> texts = new Dictionary<string, StringBuilder>();
                // 音声用字幕
                texts.Add("", new StringBuilder());
                // 翻訳字幕
                foreach (var to in subtitlesService.TranslationLanguageToList) {
                    if (to != "") {
                        texts.Add(to, new StringBuilder());
                    }
                }

                // 字幕作成
                foreach (var subtitlesText in subtitlesTexts) {
                    var voiceText = texts[""];
                    // 音声字幕
                    if (voiceText.Length > 0) voiceText.Append(' ');
                    if (subtitlesText.Recognized) {
                        voiceText.Append(subtitlesText.VoiceText);
                    } else if (this.DisplayRecognizingText) {
                        // 音声が確定していない場合、音声をカッコで囲う
                        voiceText.Append($"（{subtitlesText.VoiceText}）");
                    }

                    // 翻訳字幕
                    foreach (var translationText in subtitlesText.TranslationTexts) {
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

                // 字幕表示
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
