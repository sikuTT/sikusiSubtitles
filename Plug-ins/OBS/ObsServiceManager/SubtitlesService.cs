using Newtonsoft.Json.Linq;
using ObsWebSocket5.Message.Data.InputSettings;
using sikusiSubtitles.SpeechRecognition;
using sikusiSubtitles.Translation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.OBS {
    class SubtitlesText {
        public SubtitlesText(string text, bool recognized) {
            Text = text;
            Recognized = recognized;
        }
        public string Text { get; set; }
        public bool Recognized { get; set; }
    }

    public class SubtitlesService : sikusiSubtitles.Service {
        public class Properties {
            public string TranslationEngine { get; set; } = "";
            public string TranslationLanguageFrom { get; set; } = "";
            public string[] TranslationLanguageTo { get; set; } = new string[TranslationMaxCount];
            public bool[] Translation { get; set; } = new bool[TranslationMaxCount];
            public string VoiceTarget { get; set; } = "";
            public string[] TranslationTargets { get; set; } = new string[TranslationMaxCount];
            public bool IsClearInterval { get; set; } = false;
            public int ClearInterval { get; set; }
            public bool IsAdditionalTime { get; set; } = false;
            public int AdditionalTime { get; set; }
        }

        private static int TranslationMaxCount = 2;

        private ObsService? obsService;
        private SpeechRecognitionService? speechRecognitionService;
        private TranslationService? translationService;

        private Dictionary<string, string> recognizedText = new Dictionary<string, string>();
        private Dictionary<string, string> recognizingText = new Dictionary<string, string>();
        private Dictionary<string, System.Timers.Timer> clearTimer = new Dictionary<string, System.Timers.Timer>();

        private Properties props = new Properties();

        public Properties Props {
            get { return props; }
        }

        public SubtitlesService(ServiceManager serviceManager) : base(serviceManager, ObsServiceManager.ServiceName, "Subtitles", "字幕", 200) {
        }

        public override UserControl? GetSettingPage() {
            return new SubtitlesPage(ServiceManager, this);
        }

        public override void Load(JToken token) {
            var props = token.ToObject<Properties>();
            if (props != null) {
                this.props = props;
            }
        }

        public override JObject Save() {
            return new JObject(Name, props);
        }

        public bool Start(ObsService obsService) {
            this.obsService = obsService;

            var manager = this.ServiceManager.GetManager<SpeechRecognitionServiceManager>();
            if (manager != null) {
                this.speechRecognitionService = manager.GetEngine();

                if (this.speechRecognitionService != null) {
                    this.speechRecognitionService.Recognizing += Recognizing;
                    this.speechRecognitionService.Recognized += Recognized;

                    var translationServices = this.ServiceManager.GetServices<TranslationService>();
                    this.translationService = translationServices.Where(service => service.Name == props.TranslationEngine).FirstOrDefault();

                    return true;
                }
            }
            return false;
        }

        public void Stop() {
            if (this.speechRecognitionService != null) {
                this.speechRecognitionService.Recognizing -= Recognizing;
                this.speechRecognitionService.Recognized -= Recognized;
                this.speechRecognitionService = null;
            }
        }

        async public Task SetTextAsync(string sourceName, string text) {
            if (this.obsService == null) {
                return;
            }
            try {
                if (obsService.IsConnected) {
                    var obsSocket = this.obsService.ObsSocket;
                    var response = await obsSocket.GetInputSettingsAsync(sourceName);
                    var settings = response.inputSettings as TextGdiplusV2;
                    if (settings != null) {
                        settings.text = text;
                        await obsSocket.SetInputSettingsAsync(sourceName, settings);
                    }
                }
            } catch (Exception ex) {
                Debug.WriteLine("SubtitlesService.SetText: " + ex.Message);
            }
        }

        async private void Recognizing(object? sender, SpeechRecognitionEventArgs args) {
            if (this.obsService != null && this.obsService.IsConnected) {
                if (args.Text != "") {
                    await SetSubtitlesAsync(args.Text, props.VoiceTarget, false);
                }
            }
        }

        async private void Recognized(object? sender, SpeechRecognitionEventArgs args) {
            if (this.obsService != null && this.obsService.IsConnected) {
                if (args.Text != "") {
                    // 字幕を表示する。
                    await SetSubtitlesAsync(args.Text, props.VoiceTarget, true, props.ClearInterval, props.AdditionalTime);

                    // 翻訳サービスが設定されている場合、翻訳する
                    await TranslateAsync(args.Text);
                }
            }
        }

        async private Task SetSubtitlesAsync(string text, string target, bool recognized, int? timeout = null, int? additionalTimeout = null) {
            if (this.obsService == null || this.obsService.IsConnected == false) {
                return;
            }

            try {
                if (this.recognizedText.ContainsKey(target) == false) {
                    this.recognizedText.Add(target, "");
                }
                if (this.recognizingText.ContainsKey(target) == false) {
                    this.recognizingText.Add(target, "");
                }

                // 字幕を追加もしくは既存字幕を更新
                if (recognized) {
                    this.recognizedText[target] = text;
                    this.recognizingText[target] = "";
                } else {
                    this.recognizingText[target] = text;
                }

                // 字幕を表示
                await this.SetTextAsync(target, CreateSubtitlesText(target));

                // 字幕削除のタイマーを作成する。
                if (timeout != null) {
                    double timeoutms = (double)timeout * 1000;
                    if (additionalTimeout != null) {
                        timeoutms += ((double)additionalTimeout / 80) * text.Length * 1000;
                    }

                    // 既存のタイマーがある場合はタイマーを削除
                    // （タイマーで字幕が消える前に次の字幕が設定された場合、前の字幕は置き換えられたのでタイマーも不要になる）
                    if (clearTimer.ContainsKey(target)) {
                        var oldTimer = clearTimer[target];
                        oldTimer.Close();
                        clearTimer.Remove(target);
                    }

                    // 字幕自動クリア用のタイマーを作成する
                    var timer = new System.Timers.Timer(timeoutms);
                    timer.AutoReset = false;
                    timer.Elapsed += ClearSubtitles;
                    timer.Start();
                    this.clearTimer.Add(target, timer);
                }
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message);
            }
        }

        // 翻訳サービスが設定されている場合、翻訳する
        private async Task TranslateAsync(string text) {
            if (this.translationService != null) {
                var toList = new List<string>();
                for (var i = 0; i < TranslationMaxCount; i++) {
                    if (props.Translation[i] == true && props.TranslationLanguageTo[i] != null && props.TranslationLanguageTo[i] != "") {
                        toList.Add(props.TranslationLanguageTo[i]);
                    }
                }
                var result = await this.translationService.TranslateAsync(text, this.props.TranslationLanguageFrom, toList.ToArray());
                for (int i = 0, j = 0 ; i < TranslationMaxCount && j  < result.Translations.Count; i++) {
                    if (props.Translation[i] == true && props.TranslationTargets[i] != null && props.TranslationTargets[i] != "") {
                        var translation = result.Translations[j++];
                        if (translation != null && translation.Text != null) {
                            await SetSubtitlesAsync(translation.Text, props.TranslationTargets[i], true, props.IsClearInterval ? props.ClearInterval : null, props.IsAdditionalTime ? props.AdditionalTime : null);
                        }
                    }
                }
            }
        }

        private string CreateSubtitlesText(string target) {
            string displayText = this.recognizedText[target];
            if (this.recognizingText[target].Length > 0) {
                displayText += "  ( " + this.recognizingText[target] + " )";
            }
            return displayText;
        }

        async private void ClearSubtitles(Object? sender, System.Timers.ElapsedEventArgs args) {
            try {
                foreach (var dic in clearTimer) {
                    if (dic.Value == sender) {
                        this.recognizedText[dic.Key] = "";
                        await this.SetTextAsync(dic.Key, CreateSubtitlesText(dic.Key));
                        break;
                    }
                }
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
