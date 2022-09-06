using Newtonsoft.Json.Linq;
using ObsWebSocket5.Message.Data.Response.InputSettings;
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

    public class SubtitlesService : Service.Service {
        private ObsService? obsService;
        private SpeechRecognitionService? speechRecognitionService;
        private TranslationService? translationService;

        private Dictionary<string, string> recognizedText = new Dictionary<string, string>();
        private Dictionary<string, string> recognizingText = new Dictionary<string, string>();
        private Dictionary<string, System.Timers.Timer> clearTimer = new Dictionary<string, System.Timers.Timer>();

        public string TranslationEngine { get; set; }
        public string TranslationLanguageFrom { get; set; }
        public string[] TranslationLanguageTo { get; set; }
        public bool[] Translation { get; set; }
        public string VoiceTarget { get; set; }
        public string[] TranslationTargets { get; set; }
        public bool IsClearInterval { get; set; }
        public int ClearInterval { get; set; }
        public bool IsAdditionalTime { get; set; }
        public int AdditionalTime { get; set; }

        public override void Load() {
            TranslationEngine = Properties.Settings.Default.SubtitlesTranslationEngine;
            TranslationLanguageFrom = Properties.Settings.Default.SubtitlesTranslationLanguageFrom;
            TranslationLanguageTo[0] = Properties.Settings.Default.SubtitlesTranslationLanguageTo1;
            TranslationLanguageTo[1] = Properties.Settings.Default.SubtitlesTranslationLanguageTo2;
            Translation[0] = Properties.Settings.Default.SubtitlesTranslation1;
            Translation[1] = Properties.Settings.Default.SubtitlesTranslation2;
            VoiceTarget = Properties.Settings.Default.SubtitlesVoiceTarget;
            TranslationTargets[0] = Properties.Settings.Default.SubtitlesTranslationTarget1;
            TranslationTargets[1] = Properties.Settings.Default.SubtitlesTranslationTarget2;
            IsClearInterval = Properties.Settings.Default.SubtitlesIsClearInterval;
            ClearInterval = Properties.Settings.Default.SubtitlesClearInterval;
            IsAdditionalTime = Properties.Settings.Default.SubtitlesIsAdditionalTime;
            AdditionalTime = Properties.Settings.Default.SubtitlesAdditionalTime;
        }

        public override void Save() {
            Properties.Settings.Default.SubtitlesTranslationEngine = TranslationEngine;
            Properties.Settings.Default.SubtitlesTranslationLanguageFrom = TranslationLanguageFrom;
            Properties.Settings.Default.SubtitlesTranslationLanguageTo1 = TranslationLanguageTo[0];
            Properties.Settings.Default.SubtitlesTranslationLanguageTo2 = TranslationLanguageTo[1];
            Properties.Settings.Default.SubtitlesTranslation1 = Translation[0];
            Properties.Settings.Default.SubtitlesTranslation2 = Translation[1];
            Properties.Settings.Default.SubtitlesVoiceTarget = VoiceTarget;
            Properties.Settings.Default.SubtitlesTranslationTarget1 = TranslationTargets[0];
            Properties.Settings.Default.SubtitlesTranslationTarget2 = TranslationTargets[1];
            Properties.Settings.Default.SubtitlesIsClearInterval = IsClearInterval;
            Properties.Settings.Default.SubtitlesClearInterval = (int)ClearInterval;
            Properties.Settings.Default.SubtitlesIsAdditionalTime = IsAdditionalTime;
            Properties.Settings.Default.SubtitlesAdditionalTime = (int)AdditionalTime;
        }

        public SubtitlesService(Service.ServiceManager serviceManager) : base(serviceManager, ObsServiceManager.ServiceName, "Subtitles", "字幕", 200) {
            TranslationEngine = "";
            TranslationLanguageFrom = "";
            TranslationLanguageTo = new string[2];
            Translation = new bool[2];
            VoiceTarget = "";
            TranslationTargets = new string[2];
        }

        public bool Start(ObsService obsService) {
            this.obsService = obsService;

            this.speechRecognitionService = this.ServiceManager.GetActiveService<SpeechRecognitionService>();
            if (this.speechRecognitionService == null) {
                return false;
            } else {
                this.speechRecognitionService.Recognizing += Recognizing;
                this.speechRecognitionService.Recognized += Recognized;
            }

            var translationServices = this.ServiceManager.GetServices<TranslationService>();
            this.translationService = translationServices.Where(service => service.Name == TranslationEngine).First();
            if (this.translationService != null) {
                this.translationService.Translated += Translated;
            }

            return true;
        }

        public void Stop() {
            if (this.speechRecognitionService != null) {
                this.speechRecognitionService.Recognizing -= Recognizing;
                this.speechRecognitionService.Recognized -= Recognized;
            }

            if (this.translationService != null) {
                this.translationService.Translated -= Translated;
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
                    var settings = response?.d?.responseData?.inputSettings as TextGdiplusV2;
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
                    await SetSubtitlesAsync(args.Text, this.VoiceTarget, false);
                }
            }
        }

        async private void Recognized(object? sender, SpeechRecognitionEventArgs args) {
            if (this.obsService != null && this.obsService.IsConnected) {
                if (args.Text != "") {
                    // 字幕を表示する。
                    await SetSubtitlesAsync(args.Text, this.VoiceTarget, true, this.ClearInterval, this.AdditionalTime);

                    // 翻訳サービスが設定されている場合、翻訳する
                    Translate(args.Text);
                }
            }
        }

        async private void Translated(object? sender, TranslationResult result) {
            if (result.Obj == this) {
                var i = 0;
                foreach (var target in this.TranslationTargets) {
                    if (target != null) {
                        if (result.Translations.Count > i) {
                            var translation = result.Translations[i++];
                            if (translation != null && translation.Text != null) {
                                await SetSubtitlesAsync(translation.Text, target, true, this.IsClearInterval ? this.ClearInterval : null, this.IsAdditionalTime ? this.AdditionalTime : null);
                            }
                        }
                    }
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
        private void Translate(string text) {
            if (this.translationService != null) {
                this.translationService.Translate(this, text);
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
