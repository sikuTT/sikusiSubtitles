﻿using sikusiSubtitles.OBS;
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
        private OcrService? ocrService;

        private List<TranslationService> translationServices = new List<TranslationService>();
        private TranslationService? translationService;

        private ObsService? obsService;
        private SubtitlesService? subtitlesService;

        private ShortcutService? shortcutService;

        private int processId;
        private Rectangle captureArea;

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
            this.ocrManager = this.serviceManager.GetManager<OcrServiceManager>();
            if (this.ocrManager != null) {
                this.ocrService = ocrManager.GetEngine();
                if (this.ocrService != null) {
                    this.ocrService.OcrFinished += OcrFinished;
                }
            }

            this.translationServices = this.serviceManager.GetServices<TranslationService>();

            // 翻訳エンジンの一覧をコンボボックスに設定
            this.translationServices.ForEach(service => this.translationEngineComboBox.Items.Add(service.DisplayName));
            var i = this.translationServices.FindIndex(service => service.Name == ocrManager?.TranslationService?.Name);
            this.translationEngineComboBox.SelectedIndex = i != -1 ? i : 0;

            // OBSのテキストソースの一覧を取得
            obsService = this.serviceManager.GetService<ObsService>();
            subtitlesService = this.serviceManager.GetService<SubtitlesService>();
            GetObsTextSourcesAsync();
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
                langs.ForEach(lang => {
                    var i = this.translationLangComboBox.Items.Add(lang.Item2);
                    if (lang.Item1 == this.ocrManager?.TranslationLanguage) {
                        this.translationLangComboBox.SelectedIndex = i;
                    }
                });
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
            GetObsTextSourcesAsync();
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
        private async void DoTranslate() {
            this.translationService = this.translationServices[this.translationEngineComboBox.SelectedIndex];
            var langs = this.translationService.GetLanguages();
            var lang = langs[this.translationLangComboBox.SelectedIndex];
            var result = await this.translationService.TranslateAsync(this.ocrTextBox.Text, null, lang.Item1);
            if (result.Translations.Count > 0) {
                this.translatedTextBox.Text = result.Translations[0].Text;

                // OBSに接続済みで、翻訳結果表示先が指定されている場合、OBS上に翻訳結果を表示する。
                if (obsService != null && obsService.ObsSocket.IsConnected && subtitlesService != null && this.obsTextSourceComboBox.SelectedItem != null) {
                    var sourceName = this.obsTextSourceComboBox.SelectedItem.ToString();
                    if (sourceName != null) {
                        await this.subtitlesService.SetTextAsync(sourceName, result.Translations[0].Text ?? "");
                    }
                }
            } else {
                // 翻訳に失敗
                MessageBox.Show("翻訳に失敗しました。", null, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            this.translateButton.Enabled = true;
        }

        /**
         * OBSのテキストソースの一覧を取得する。
         */
        async private void GetObsTextSourcesAsync() {
            this.obsTextSourceComboBox.Items.Clear();
            var nameList = new List<string>() { "" };
            if (obsService != null && obsService.ObsSocket.IsConnected) {
                // シーン一覧を取得する
                var sceneList= await obsService.ObsSocket.GetSceneListAsync();
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
            }
            this.obsTextSourceComboBox.Items.AddRange(nameList.Distinct().ToArray());
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
         * ショートカットが実行された
         */
        private void ShortcutRun(object? sender, Shortcut.Shortcut shortcut) {
            if (shortcut.Name == "ExecuteOCR" && this.ocrButton.Enabled == true) {
                Ocr();
            }
        }
    }
}
