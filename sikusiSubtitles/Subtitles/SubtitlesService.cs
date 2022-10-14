using Newtonsoft.Json.Linq;
using sikusiSubtitles.SpeechRecognition;
using sikusiSubtitles.Translation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace sikusiSubtitles.Subtitles {
    public class SubtitlesService : Service {
        // Services
        SpeechRecognitionServiceManager? speechRecognitionServiceManager;

        Label engineNameBox = new Label();

        // Subtitles texts
        List<SubtitlesText> subtitlesTexts = new List<SubtitlesText>();
        System.Timers.Timer? clearTimer;

        // Events
        public event EventHandler<List<SubtitlesText>>? SubtitlesChanged;

        // Properties
        public string TranslationEngine {
            get { return translationEngine; }
            set {
                translationEngine = value;
                var translationService = ServiceManager.GetServices<TranslationService>().Find(service => service.Name == translationEngine);
                engineNameBox.Content = translationService != null ? translationService.DisplayName : "";
            }
        }
        string translationEngine = "";
        public string TranslationLanguageFrom { get; set; } = "";
        public List<string> TranslationLanguageToList { get; set; } = new List<string>();
        public bool ClearInterval { get; set; } = false;
        public int ClearIntervalTime { get; set; } = 1;
        public bool AdditionalClear { get; set; } = false;
        public int AdditionalClearTime { get; set; } = 1;

        public SubtitlesService(ServiceManager serviceManager) : base(serviceManager, SubtitlesServiceManager.ServiceName, "Subtitles", "字幕", 100) {
            // ステータスバーに字幕で使用する翻訳エンジンを表示する
            var stackPanel = new StackPanel { Orientation = Orientation.Horizontal };
            stackPanel.Children.Add(new Image { Source = new BitmapImage(new Uri("pack://application:,,,/Resources/translation.png")) });
            stackPanel.Children.Add(engineNameBox);
            serviceManager.AddStatusBarControl(stackPanel, 200);

            // 設定ページ
            settingsPage = new SubtitlesPage(ServiceManager, this);
        }

        /**
         * サービスの初期化
         * 音声認識の監視を始める。
         */
        public override void Init() {
            speechRecognitionServiceManager = ServiceManager.GetService<SpeechRecognitionServiceManager>();
            if (speechRecognitionServiceManager != null) {
                speechRecognitionServiceManager.Recognizing += RecognizingHandler;
                speechRecognitionServiceManager.Recognized += RecognizedHandler;
            }
        }

        /** 設定の読み込み */
        public override void Load(JToken token) {
            TranslationEngine = token.Value<string>("TranslationEngine") ?? "";
            TranslationLanguageFrom = token.Value<string>("TranslationLanguageFrom") ?? "";
            var toList = token.Value<JArray>("TranslationLanguageToList");
            if (toList != null) {
                foreach (var to in toList) {
                    TranslationLanguageToList.Add(to.ToString());
                }
            }
            ClearInterval = token.Value<bool>("ClearInterval");
            ClearIntervalTime = token.Value<int?>("ClearIntervalTime") ?? ClearIntervalTime;
            AdditionalClear = token.Value<bool>("AdditionalClear");
            AdditionalClearTime = token.Value<int?>("AdditionalClearTime") ?? AdditionalClearTime;
        }

        /** 設定を保存 */
        public override JObject? Save() {
            // 空文字の翻訳先は削除する
            TranslationLanguageToList.RemoveAll(to => to == "");

            return new JObject {
                new JProperty("TranslationEngine", TranslationEngine),
                new JProperty("TranslationLanguageFrom", TranslationLanguageFrom),
                new JProperty("TranslationLanguageToList", TranslationLanguageToList),
                new JProperty("ClearInterval", ClearInterval),
                new JProperty("ClearIntervalTime", ClearIntervalTime),
                new JProperty("AdditionalClear", AdditionalClear),
                new JProperty("AdditionalClearTime", AdditionalClearTime),
            };
        }

        /** 音声が読み上げられたら字幕を更新する */
        private void RecognizingHandler(Object? sender, SpeechRecognitionEventArgs args) {
            UpdateSubtitlesText(false, args.Text);
            SubtitlesChanged?.Invoke(this, subtitlesTexts);
        }

        /** 音声の読み上げが確定したら翻訳する */
        private async void RecognizedHandler(Object? sender, SpeechRecognitionEventArgs args) {
            var subtitlesText = UpdateSubtitlesText(true, args.Text);

            var service = ServiceManager.GetServices<TranslationService>().Find(service => service.Name == TranslationEngine);
            if (service != null) {
                var result = await service.TranslateAsync(args.Text, TranslationLanguageFrom, TranslationLanguageToList.Where(lang => lang != "").ToArray());
                if (result != null && result.Error == false) {
                    result.Translations.ForEach(translation => {
                        var translationText = subtitlesText.TranslationTexts.Find(text => text.Language == translation.Language);
                        if (translationText != null) {
                            translationText.Text = translation.Text ?? "";
                        }
                    });
                }
            }
            SubtitlesChanged?.Invoke(this, subtitlesTexts);
        }

        /** 字幕情報の更新 */
        private SubtitlesText UpdateSubtitlesText(bool recognized, string text) {
            SubtitlesText? subtitlesText = null;
            if (subtitlesTexts.Count > 0 && subtitlesTexts.Last().Recognized == false) {
                subtitlesText = subtitlesTexts.Last();
                subtitlesText.Recognized = recognized;
                subtitlesText.VoiceText = text;
            } else {
                // 字幕を一定時間でクリアしない場合は、次の字幕が着た時点でクリアする
                if (ClearInterval == false) {
                    subtitlesTexts.Clear();
                }

                // 新しい字幕の作成
                var textList = new List<TranslationText>(TranslationLanguageToList.Count);
                TranslationLanguageToList.ForEach(lang => textList.Add(new TranslationText(lang)));
                subtitlesText = new SubtitlesText(recognized, text, textList);
                subtitlesTexts.Add(subtitlesText);
            }

            // 字幕が確定して、一定時間で字幕を削除するオプションの場合、タイマーを作成する。
            if (recognized == true && ClearInterval == true) {
                CreateClearTimer();
            }

            return subtitlesText;
        }

        /** 字幕削除用のタイマーを作成する */
        private void CreateClearTimer() {
            if (clearTimer == null) {
                var time = ClearIntervalTime * 1000;
                if (AdditionalClear == true) {
                    time += (int)(AdditionalClearTime / 100.0 * subtitlesTexts[0].VoiceText.Length * 1000);
                }
                clearTimer = new System.Timers.Timer(time);
                clearTimer.Elapsed += ClearSubtitles;
                clearTimer.AutoReset = false;
                clearTimer.Enabled = true;
            }
        }

        /** 字幕をクリアする */
        private void ClearSubtitles(Object? sender, ElapsedEventArgs args) {
            clearTimer = null;
            subtitlesTexts.RemoveAt(0);
            SubtitlesChanged?.Invoke(this, subtitlesTexts);

            if (subtitlesTexts.Count > 0) {
                if (subtitlesTexts[0].Recognized == true) {
                    CreateClearTimer();
                }
            }
        }
    }
}
