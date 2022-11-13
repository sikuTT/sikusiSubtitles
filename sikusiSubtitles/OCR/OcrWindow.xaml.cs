using Reactive.Bindings;
using sikusiSubtitles.OBS;
using sikusiSubtitles.Shortcut;
using sikusiSubtitles.Speech;
using sikusiSubtitles.Translation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Windows.UI.Core;
using static sikusiSubtitles.OCR.ScreenCapture;

namespace sikusiSubtitles.OCR {
    class OcrWindowViewModel {
        // キャプチャ対象の情報
        public ReactivePropertySlim<string> WindowTitle { get; } = new();

        // 読み上げ
        public ReactivePropertySlim<bool> SpeechWhenOcrRun { get; } = new();
    }

    /// <summary>
    /// OcrWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class OcrWindow : Window {
        OcrWindowViewModel viewModel = new();
        ServiceManager serviceManager;
        OcrServiceManager ocrManager;
        int processId;

        // OCR
        List<OcrService> ocrServices = new List<OcrService>();
        OcrService? ocrService;
        string ocrServiceName;
        string? ocrLanguageCode;

        // 翻訳
        List<TranslationService> translationServices = new List<TranslationService>();
        TranslationService? translationService;
        string translationServiceName;
        string? translationLanguageCode;

        // OBS
        ObsService? obsService;
        string obsTextSource = "";

        // 読み上げ
        List<SpeechService> speechServices = new List<SpeechService>();
        SpeechService? speechService;
        string speechServiceName;
        string speechVoiceId;

        // ショートカット
        ShortcutService? shortcutService;

        // 画面のキャプチャーエリア指定
        CaptureWindow? captureWindow;
        System.Drawing.Rectangle captureArea;
        int captureScale = 1;

        // Notion
        string? notionSaveId;

        public OcrWindow(ServiceManager serviceManager, OcrServiceManager ocrManager, int processId) {
            InitializeComponent();
            DataContext = viewModel;

            this.serviceManager = serviceManager;
            this.ocrManager = ocrManager;
            this.processId = processId;

            // OCR
            this.ocrServiceName = ocrManager.OcrEngine;
            this.ocrLanguageCode = ocrManager.OcrLanguage;

            // 翻訳
            this.translationServiceName = ocrManager.TranslationEngine;
            this.translationLanguageCode = ocrManager.TranslationLanguage;

            // 読み上げ
            this.speechServiceName = ocrManager.OcrSpeechEngine;
            this.speechVoiceId = ocrManager.OcrSpeechVoice;
            viewModel.SpeechWhenOcrRun.Value = ocrManager.SpeechWhenOcrRun;

            // ショートカットの設定
            ocrShortcutKeyTextBox.Text = ocrManager.OcrShortcut.ShortcutKey;
            clearTranslatedShortcutKeyTextBox.Text = ocrManager.ClearObsTextShortcut.ShortcutKey;
        }

        public OcrWindow(OcrWindow ocrWindow) {
            InitializeComponent();
            DataContext = viewModel;

            this.serviceManager = ocrWindow.serviceManager;
            this.ocrManager = ocrWindow.ocrManager;
            this.processId = ocrWindow.processId;

            // OCR
            this.ocrServiceName = ocrWindow.ocrServiceName;
            this.ocrLanguageCode = ocrWindow.ocrLanguageCode;

            // 翻訳
            this.translationServiceName = ocrWindow.translationServiceName;
            this.translationLanguageCode = ocrWindow.translationLanguageCode;

            // OBS
            obsTextSource = ocrWindow.obsTextSource;

            // 読み上げ
            this.speechServiceName = ocrWindow.speechServiceName;
            this.speechVoiceId = ocrWindow.speechVoiceId;
            viewModel.SpeechWhenOcrRun.Value = ocrWindow.viewModel.SpeechWhenOcrRun.Value;

            // 画面のキャプチャーエリア指定
            this.captureArea = ocrWindow.captureArea;
            this.captureScale = ocrWindow.captureScale;
            if (!captureArea.IsEmpty) {
                this.ocrButton.IsEnabled = true;
            }

            // ショートカット
            this.ocrShortcutKeyTextBox.Text = ocrWindow.ocrShortcutKeyTextBox.Text;
            this.clearTranslatedShortcutKeyTextBox.Text = ocrWindow.clearTranslatedShortcutKeyTextBox.Text;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            serviceManager.MainWindow.Closed += MainWindowClosed;

            // キャプチャー対象のウィンドウ名をフォームに表示する。
            try {
                Process process = Process.GetProcessById(processId);
                viewModel.WindowTitle.Value = process.MainWindowTitle;
            }
            catch (Exception) {
                MessageBox.Show("キャプチャー対象のプロセスが取得できませんでした。", null, MessageBoxButton.OK, MessageBoxImage.Error);
            }

            // OCRサービス一覧をコンボボックスに設定
            ocrServices = this.serviceManager.GetServices<OcrService>();
            ocrServiceComboBox.ItemsSource = ocrServices;
            ocrServiceComboBox.SelectedItem = ocrServices.Find(service => service.Name == ocrServiceName);

            // 翻訳エンジンの一覧をコンボボックスに設定
            translationServices = serviceManager.GetServices<TranslationService>();
            translationServiceComboBox.ItemsSource = translationServices;
            translationServiceComboBox.SelectedItem = translationServices.Find(service => service.Name == translationServiceName);

            // OBS
            obsService = this.serviceManager.GetService<ObsService>();
            if (obsService != null) {
                obsService.ConnectionChanged += obsConnectionChangedHandler;
                obsConnectionChangedHandler(this , obsService.IsConnected);
            }

            // 読み上げサービス一覧
            speechServices = this.serviceManager.GetServices<SpeechService>();
            speechServiceComboBox.ItemsSource = speechServices;
            speechServiceComboBox.SelectedItem = speechServices.Find(service => service.Name == speechServiceName);

            // ショートカットイベントを取得
            shortcutService = this.serviceManager.GetService<ShortcutService>();
            if (shortcutService != null) {
                shortcutService.ShortcutRun += ShortcutRun;
            }

            // ステータスバーを更新
            SetOcrStatusText();
            SetTranslationStatusText();
            SetObsStatusText();
            SetSpeechStatusText();
            SetShortcutKeyStatusText();
        }

        /** ウィンドウがクローズされた */
        private void Window_Closed(object sender, EventArgs e) {
            // OBS
            if (obsService != null) {
                obsService.ConnectionChanged -= obsConnectionChangedHandler;
            }

            if (this.shortcutService != null) {
                this.shortcutService.ShortcutRun -= ShortcutRun;
            }

            if (captureWindow != null) {
                captureWindow.Close();
            }
        }

        /** 読み取りエリアの設定ボタン */
        private void captureAreaButton_Click(object sender, RoutedEventArgs e) {
            // キャプチャーするウィンドウの上に、キャプチャー画面を表示する。
            try {
                Process process = Process.GetProcessById(processId);
                if (captureWindow != null) {
                    captureWindow.Close();
                    captureWindow = null;
                }

                // キャプチャー対象のウィンドウをアクティブにする
                Microsoft.VisualBasic.Interaction.AppActivate(processId);

                // キャプチャー対象のウィンドウの上にキャプチャー処理をするウィンドウを作成する
                captureWindow = new CaptureWindow(process.Handle, process.MainWindowHandle, captureArea);
                captureWindow.Show();
                captureWindow.Activate();
                captureWindow.AreaSelected += CaptureAreaSelected;
                captureWindow.Closed += (object? sender, EventArgs e) => {
                    captureWindow.AreaSelected -= CaptureAreaSelected;
                    captureWindow = null;
                };
            } catch (Exception) {
                MessageBox.Show("キャプチャー対象のプロセスが取得できませんでした。", null, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /** OCRエンジンが変更された */
        private void ocrServiceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var service = ocrServiceComboBox.SelectedItem as OcrService;
            if (service != null) {
                // デフォルトのOCRサービスを変更
                ocrService = service;
                ocrManager.OcrEngine = ocrServiceName = service.Name;

                // 言語一覧を選択したOCRサービスがサポートする言語一覧にする
                var languages = service.GetLanguages();
                var currentOcrLanguageCode = ocrLanguageCode;
                ocrLanguageComboBox.ItemsSource = languages;
                ocrLanguageComboBox.SelectedItem = languages.Find(lang => lang.Code == currentOcrLanguageCode);
            } else {
                ocrService = null;
                ocrManager.OcrEngine = ocrServiceName = "";
            }
            SetOcrStatusText();
        }

        /** OCRの読み取り言語が変更された */
        private void ocrLanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var lang = ocrLanguageComboBox.SelectedItem as Language;
            if (lang != null) {
                ocrManager.OcrEngine = ocrService?.Name ?? "";  // 別のOCRが設定されているかもしれないので、OCRも設定する
                ocrManager.OcrLanguage = ocrLanguageCode = lang.Code;
            } else {
                ocrLanguageCode = null;
            }
            SetOcrStatusText();
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
                ocrManager.TranslationEngine = translationServiceName = service.Name;

                var languages = service.GetLanguages();
                var currentLanguageCode = translationLanguageCode;
                translationLanguageComboBox.ItemsSource = languages;
                translationLanguageComboBox.SelectedItem = languages.Find(lang => lang.Code == currentLanguageCode);
            } else {
                translationService = null;
                ocrManager.TranslationEngine = translationServiceName = "";
            }
            SetTranslationStatusText();
        }

        /** 翻訳先言語が変更された */
        private void translationLanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var lang = translationLanguageComboBox.SelectedItem as Language;
            if (lang != null) {
                ocrManager.TranslationEngine = translationService?.Name ?? "";
                ocrManager.TranslationLanguage = translationLanguageCode = lang.Code;
            } else {
                translationLanguageCode = null;
            }
            SetTranslationStatusText();
        }

        /** 翻訳ボタンが押された */
        private async void translationButton_Click(object sender, RoutedEventArgs e) {
            await TranslateAsync();
        }

        /** OCRテキストソースが選択された */
        private void obsTextSourceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            obsTextSource = obsTextSourceComboBox.SelectedItem as string ?? "";
            SetObsStatusText();
        }

        /** OBSに表示された翻訳結果をクリアする */
        private void clearObsTranslatedTextButton_Click(object sender, RoutedEventArgs e) {
            ClearObsTranslatedText();
        }

        /** 音声読み上げエンジンが変更された */
        private void speechServiceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var service = speechServiceComboBox.SelectedItem as SpeechService;
            if (service != null) {
                speechService = service;
                ocrManager.OcrSpeechEngine = speechServiceName = service.Name;

                var voices = service.GetVoices();
                var currentVoiceId = speechVoiceId;
                speechVoiceComboBox.ItemsSource = voices;
                speechVoiceComboBox.SelectedItem = voices.Find(voice => voice.Id == currentVoiceId);
            } else {
                speechService = null;
                ocrManager.OcrSpeechEngine = speechServiceName = "";
            }
            SetSpeechStatusText();
        }

        /** 音声読み上げボイスが変更された */
        private void speechVoiceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var voice = speechVoiceComboBox.SelectedItem as Voice;
            if (voice != null) {
                ocrManager.OcrSpeechVoice = speechVoiceId = voice.Id;
            }

            SetSpeechButtonEnabled();
            SetSpeechStatusText();
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

                if (ocrService == null) {
                    MessageBox.Show("OCRに使用するサービスが設定されていません。", null, MessageBoxButton.OK, MessageBoxImage.Error);
                } else if (ocrLanguageCode == null) {
                    MessageBox.Show("OCRで読み取る言語が設定されていません。", null, MessageBoxButton.OK, MessageBoxImage.Error);
                } else if (this.ocrService != null) {
                    var bitmap = CaptureWindow();
                    if (bitmap == null) {
                        MessageBox.Show("画面をキャプチャー出来ませんでした。", null, MessageBoxButton.OK, MessageBoxImage.Error);
                    } else {
                        notionSaveId = null;
                        System.Windows.Forms.Clipboard.SetImage(bitmap);
                        OcrResult result = await this.ocrService.ExecuteAsync(bitmap, ocrLanguageCode);
                        if (result.Text != null) {
                            this.ocrTextBox.Text = result.Text;
                            this.translatedTextBox.Text = "";

                            // 文字が取得できた場合、読み上げと翻訳をする
                            if (result.Text.Length > 0) {
                                if (viewModel.SpeechWhenOcrRun.Value) {
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
            if (translationService != null && translationLanguageCode != null) {
                try {
                    var result = await translationService.TranslateAsync(ocrTextBox.Text, null, translationLanguageCode);
                    if (result != null && result.Translations.Count > 0) {
                        translatedTextBox.Text = result.Translations[0].Text;

                        // OBSに接続済みで、翻訳結果表示先が指定されている場合、OBS上に翻訳結果を表示する。
                        var subtitlesService = this.serviceManager.GetService<ObsSubtitlesService>();
                        if (obsService != null && obsService.IsConnected && subtitlesService != null && obsTextSource != "") {
                            await subtitlesService.SetTextAsync(obsTextSource , result.Translations[0].Text ?? "");
                        }

                        if (ocrManager.Archive == OcrArchives.Notion) {
                            var notion = new Notion.Notion(ocrManager.NotionToken);
                            if (notionSaveId == null) {
                                notionSaveId = await notion.AddOcrResult(ocrManager, viewModel.WindowTitle.Value, ocrTextBox.Text, result.Translations[0].Text, translationService.DisplayName);
                            } else {
                                await notion.UpdateOcrResult(ocrManager, notionSaveId, viewModel.WindowTitle.Value, ocrTextBox.Text, result.Translations[0].Text, translationService.DisplayName);
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
        async private Task GetObsTextSourcesAsync() {
            var currentSource = this.obsTextSource;
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

                // コンボボックスを更新
                var items = nameList.Distinct();
                this.obsTextSourceComboBox.ItemsSource = items;

                // 更新前に選択されていたアイテムと同じものがあれば再選択する
                var newSelectedItem = items.Where(i => i == currentSource).FirstOrDefault();
                this.obsTextSourceComboBox.SelectedItem = newSelectedItem;
            }
        }

        /** OBSに表示された翻訳結果をクリアする */
        private async void ClearObsTranslatedText() {
            var subtitlesService = this.serviceManager.GetService<ObsSubtitlesService>();
            if (obsService?.IsConnected == true && subtitlesService != null && obsTextSource != "") {
                await subtitlesService.SetTextAsync(obsTextSource, "");
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
            try {
                if (!captureArea.IsEmpty) {
                    Process process = Process.GetProcessById(processId);
                    var dpi = GetDpiForWindow(process.MainWindowHandle);

                    // 画面をキャプチャーする
                    RECT rect;
                    if (GetWindowRect(process.MainWindowHandle, out rect)) {
                        var left = rect.left + captureArea.Left;
                        var top = rect.top + captureArea.Top;
                        var width = captureArea.Width;
                        var height = captureArea.Height;

                        Bitmap screenBitmap = new Bitmap(width, height);
                        using (Graphics g = Graphics.FromImage(screenBitmap)) {
                            g.CopyFromScreen(left, top, 0, 0, new System.Drawing.Size(width, height));
                        }

                        // キャプチャ画像の拡大が設定されている場合は拡大する
                        Bitmap? scaledBitmap;
                        if (captureScale > 1) {
                            scaledBitmap = new Bitmap(width * captureScale, height * captureScale);
                            using (Graphics g = Graphics.FromImage(scaledBitmap)) {
                                g.DrawImage(screenBitmap, new Rectangle(0, 0, width * captureScale, height * captureScale), new Rectangle(0, 0, width, height), GraphicsUnit.Pixel);
                            }
                        }
                        else {
                            scaledBitmap = screenBitmap;
                        }
                        return scaledBitmap;
                    }
                }
            }
            catch (Exception) {
                MessageBox.Show("キャプチャー対象のプロセスが取得できませんでした。", null, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return null;
        }

        /** OBSの接続状態が変更された */
        private async void obsConnectionChangedHandler(object? sender , bool e) {
            if (e == true) {
                this.connectObs.Visibility = Visibility.Collapsed;
                this.obsSettings.Visibility = Visibility.Visible;

                if (obsStatusPopup.IsOpen) {
                    await GetObsTextSourcesAsync();
                }
            } else {
                this.connectObs.Visibility = Visibility.Visible;
                this.obsSettings.Visibility = Visibility.Collapsed;
            }
        }

        /** OBSに接続する */
        private async void connectObsButton_Click(object sender , RoutedEventArgs e) {
            if (obsService != null) {
                await obsService.ConnectAsync();
            }
        }

        /**
         * ショートカットが実行された
         */
        private async void ShortcutRun(object? sender, Shortcut.Shortcut shortcut) {
            if (this.IsActive == true && ocrShortcutKeyTextBox.IsFocused == true) {
                ocrShortcutKeyTextBox.Text = shortcut.ShortcutKey;
                SetShortcutKeyStatusText();
            } else if (this.IsActive == true && clearTranslatedShortcutKeyTextBox.IsFocused == true) {
                clearTranslatedShortcutKeyTextBox.Text = shortcut.ShortcutKey;
            } else {
                if (ocrButton.IsEnabled == true && shortcut.ShortcutKey == ocrShortcutKeyTextBox.Text) {
                    await Ocr();
                } else if (shortcut.ShortcutKey == clearTranslatedShortcutKeyTextBox.Text) {
                    ClearObsTranslatedText();
                }
            }
        }

        /** OCR実行のショートカットを削除する */
        private void removeOcrShortcutKeyButton_Click(object sender, RoutedEventArgs e) {
            ocrShortcutKeyTextBox.Text = "";
            SetShortcutKeyStatusText();
        }

        /** OCR実行のショートカットをデフォルト設定に戻す */
        private void resetOcrShortcutKeyButton_Click(object sender, RoutedEventArgs e) {
            ocrShortcutKeyTextBox.Text = ocrManager.OcrShortcut.ShortcutKey;
            SetShortcutKeyStatusText();
        }

        /** OCR結果の字幕をクリアするショートカットを削除する */
        private void removeClearTranslatedTextShortcutKey_Click(object sender, RoutedEventArgs e) {
            clearTranslatedShortcutKeyTextBox.Text = "";
        }

        /** OCR結果の字幕をクリアするショートカットをデフォルト設定に戻す */
        private void resetClearTranslatedTextShortcutKey_Click(object sender, RoutedEventArgs e) {
            clearTranslatedShortcutKeyTextBox.Text = ocrManager.ClearObsTextShortcut.ShortcutKey;
        }

        /** OCRで取得した文字列をweblioで検索する */
        private void searchByWeblioMenuItem_Click(object sender, RoutedEventArgs e) {
            var text = ocrTextBox.SelectedText.Length > 0 ? ocrTextBox.SelectedText : ocrTextBox.Text;
            var url = "https://ejje.weblio.jp/content/" + Uri.EscapeDataString(text.Trim());
            ProcessStartInfo pi = new ProcessStartInfo() {
                FileName = url,
                UseShellExecute = true,
            };
            System.Diagnostics.Process.Start(pi);
        }

        /** OCRで取得した文字列をALCで検索する */
        private void searchByAlcMenuItem_Click(object sender, RoutedEventArgs e) {
            var text = ocrTextBox.SelectedText.Length > 0 ? ocrTextBox.SelectedText : ocrTextBox.Text;
            var url = "https://eow.alc.co.jp/search?q=" + Uri.EscapeDataString(text.Trim());
            ProcessStartInfo pi = new ProcessStartInfo() {
                FileName = url,
                UseShellExecute = true,
            };
            System.Diagnostics.Process.Start(pi);
        }

        /** OCRで取得した文字列をgoogleで検索する */
        private void searchByGoogleMenuItem_Click(object sender, RoutedEventArgs e) {
            var text = ocrTextBox.SelectedText.Length > 0 ? ocrTextBox.SelectedText : ocrTextBox.Text;
            var url = "https://www.google.com/search?q=" + Uri.EscapeDataString(text.Trim());
            ProcessStartInfo pi = new ProcessStartInfo() {
                FileName = url,
                UseShellExecute = true,
            };
            System.Diagnostics.Process.Start(pi);
        }

        /** OCRウィンドウを複製する */
        private void duplicateStatusButton_Click(object sender, RoutedEventArgs e) {
            var ocrWindow = new OcrWindow(this);
            ocrWindow.Show();
        }

        /**
         */
        private async void speechMenuItem_Click(object sender, RoutedEventArgs e) {
            await SpeechOcrTextAsync();
        }

        /**
         * OCRテキストボックスで選択範囲を切り取り
         * OCRテキストボックスのコンテキストメニューを置き換えており、一般的なメニューが消えているので追加
         */
        private void cutMenuItem_Click(object sender, RoutedEventArgs e) {
            ocrTextBox.Cut();
        }

        /**
         * OCRテキストボックスで選択範囲をコピー
         * OCRテキストボックスのコンテキストメニューを置き換えており、一般的なメニューが消えているので追加
         */
        private void copyMenuItem_Click(object sender, RoutedEventArgs e) {
            ocrTextBox.Copy();
        }

        /**
         * OCRテキストボックスで選択範囲にペースト
         * OCRテキストボックスのコンテキストメニューを置き換えており、一般的なメニューが消えているので追加
         */
        private void pasteMemuItem_Click(object sender, RoutedEventArgs e) {
            ocrTextBox.Paste();
        }

        private void SetOcrStatusText() {
            var service = ocrManager.GetOcrEngine();
            if (service != null) {
                ocrStatusText.Text = service.DisplayName;
            } else {
                ocrStatusText.Text = "";
            }
        }

        private void SetTranslationStatusText() {
            var service = ocrManager.GetTranslationEngine();
            if (service != null) {
                translationStatusText.Text = service.DisplayName;
            } else {
                translationStatusText.Text = "";
            }
        }

        private void SetObsStatusText() {
            obsStatusText.Text = obsTextSource;
        }

        private void SetSpeechStatusText() {
            var service = serviceManager.GetServices<SpeechService>().Find(service => service.Name == ocrManager.OcrSpeechEngine);
            if (service != null) {
                var voice = service.GetVoices().Find(voice => voice.Id == ocrManager.OcrSpeechVoice);
                if (voice != null) {
                    speechStatusText.Text = voice.Name;
                } else {
                    speechStatusText.Text = service.DisplayName;
                }
            } else {
                speechStatusText.Text = "";
            }
        }

        private void SetShortcutKeyStatusText() {
            this.shortcutKeyStatusText.Text = ocrShortcutKeyTextBox.Text;
        }

        private void ocrStatusButton_Click(object sender, RoutedEventArgs e) {
            ocrStatusPopup.IsOpen = true;
        }

        private void translationStatusButton_Click(object sender, RoutedEventArgs e) {
            translationStatusPopup.IsOpen = true;
        }

        private async void obsStatusButton_Click(object sender, RoutedEventArgs e) {
            await GetObsTextSourcesAsync();
            obsStatusPopup.IsOpen = true;
        }

        private void speechStatusButton_Click(object sender, RoutedEventArgs e) {
            speechStatusPopup.IsOpen = true;
        }

        private void shortcutKeyStatusButton_Click(object sender, RoutedEventArgs e) {
            shortcutKeyStatusPopup.IsOpen = true;
        }

        /** メインウィンドウがクローズされた場合、このウィンドウも終了する */
        private void MainWindowClosed(object? sender, EventArgs e) {
            this.Close();
        }
    }
}
