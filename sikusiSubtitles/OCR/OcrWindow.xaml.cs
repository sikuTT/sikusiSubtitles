using sikusiSubtitles.OBS;
using sikusiSubtitles.Shortcut;
using sikusiSubtitles.Speech;
using sikusiSubtitles.Translation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace sikusiSubtitles.OCR {
    /// <summary>
    /// OcrWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class OcrWindow : Window {
        ServiceManager serviceManager;
        int processId;

        // OCR
        OcrServiceManager ocrManager;
        List<OcrService> ocrServices = new List<OcrService>();

        // 翻訳
        List<TranslationService> translationServices = new List<TranslationService>();
        TranslationService? translationService;

        // OBS
        ObsService? obsService;

        // 読み上げ
        List<SpeechService> speechServices = new List<SpeechService>();
        SpeechService? speechService;

        // ショートカット
        ShortcutService? shortcutService;

        // 画面のキャプチャーエリア指定
        CaptureWindow? captureWindow;
        System.Drawing.Rectangle captureArea;

        public OcrWindow(ServiceManager serviceManager, OcrServiceManager ocrManager, int processId) {
            this.serviceManager = serviceManager;
            this.ocrManager = ocrManager;
            this.processId = processId;

            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            // キャプチャー対象のウィンドウ名をフォームに表示する。
            Process process = Process.GetProcessById(processId);
            windowTitleTextBox.Text = process.MainWindowTitle;

            // ショートカットイベントを取得
            this.shortcutService = this.serviceManager.GetService<ShortcutService>();
            if (shortcutService != null) {
                // shortcutService.ShortcutRun += ShortcutRun;
            }

            // OCRサービス一覧をコンボボックスに設定
            ocrServices = this.serviceManager.GetServices<OcrService>();
            ocrServices.ForEach(service => {
                var i = ocrServiceComboBox.Items.Add(service.DisplayName);
                if (service.Name == ocrManager.OcrEngine) ocrServiceComboBox.SelectedIndex = i;
            });

            // 翻訳エンジンの一覧をコンボボックスに設定
            translationServices = serviceManager.GetServices<TranslationService>();
            translationServiceComboBox.ItemsSource = translationServices.Select(service => service.DisplayName);
            translationServiceComboBox.SelectedIndex = translationServices.FindIndex(service => service.Name == ocrManager.TranslationEngine);

            // OBSに接続されている場合、テキストソースを取得する
            GetObsTextSourcesAsync();
            
            // 読み上げサービス一覧
            speechServices = this.serviceManager.GetServices<SpeechService>();
            speechServiceComboBox.ItemsSource = speechServices.Select(service => service.DisplayName);
            speechServiceComboBox.SelectedIndex = speechServices.FindIndex(service => service.Name == ocrManager.OcrSpeechEngine);
        }

        /** ウィンドウがクローズされた */
        private void Window_Closed(object sender, EventArgs e) {
            if (captureWindow != null) {
                captureWindow.Close();
            }
        }

        /** 読み取りエリアの設定ボタン */
        private void captureAreaButton_Click(object sender, RoutedEventArgs e) {
            // キャプチャーするウィンドウの上に、キャプチャー画面を表示する。
            Process process = Process.GetProcessById(processId);
            if (captureWindow != null) {
                captureWindow.Close();
            }

            // キャプチャー対象のウィンドウをアクティブにする
            Microsoft.VisualBasic.Interaction.AppActivate(processId);

            // キャプチャー対象のウィンドウの上にキャプチャー処理をするウィンドウを作成する
            captureWindow = new CaptureWindow(process.MainWindowHandle, captureArea);
            captureWindow.Show();
            captureWindow.Activate();
            captureWindow.AreaSelected += CaptureAreaSelected;
            captureWindow.Closed += (object? sender, EventArgs e) => {
                captureWindow.AreaSelected -= CaptureAreaSelected;
                captureWindow = null;
            };
        }

        /** 翻訳エンジンが変更された */
        private void translationServiceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (translationServiceComboBox.SelectedIndex != -1) {
                translationService = translationServices[translationServiceComboBox.SelectedIndex];
                ocrManager.TranslationEngine = translationService.Name;

                var languages = translationService.GetLanguages();
                translationLanguageComboBox.ItemsSource = languages.Select(lang => lang.Name);
                translationLanguageComboBox.SelectedIndex = languages.FindIndex(lang => lang.Code == ocrManager.TranslationLanguage);
            }
        }

        /** 翻訳先言語が変更された */
        private void translationLanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (translationService != null && translationLanguageComboBox.SelectedIndex != -1) {
                var languages = translationService.GetLanguages();
                var lang = languages[translationLanguageComboBox.SelectedIndex];
                ocrManager.TranslationLanguage = lang.Code;
            }
        }

        /** 翻訳ボタンが押された */
        private async void translationButton_Click(object sender, RoutedEventArgs e) {
            await TranslateAsync();
        }

        /** OBSテキストソースの更新ボタンが押された */
        private void refreshObsTextSourceButton_Click(object sender, RoutedEventArgs e) {
            GetObsTextSourcesAsync();
        }

        /** 音声読み上げエンジンが変更された */
        private void speechServiceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (speechServiceComboBox.SelectedIndex != -1) {
                speechService = speechServices[speechServiceComboBox.SelectedIndex];
                ocrManager.OcrSpeechEngine = speechService.Name;

                var voices = speechService.GetVoices();
                speechVoiceComboBox.ItemsSource = voices.Select(voice => voice.DisplayName);
                speechVoiceComboBox.SelectedIndex = voices.FindIndex(voice => voice.Id == ocrManager.OcrSpeechVoice);
            }
        }

        /** 音声読み上げボイスが変更された */
        private void speechVoiceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (speechService != null && speechVoiceComboBox.SelectedIndex != -1) {
                var voices = speechService.GetVoices();
                ocrManager.OcrSpeechVoice = voices[speechVoiceComboBox.SelectedIndex].Id;
            }

            SetSpeechButtonEnabled();
        }

        /** 読み上げボタン */
        private async void speechButton_Click(object sender, RoutedEventArgs e) {
            await SpeechOcrTextAsync();
        }

        /** 読み上げキャンセルボタン */
        private void speechCancelButton_Click(object sender, RoutedEventArgs e) {
            if (speechService != null) {
                speechService.CancelSpeakAsync();
            }
        }

        /** OCR時に読み上げるがチェックされた */
        private void speechCheckBox_Checked(object sender, RoutedEventArgs e) {
            ocrManager.SpeechWhenOcrRun = true;
        }

        /** OCR時に読み上げるのチェックが外された */
        private void speechCheckBox_Unchecked(object sender, RoutedEventArgs e) {
            ocrManager.SpeechWhenOcrRun = false;
        }

        /** OCRテキストが変更された */
        private void ocrTextBox_TextChanged(object sender, TextChangedEventArgs e) {
            SetSpeechButtonEnabled();
            translationButton.IsEnabled = ocrTextBox.Text.Length > 0;
        }

        /** OCRテキストのフォーカスが外れた */
        private void ocrTextBox_LostFocus(object sender, RoutedEventArgs e) {
            //  テキストが選択されている場合、選択状態を維持する
            if (ocrTextBox.SelectedText.Length > 0) {
                e.Handled = true;
            }
        }

        /** OCRを実行する */
        private void Ocr() {

        }

        /** 翻訳をする */
        private async Task TranslateAsync() {
            if (translationService != null) {
                try {
                    var result = await translationService.TranslateAsync(ocrTextBox.Text, null, ocrManager.TranslationLanguage);
                    if (result != null && result.Translations.Count > 0) {
                        translatedTextBox.Text = result.Translations[0].Text;
                    }
                } catch (Exception ex) {
                    Debug.WriteLine("translationButton_Click: " + ex.Message);
                }
            }
        }

        /** OCRテキストを読み上げる */
        private async Task SpeechOcrTextAsync() {
            if (speechService != null && speechVoiceComboBox.SelectedIndex != -1) {
                var voices = speechService.GetVoices();
                var voice = voices[speechVoiceComboBox.SelectedIndex];

                try {
                    speechButton.Visibility = Visibility.Collapsed;
                    speechCancelButton.Visibility = Visibility.Visible;

                    var text = ocrTextBox.SelectedText.Length > 0 ? ocrTextBox.SelectedText : ocrTextBox.Text;
                    await speechService.SpeakAsync(voice, text);
                } finally {
                    speechButton.Visibility = Visibility.Visible;
                    speechCancelButton.Visibility = Visibility.Collapsed;
                }
            }
        }

        /**
         * OBSのテキストソースの一覧を取得する。
         */
        async private void GetObsTextSourcesAsync() {
            this.obsTextSourceComboBox.ItemsSource = null;

            // OBSのテキストソースの一覧を取得
            obsService = this.serviceManager.GetService<ObsService>();

            if (obsService != null && obsService.ObsSocket.IsConnected) {
                var nameList = new List<string>() { "" };
                // シーン一覧を取得する
                var sceneList = await obsService.ObsSocket.GetSceneListAsync();
                var sceneNames = sceneList.scenes.Select(scene => scene.sceneName).ToList();
                if (sceneNames != null) {
                    // 各シーン内のソースを取得し、GDIテキストだけを取り出す
                    foreach (var sceneName in sceneNames) {
                        // シーン内のソース一覧を取得し、GDIテキストとグループだけを取り出す
                        var sceneItemList = await obsService.ObsSocket.GetSceneItemListAsync(sceneName);
                        var sourceItems = sceneItemList.sceneItems.Where(item => item.inputKind == "text_gdiplus_v2" || item.isGroup == true).ToList();
                        if (sourceItems != null) {
                            // シーン内のアイテムを1個ずつ処理
                            foreach (var item in sourceItems) {
                                if (item.isGroup == true) {
                                    // グループの場合、グループ内のソース一覧を取得し、その中からGDIテキストだけを取得する
                                    var groupItemList = await obsService.ObsSocket.GetGroupSceneItemListAsync(item.sourceName);
                                    var groupSourceItems = groupItemList.sceneItems.Where(item => item.inputKind == "text_gdiplus_v2").Select(item => item.sourceName).ToList();
                                    if (groupSourceItems != null) {
                                        nameList.AddRange(groupSourceItems);
                                    }
                                } else {
                                    // テキストなら名前を取得
                                    nameList.Add(item.sourceName);
                                }
                            }
                        }
                    }
                }
                this.obsTextSourceComboBox.ItemsSource = nameList.Distinct();
            }
        }

        /** 読み上げボタンの状態を設定する */
        private void SetSpeechButtonEnabled() {
            var enabled = speechVoiceComboBox.SelectedIndex != -1 && ocrTextBox.Text.Length > 0;
            speechButton.IsEnabled = speechCancelButton.IsEnabled = enabled;
        }

        /** OCRをするウィンドウエリアが選択された */
        private void CaptureAreaSelected(object? sender, System.Drawing.Rectangle area) {
            captureArea = area;
            ocrButton.IsEnabled = true;
            Ocr();
        }
    }
}
