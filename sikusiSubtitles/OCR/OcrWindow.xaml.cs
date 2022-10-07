using sikusiSubtitles.OBS;
using sikusiSubtitles.Shortcut;
using sikusiSubtitles.Speech;
using sikusiSubtitles.Translation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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
using static sikusiSubtitles.OCR.ScreenCapture;

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
        OcrService? ocrService;

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
            ocrServiceComboBox.ItemsSource = ocrServices;
            ocrServiceComboBox.SelectedItem = ocrServices.Find(service => service.Name == ocrManager.OcrEngine);

            // 翻訳エンジンの一覧をコンボボックスに設定
            translationServices = serviceManager.GetServices<TranslationService>();
            translationServiceComboBox.ItemsSource = translationServices;
            translationServiceComboBox.SelectedItem = translationServices.Find(service => service.Name == ocrManager.TranslationEngine);

            // OBSに接続されている場合、テキストソースを取得する
            obsService = this.serviceManager.GetService<ObsService>();
            GetObsTextSourcesAsync();

            // 読み上げサービス一覧
            speechServices = this.serviceManager.GetServices<SpeechService>();
            speechServiceComboBox.ItemsSource = speechServices;
            speechServiceComboBox.SelectedItem = speechServices.Find(service => service.Name == ocrManager.OcrSpeechEngine);

            // OCR時の自動読み上げ
            speechCheckBox.IsChecked = ocrManager.SpeechWhenOcrRun;
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

        /** OCRエンジンが変更された */
        private void ocrServiceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var service = ocrServiceComboBox.SelectedItem as OcrService;
            if (service != null) {
                // デフォルトのOCRサービスを変更
                ocrService = service;
                ocrManager.OcrEngine = service.Name;

                // 言語一覧を選択したOCRサービスがサポートする言語一覧にする
                var languages = service.GetLanguages();
                ocrLanguageComboBox.ItemsSource = languages;
                ocrLanguageComboBox.SelectedItem = languages.Find(lang => lang.Code == ocrManager.OcrLanguage);
            }
        }

        /** OCRの読み取り言語が変更された */
        private void ocrLanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var lang = ocrLanguageComboBox.SelectedItem as Language;
            if (lang != null) {
                ocrManager.OcrLanguage = lang.Code;
            }
        }

        /** OCRの実行ボタンが押された */
        private async void ocrButton_Click(object sender, RoutedEventArgs e) {
            await Ocr();
        }

        /** 翻訳エンジンが変更された */
        private void translationServiceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var service = translationServiceComboBox.SelectedItem as TranslationService;
            if (service != null) {
                // デフォルトの翻訳サービスを変更
                translationService = service;
                ocrManager.TranslationEngine = service.Name;

                var languages = service.GetLanguages();
                translationLanguageComboBox.ItemsSource = languages;
                translationLanguageComboBox.SelectedItem = languages.Find(lang => lang.Code == ocrManager.TranslationLanguage);
            }
        }

        /** 翻訳先言語が変更された */
        private void translationLanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var lang = translationLanguageComboBox.SelectedItem as Language;
            if (lang != null) {
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
            var service = speechServiceComboBox.SelectedItem as SpeechService;
            if (service != null) {
                speechService = service;
                ocrManager.OcrSpeechEngine = service.Name;

                var voices = service.GetVoices();
                speechVoiceComboBox.ItemsSource = voices;
                speechVoiceComboBox.SelectedItem = voices.Find(voice => voice.Id == ocrManager.OcrSpeechVoice);
            }
        }

        /** 音声読み上げボイスが変更された */
        private void speechVoiceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var voice = speechVoiceComboBox.SelectedItem as Voice;
            if (voice != null) {
                ocrManager.OcrSpeechVoice = voice.Id;
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
        private async Task Ocr() {
            try {
                this.ocrButton.IsEnabled = false;

                var ocrLanguage = ocrLanguageComboBox.SelectedItem as Language;

                if (ocrService == null) {
                    MessageBox.Show("OCRに使用するサービスが設定されていません。", null, MessageBoxButton.OK, MessageBoxImage.Error);
                } else if (this.ocrLanguageComboBox.SelectedIndex == -1) {
                    MessageBox.Show("OCRで読み取る言語が設定されていません。", null, MessageBoxButton.OK, MessageBoxImage.Error);
                } else if (this.ocrService != null && ocrLanguage != null) {
                    var bitmap = CaptureWindow();
                    if (bitmap == null) {
                        MessageBox.Show("画面をキャプチャー出来ませんでした。", null, MessageBoxButton.OK, MessageBoxImage.Error);
                    } else {
                        OcrResult result = await this.ocrService.ExecuteAsync(bitmap, ocrLanguage.Code);
                        if (result.Text != null) {
                            this.ocrTextBox.Text = result.Text;

                            // 文字が取得できた場合、読み上げと翻訳をする
                            if (result.Text.Length > 0) {
                                if (ocrManager.SpeechWhenOcrRun) {
                                    Task task = SpeechOcrTextAsync();
                                }
                                await TranslateAsync();
                            }
                        } else {
                            this.ocrTextBox.Text = result.Error;
                        }
                    }
                }
            } catch (Exception ex) {
                Debug.WriteLine("ServiceManager: " + ex.Message);
            } finally {
                this.ocrButton.IsEnabled = true;
            }
        }

        /** 翻訳をする */
        private async Task TranslateAsync() {
            if (translationService != null) {
                try {
                    var result = await translationService.TranslateAsync(ocrTextBox.Text, null, ocrManager.TranslationLanguage);
                    if (result != null && result.Translations.Count > 0) {
                        translatedTextBox.Text = result.Translations[0].Text;

                        // OBSに接続済みで、翻訳結果表示先が指定されている場合、OBS上に翻訳結果を表示する。
                        var subtitlesService = this.serviceManager.GetService<ObsSubtitlesService>();
                        if (obsService != null && obsService.IsConnected && subtitlesService != null && obsTextSourceComboBox.SelectedItem != null) {
                            var sourceName = obsTextSourceComboBox.SelectedItem as string;
                            if (sourceName != null) {
                                await subtitlesService.SetTextAsync(sourceName, result.Translations[0].Text ?? "");
                            }
                        }
                    } else {
                        // 翻訳に失敗
                        MessageBox.Show("翻訳に失敗しました。", null, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                } catch (Exception ex) {
                   Debug.WriteLine("translationButton_Click: " + ex.Message);
                }
            }
        }

        /** OCRテキストを読み上げる */
        private async Task SpeechOcrTextAsync() {
            var voice = speechVoiceComboBox.SelectedItem as Voice;
            if (speechService != null && voice != null) {
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
            if (obsService != null && obsService.IsConnected) {
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
        private async void CaptureAreaSelected(object? sender, System.Drawing.Rectangle area) {
            captureArea = area;
            await Ocr();
        }

        /** ウィンドウをキャプチャーする */
        private Bitmap? CaptureWindow() {
            Process process = Process.GetProcessById(processId);
            if (!captureArea.IsEmpty) {
                // 画面をキャプチャーする
                RECT rect;
                if (GetWindowRect(process.MainWindowHandle, out rect)) {
                    var left = rect.left + captureArea.Left;
                    var top = rect.top + captureArea.Top;
                    var width = captureArea.Width;
                    var height = captureArea.Height;

                    Bitmap bitmap = new Bitmap(width, height);
                    using (Graphics g = Graphics.FromImage(bitmap)) {
                        g.CopyFromScreen(left, top, 0, 0, new System.Drawing.Size(width, height));
                    }
                    return bitmap;
                }
            }

            return null;
        }
    }
}
