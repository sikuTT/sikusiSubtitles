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
        private Service.ServiceManager serviceManager;
        private ObsService? obsService;
        private SpeechRecognitionService? speechRecognitionService;
        private TranslationService? translationService;
        private Dictionary<string, string> recognizedText = new Dictionary<string, string>();
        private Dictionary<string, string> recognizingText = new Dictionary<string, string>();
        private Dictionary<string, System.Timers.Timer> clearTimer = new Dictionary<string, System.Timers.Timer>();

        public string VoiceTarget { get; set; }
        public string Translation1Target { get; set; }
        public string Translation2Target { get; set; }
        public int? ClearInterval { get; set; }
        public int? AdditionalTime { get; set; }

        public SubtitlesService(Service.ServiceManager serviceManager, string name, string displayName, int index) : base(ObsService.SERVICE_NAME, name, displayName, index) {
            this.serviceManager = serviceManager;

            this.VoiceTarget = "";
            this.Translation1Target = "";
            this.Translation2Target = "";
        }

        public override bool Start() {
            this.obsService = this.serviceManager.GetService<ObsService>(ObsService.SERVICE_NAME, "OBS");

            this.speechRecognitionService = this.serviceManager.GetActiveService<SpeechRecognitionService>();
            if (this.speechRecognitionService == null) {
                return false;
            } else {
                this.speechRecognitionService.Recognizing += Recognizing;
                this.speechRecognitionService.Recognized += Recognized;
            }

            this.translationService = this.serviceManager.GetActiveService<TranslationService>();
            if (this.translationService != null) {
                this.translationService.Translated += Translated;
            }

            return true;
        }

        public override void Stop() {
            if (this.speechRecognitionService != null) {
                this.speechRecognitionService.Recognizing -= Recognizing;
                this.speechRecognitionService.Recognized -= Recognized;
            }

            this.translationService = this.serviceManager.GetActiveService<TranslationService>();
            if (this.translationService != null) {
                this.translationService.Translated -= Translated;
            }
        }

        private void Recognizing(object? sender, SpeechRecognitionEventArgs args) {
            if (this.obsService != null && this.obsService.IsConnected) {
                if (args.Text != "") {
                    SetSubtitles(args.Text, this.VoiceTarget, false);
                }
            }
        }

        private void Recognized(object? sender, SpeechRecognitionEventArgs args) {
            if (this.obsService != null && this.obsService.IsConnected) {
                if (args.Text != "") {
                    // 字幕を表示する。
                    SetSubtitles(args.Text, this.VoiceTarget, true, this.ClearInterval, this.AdditionalTime);

                    // 翻訳サービスが設定されている場合、翻訳する
                    Translate(args.Text);
                }
            }
        }

        private void Translated(object? sender, TranslationResult result) {
            var targets = new string[] { this.Translation1Target, this.Translation2Target };
            var i = 0;
            foreach (var target in targets) {
                if (target != null) {
                    if (result.Translations.Count > i) {
                        var translation = result.Translations[i++];
                        if (translation != null && translation.Text != null) {
                            SetSubtitles(translation.Text, target, true, this.ClearInterval, this.AdditionalTime);
                        }
                    }
                }
            }
        }

        private void SetSubtitles(string text, string target, bool recognized, int? timeout = null, int? additionalTimeout = null) {
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
                this.UpdateGDIPlusText(target, CreateSubtitlesText(target));

                // 字幕削除のタイマーを作成する。
                if (timeout != null) {
                    double timeoutms = (double)timeout * 1000;
                    if (additionalTimeout != null) {
                        timeoutms += ((double)additionalTimeout / 100 * 1000) * text.Length;
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
                this.translationService.Translate(text);
            }
        }

        private void UpdateGDIPlusText(string target, string text) {
            if (this.obsService == null) {
                return;
            }
            try {
                var obsSocket = this.obsService.ObsSocket;
                if (obsService.IsConnected) {
                    var prop = obsSocket.GetTextGDIPlusProperties(target);
                    prop.Text = text;
                    obsSocket.SetTextGDIPlusProperties(prop);
                }
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message);
            }
        }

        private string CreateSubtitlesText(string target) {
            string displayText = this.recognizedText[target];
            if (this.recognizingText[target].Length > 0) {
                displayText += "  ( " + this.recognizingText[target] + " )";
            }
            return displayText;
        }

        private void ClearSubtitles(Object? sender, System.Timers.ElapsedEventArgs args) {
            try {
                foreach (var dic in clearTimer) {
                    if (dic.Value == sender) {
                        this.recognizedText[dic.Key] = "";
                        this.UpdateGDIPlusText(dic.Key, CreateSubtitlesText(dic.Key));
                        break;
                    }
                }
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
