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
        private Dictionary<string, List<System.Timers.Timer>> TimerDic = new Dictionary<string, List<System.Timers.Timer>>();
        private Dictionary<string, List<SubtitlesText>> SubtitlesList = new Dictionary<string, List<SubtitlesText>>();

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
                this.translationService.Translated += Translated;
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
                // 字幕を追加もしくは既存字幕を更新
                if (this.SubtitlesList.ContainsKey(target) == false) {
                    this.SubtitlesList.Add(target, new List<SubtitlesText>());
                }
                var subtitlesText = this.SubtitlesList[target];
                if (subtitlesText.Count == 0 || (timeout != null && subtitlesText.Last().Recognized == true)) {
                    subtitlesText.Add(new SubtitlesText(text, recognized));
                } else {
                    subtitlesText.Last().Text = text;
                    subtitlesText.Last().Recognized = recognized;
                }

                // 字幕を表示
                this.UpdateGDIPlusText(target);

                // 字幕削除のタイマーを作成する。
                if (timeout != null && recognized) {
                    double timeoutms = (double)timeout * 1000;
                    if (additionalTimeout != null) {
                        timeoutms += ((double)additionalTimeout / 100 * 1000) * text.Length;
                    }
                    var timer = new System.Timers.Timer(timeoutms);
                    timer.AutoReset = false;
                    timer.Elapsed += UpdateSubtitlesTimer;
                    timer.Start();
                    if (this.TimerDic.ContainsKey(target) == false) {
                        this.TimerDic.Add(target, new List<System.Timers.Timer>());
                    }
                    TimerDic[target].Add(timer);
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

        private void UpdateGDIPlusText(string target) {
            if (this.obsService == null) {
                return;
            }
            try {
                var obsSocket = this.obsService.ObsSocket;
                if (obsService.IsConnected) {
                    string text = this.CreateSubtitlesText(target);
                    var prop = obsSocket.GetTextGDIPlusProperties(target);
                    prop.Text = text;
                    obsSocket.SetTextGDIPlusProperties(prop);
                }
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message);
            }
        }

        private string CreateSubtitlesText(string key) {
            string text = "";
            foreach (var subtitle in this.SubtitlesList[key]) {
                text += subtitle.Text;
            }
            return text;
        }

        private void UpdateSubtitlesTimer(Object? sender, System.Timers.ElapsedEventArgs args) {
            try {
                var keys = this.TimerDic.Keys;
                foreach (var key in keys) {
                    var timer = this.TimerDic[key].Find(timer => timer == sender);
                    if (timer != null) {
                        this.SubtitlesList[key].Remove(this.SubtitlesList[key][0]);
                        this.TimerDic[key].Remove(timer);
                        this.UpdateGDIPlusText(key);
                        return;
                    }
                }
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
