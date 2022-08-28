﻿using sikusiSubtitles.Shortcut;
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
        private OcrService? ocrService;
        private ShortcutService? shortcutService;
        private List<TranslationService> translationServices;
        private TranslationService? translationService;

        private int processId;
        private Rectangle captureArea;

        public OcrForm(Service.ServiceManager serviceManager, int processId, Rectangle captureArea) {
            InitializeComponent();

            this.serviceManager = serviceManager;

            // OCRサービスを取得
            this.ocrService = this.serviceManager.GetActiveService<OcrService>();
            if (this.ocrService != null) {
                this.ocrService.OcrFinished += OrcFinished;
            }

            // 翻訳サービスを取得
            this.translationServices = this.serviceManager.GetServices<TranslationService>();
            var activeTranslationService = this.serviceManager.GetActiveService<TranslationService>();
            for (var i = 0; i < this.translationServices.Count; i++) {
                var service = this.translationServices[i];
                this.translationComboBox.Items.Add(service.DisplayName);
                if (service == activeTranslationService) {
                    this.translationComboBox.SelectedIndex = i;
                }
            }
            if (this.translationComboBox.SelectedIndex == -1) {
                this.translationComboBox.SelectedIndex = 0;
            }

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

        private void OcrForm_FormClosed(object sender, FormClosedEventArgs e) {
            if (this.ocrService != null) {
                this.ocrService.OcrFinished -= OrcFinished;
            }

            if (this.shortcutService != null) {
              this.shortcutService.ShortcutRun -= ShortcutRun;
            }
        }

        private void ocrButton_Click(object sender, EventArgs e) {
            Ocr();
        }

        private void translateButton_Click(object sender, EventArgs e) {
            Translate();
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
            if (this.orcTextBox.Text == "") {
                MessageBox.Show("文字が入力されていません。", null, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.translateButton.Enabled = true;
            } else if (this.translationComboBox.SelectedIndex == -1) {
                MessageBox.Show("翻訳に使用するサービスが設定されていません。", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.translateButton.Enabled = true;
            } else {
                DoTranslate();
            }
        }
        private void DoTranslate() {
            this.translationService = this.translationServices[this.translationComboBox.SelectedIndex];
            this.translationService.Translated += Translated;
            this.translationService.Translate(this, this.orcTextBox.Text, "ja");
        }

        private void OrcFinished(object? sender, OcrResult result) {
            if (result.Obj == this) {
                var text = result.Text.Replace("\r\n", " ").Replace("\r", " ").Replace("\n", " ");
                this.orcTextBox.Text = text;
                this.Translate();
                this.ocrButton.Enabled = true;
            }
        }

        private void Translated(object? sender, TranslationResult result) {
            if (result.Obj == this) {
                if (result.Translations.Count > 0) {
                    this.translatedTextBox.Text = result.Translations[0].Text;
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
         * ショートカットから処理を実行する
         */
        private void ShortcutRun(object? sender, Shortcut.Shortcut shortcut) {
            if (shortcut.Name == "ExecuteOCR" && this.ocrButton.Enabled == true) {
                Ocr();
            }
        }
    }
}
