using sikusiSubtitles.OBS;
using sikusiSubtitles.Shortcut;
using sikusiSubtitles.Translation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static sikusiSubtitles.OCR.ScreenCapture;

namespace sikusiSubtitles.OCR {
    public partial class OcrForm : Form {
        private Service.ServiceManager serviceManager;

        private OcrServiceManager? ocrManager;
        private List<OcrService> ocrServices = new List<OcrService>();
        private OcrService? ocrService;
        private List<Tuple<string, string>> ocrLanguages = new List<Tuple<string, string>>();
        private string? ocrLanguage;

        private List<TranslationService> translationServices = new List<TranslationService>();
        private TranslationService? translationService;
        private List<Tuple<string, string>> translationLanguages = new List<Tuple<string, string>>();
        private string? translationLanguage;

        private ShortcutService? shortcutService;

        private int processId;
        private Rectangle captureArea;

        public OcrForm(Service.ServiceManager serviceManager, int processId, Rectangle captureArea) {
            this.serviceManager = serviceManager;

            InitializeComponent();

            // キャプチャー対象
            this.processId = processId;
            this.captureArea = captureArea;

            // キャプチャー対象のウィンドウ名をフォームに表示する。
            Process process = Process.GetProcessById(processId);
            this.windowNameTextBox.Text = process.MainWindowTitle;

            // ショートカットイベントを取得
            this.shortcutService = this.serviceManager.GetService<ShortcutService>();
            if (shortcutService != null) {
                shortcutService.ShortcutRun += ShortcutRun;
            }
        }

        private void OcrForm_Load(object sender, EventArgs e) {
            // OCRサービス
            this.ocrManager = this.serviceManager.GetManager<OcrServiceManager>();
            if (this.ocrManager != null) {
                this.ocrService = ocrManager.GetOcrEngine();
            }

            // OCRサービス一覧をコンボボックスに設定
            ocrServices = this.serviceManager.GetServices<OcrService>();
            ocrServices.ForEach(service => {
                var i = ocrEngineComboBox.Items.Add(service.DisplayName);
                if (service.Name == ocrService?.Name) ocrEngineComboBox.SelectedIndex = i;
            });

            // 翻訳エンジンの一覧をコンボボックスに設定
            this.translationServices = this.serviceManager.GetServices<TranslationService>();
            this.translationServices.ForEach(service => {
                var i = translationEngineComboBox.Items.Add(service.DisplayName);
                if (service.Name == ocrManager?.TranslationEngine) translationEngineComboBox.SelectedIndex = i;
            });

            // OBSに接続されている場合、テキストソースを取得する
            GetObsTextSourcesAsync();
        }

        private void OcrForm_FormClosed(object sender, FormClosedEventArgs e) {
            if (this.shortcutService != null) {
              this.shortcutService.ShortcutRun -= ShortcutRun;
            }
        }

        /** OCRエンジンが変更された */
        private void ocrEngineComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            ocrLanguageComboBox.Items.Clear();

            if (ocrEngineComboBox.SelectedIndex != -1) {
                ocrService = ocrServices[ocrEngineComboBox.SelectedIndex];
                ocrLanguages = ocrService.GetLanguages();
                ocrLanguages.ForEach(lang => {
                    var i = ocrLanguageComboBox.Items.Add(lang.Item2);
                    if (lang.Item1 == ocrManager?.OcrLanguage) ocrLanguageComboBox.SelectedIndex = i;
                });
            } else {
                ocrService = null;
            }
        }

        /** 読み取り言語が変更された */
        private void ocrLanguageComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (ocrLanguageComboBox.SelectedIndex != -1) {
                ocrLanguage = ocrLanguages[ocrLanguageComboBox.SelectedIndex].Item1;
            } else {
                ocrLanguage = null;
            }
        }

        /** 翻訳エンジンが変更された */
        private void translationEngineComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            translationLangComboBox.Items.Clear();
            if (translationEngineComboBox.SelectedIndex != -1) {
                this.translationService = translationServices[translationEngineComboBox.SelectedIndex];
                translationLanguages = this.translationService.GetLanguages();
                translationLanguages.ForEach(lang => {
                    var i = this.translationLangComboBox.Items.Add(lang.Item2);
                    if (lang.Item1 == ocrManager?.TranslationLanguage) translationLangComboBox.SelectedIndex = i;
                });
            } else {
                this.translationService = null;
            }
        }

        /** 翻訳先が選択された */
        private void translationLangComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (translationLangComboBox.SelectedIndex != -1) {
                this.translationLanguage = this.translationLanguages[translationLangComboBox.SelectedIndex].Item1;
            } else {
                this.translationLanguage = null;
            }
        }

        /**
         * OCRを実行
         */
        private void ocrButton_Click(object sender, EventArgs e) {
            Ocr();
        }

        /**
         * 翻訳を実行
         */
        private void translateButton_Click(object sender, EventArgs e) {
            Translate();
        }

        /**
         * OBSのテキストソースの一覧を再取得
         */
        private void obsTextSourceRefreshButton_Click(object sender, EventArgs e) {
            GetObsTextSourcesAsync();
        }

        /** OBSのテキストをクリア */
        private void obsClearButton_Click(object sender, EventArgs e) {
            ClearObsTranslatedText();
        }

        private Bitmap? CaptureWindow() {
            Process process = Process.GetProcessById(processId);

            // 画面をキャプチャーする
            RECT rect;
            if (GetWindowRect(process.MainWindowHandle, out rect)) {
                var left = rect.left + captureArea.Left;
                var top = rect.top + captureArea.Top;
                var width = captureArea.Width;
                var height = captureArea.Height;

                Bitmap bitmap = new Bitmap(width, height);
                using (Graphics g = Graphics.FromImage(bitmap)) {
                    g.CopyFromScreen(left, top, 0, 0, new Size(width, height));
                }
                return bitmap;
            }
            return null;
        }

        private async void Ocr() {
            this.ocrButton.Enabled = false;
            if (this.ocrService == null) {
                MessageBox.Show("OCRに使用するサービスが設定されていません。", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else {
                var bitmap = CaptureWindow();
                if (bitmap == null) {
                    MessageBox.Show("画面をキャプチャー出来ませんでした。", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
                } else {
                    if (ocrLanguage != null) {
                        string? text = await this.ocrService.ExecuteAsync(bitmap, ocrLanguage);
                        if (text != null) {
                            text = text.Replace("\r\n", " ").Replace("\r", " ").Replace("\n", " ");
                            this.ocrTextBox.Text = text;
                            this.Translate();
                        }
                    }
                }
            }
            this.ocrButton.Enabled = true;
        }

        private void Translate() {
            this.translateButton.Enabled = false;
            if (this.ocrTextBox.Text == "") {
                MessageBox.Show("文字が入力されていません。", null, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.translateButton.Enabled = true;
            } else if (this.translationEngineComboBox.SelectedIndex == -1) {
                MessageBox.Show("翻訳に使用するサービスが設定されていません。", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.translateButton.Enabled = true;
            } else if (this.translationLangComboBox.SelectedIndex == -1) {
                MessageBox.Show("翻訳先の言語が設定されていません。", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.translateButton.Enabled = true;
            } else {
                DoTranslate();
            }
        }
        private async void DoTranslate() {
            if (translationService != null && translationLanguage != null) {
                var result = await this.translationService.TranslateAsync(this.ocrTextBox.Text, null, translationLanguage);
                if (result.Translations.Count > 0) {
                    this.translatedTextBox.Text = result.Translations[0].Text;

                    // OBSに接続済みで、翻訳結果表示先が指定されている場合、OBS上に翻訳結果を表示する。
                    var obsService = this.serviceManager.GetService<ObsService>();
                    var subtitlesService = this.serviceManager.GetService<SubtitlesService>();
                    if (obsService != null && obsService.ObsSocket.IsConnected && subtitlesService != null && obsTextSourceComboBox.SelectedItem != null) {
                        var sourceName = obsTextSourceComboBox.SelectedItem.ToString();
                        if (sourceName != null) {
                            await subtitlesService.SetTextAsync(sourceName, result.Translations[0].Text ?? "");
                        }
                    }
                } else {
                    // 翻訳に失敗
                    MessageBox.Show("翻訳に失敗しました。", null, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            this.translateButton.Enabled = true;
        }

        /**
         * OBSのテキストソースの一覧を取得する。
         */
        async private void GetObsTextSourcesAsync() {
            this.obsTextSourceComboBox.Items.Clear();

            // OBSのテキストソースの一覧を取得
            var obsService = this.serviceManager.GetService<ObsService>();

            if (obsService != null && obsService.ObsSocket.IsConnected) {
                var nameList = new List<string>() { "" };
                // シーン一覧を取得する
                var sceneList = await obsService.ObsSocket.GetSceneListAsync();
                var sceneNames = sceneList?.d?.responseData?.scenes?.Select(scene => scene.sceneName).ToList();
                if (sceneNames != null) {
                    // 各シーン内のソースを取得し、GDIテキストだけを取り出す
                    foreach (var sceneName in sceneNames) {
                        // シーン内のソース一覧を取得し、GDIテキストとグループだけを取り出す
                        var sceneItemList = await obsService.ObsSocket.GetSceneItemListAsync(sceneName);
                        var sourceItems = sceneItemList?.d?.responseData?.sceneItems?.Where(item => item.inputKind == "text_gdiplus_v2" || item.isGroup == true).ToList();
                        if (sourceItems != null) {
                            // シーン内のアイテムを1個ずつ処理
                            foreach (var item in sourceItems) {
                                if (item.isGroup == true) {
                                    // グループの場合、グループ内のソース一覧を取得し、その中からGDIテキストだけを取得する
                                    var groupItemList = await obsService.ObsSocket.GetGroupSceneItemListAsync(item.sourceName);
                                    var groupSourceItems = groupItemList?.d?.responseData?.sceneItems?.Where(item => item.inputKind == "text_gdiplus_v2").Select(item => item.sourceName).ToList();
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
                this.obsTextSourceComboBox.Items.AddRange(nameList.Distinct().ToArray());
            }
        }

        private async void ClearObsTranslatedText() {
            var obsService = this.serviceManager.GetService<ObsService>();
            var subtitlesService = this.serviceManager.GetService<SubtitlesService>();
            if (obsService?.IsConnected == true && subtitlesService != null) {
                var sourceName = obsTextSourceComboBox.SelectedItem.ToString();
                if (sourceName != null) {
                    await subtitlesService.SetTextAsync(sourceName, "");
                }
            }
        }

        /**
         * ショートカットが実行された
         */
        private void ShortcutRun(object? sender, Shortcut.Shortcut shortcut) {
            if (shortcut.Name == "execute-ocr" && this.ocrButton.Enabled == true) {
                Ocr();
            } else if (shortcut.Name == "clear-ocr-translated-text") {
                ClearObsTranslatedText();
            }
        }
    }
}
