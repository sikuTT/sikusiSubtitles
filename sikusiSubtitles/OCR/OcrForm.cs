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
        private OcrCommonService? ocrCommonService;
        private OcrService? ocrService;

        private List<TranslationService> translationServices = new List<TranslationService>();
        private TranslationService? translationService;

        private ObsService? obsService;
        private SubtitlesService? subtitlesService;

        private ShortcutService? shortcutService;

        private int processId;
        private Rectangle captureArea;
        private List<string> obsTextSources = new List<string>();

        public OcrForm(Service.ServiceManager serviceManager, int processId, Rectangle captureArea) {
            InitializeComponent();

            this.serviceManager = serviceManager;

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
            this.ocrCommonService = this.serviceManager.GetService<OcrCommonService>();
            this.translationServices = this.serviceManager.GetServices<TranslationService>();
            this.ocrService = this.serviceManager.GetActiveService<OcrService>();
            if (this.ocrService != null) {
                this.ocrService.OcrFinished += OcrFinished;
            }

            // 翻訳エンジンの一覧をコンボボックスに設定
            this.translationServices.ForEach(service => this.translationEngineComboBox.Items.Add(service.DisplayName));
            var i = this.translationServices.FindIndex(service => service.Name == ocrCommonService?.TranslationService?.Name);
            this.translationEngineComboBox.SelectedIndex = i != -1 ? i : 0;

            // OBSのテキストソースの一覧を取得
            obsService = this.serviceManager.GetService<ObsService>();
            subtitlesService = this.serviceManager.GetService<SubtitlesService>();
            GetObsTextSources();
        }

        private void OcrForm_FormClosed(object sender, FormClosedEventArgs e) {
            if (this.ocrService != null) {
                this.ocrService.OcrFinished -= OcrFinished;
            }

            if (this.shortcutService != null) {
              this.shortcutService.ShortcutRun -= ShortcutRun;
            }
        }

        /** 翻訳エンジンが変更された */
        private void translationEngineComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            translationLangComboBox.Items.Clear();
            if (translationEngineComboBox.SelectedIndex != -1) {
                this.translationService = translationServices[translationEngineComboBox.SelectedIndex];
                var langs = this.translationService.GetLanguages();
                foreach (var lang in langs) {
                    var i = this.translationLangComboBox.Items.Add(lang.Item2);
                    if (lang.Item1 == this.ocrCommonService?.TranslationLanguage) {
                        this.translationLangComboBox.SelectedIndex = i;
                    }
                }
            } else {
                this.translationService = null;
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
            GetObsTextSources();
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

        private void Ocr() {
            this.ocrButton.Enabled = false;
            if (this.ocrService == null) {
                MessageBox.Show("OCRに使用するサービスが設定されていません。", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.ocrButton.Enabled = true;
            } else {
                var bitmap = CaptureWindow();
                if (bitmap == null) {
                    MessageBox.Show("画面をキャプチャー出来ませんでした。", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.ocrButton.Enabled = true;
                } else {
                    this.ocrService.Execute(this, bitmap);
                }
            }
        }

        private void Translate() {
            this.translateButton.Enabled = false;
            if (this.ocrTextBox.Text == "") {
                MessageBox.Show("文字が入力されていません。", null, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.translateButton.Enabled = true;
            } else if (this.translationEngineComboBox.SelectedIndex == -1) {
                MessageBox.Show("翻訳に使用するサービスが設定されていません。", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.translateButton.Enabled = true;
            } else {
                DoTranslate();
            }
        }
        private void DoTranslate() {
            this.translationService = this.translationServices[this.translationEngineComboBox.SelectedIndex];
            this.translationService.Translated += Translated;
            this.translationService.Translate(this, this.ocrTextBox.Text, "ja");
        }

        /**
         * OBSのテキストソースの一覧を取得する。
         */
        private void GetObsTextSources() {
            this.obsTextSourceComboBox.Items.Clear();
            this.obsTextSourceComboBox.Items.Add("");
            if (obsService != null && obsService.ObsSocket.IsConnected) {
                var sources = obsService.ObsSocket.GetSourcesList();
                sources.ForEach(source => {
                    if (source.TypeID == "text_gdiplus_v2") {
                        this.obsTextSources.Add(source.Name);
                        this.obsTextSourceComboBox.Items.Add(source.Name);
                    }
                });
            }
        }

        /**
         * OCRが完了した
         */
        private void OcrFinished(object? sender, OcrResult result) {
            if (result.Obj == this) {
                var text = result.Text.Replace("\r\n", " ").Replace("\r", " ").Replace("\n", " ");
                this.ocrTextBox.Text = text;
                this.Translate();
                this.ocrButton.Enabled = true;
            }
        }

        /**
         * 翻訳が完了した
         */
        private void Translated(object? sender, TranslationResult result) {
            if (result.Obj == this) {
                if (result.Translations.Count > 0) {
                    this.translatedTextBox.Text = result.Translations[0].Text;

                    // OBSに接続済みで、翻訳結果表示先が指定されている場合、OBS上に翻訳結果を表示する。
                    if (obsService != null && obsService.ObsSocket.IsConnected && subtitlesService != null && obsTextSourceComboBox.SelectedIndex > 0) {
                        var sourceName = obsTextSources[obsTextSourceComboBox.SelectedIndex - 1];
                        this.subtitlesService.SetText(sourceName, result.Translations[0].Text ?? "");
                    }
                }

                // 毎回、翻訳エンジンを選択できるので、今回の設定はクリアする。
                if (this.translationService != null) {
                    this.translationService.Translated -= Translated;
                    this.translationService = null;
                }
                this.translateButton.Enabled = true;
            }
        }

        /**
         * ショートカットが実行された
         */
        private void ShortcutRun(object? sender, Shortcut.Shortcut shortcut) {
            if (shortcut.Name == "ExecuteOCR" && this.ocrButton.Enabled == true) {
                Ocr();
            }
        }
    }
}
