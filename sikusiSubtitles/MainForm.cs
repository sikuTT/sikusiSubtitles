using sikusiSubtitles.OBS;
using sikusiSubtitles.OCR;
using sikusiSubtitles.Shortcut;
using sikusiSubtitles.SpeechRecognition;
using sikusiSubtitles.Translation;
using System.Diagnostics;

namespace sikusiSubtitles {
    public partial class MainForm : Form {
        Service.ServiceManager serviceManager = new Service.ServiceManager();
        SettingPage[] pages;

        // Speech Recognition
        SpeechRecognitionService? speechRecognitionService;
        SpeechRecognitionPage speechRecognitionPage;
        ChromeSpeechRecognitionPage chromeSpeechRecognitionPage;
        AzureSpeechRecognitionPage azureSpeechRecognitionPage;
        AmiVoiceSpeechRecognitionPage amiVoiceSpeechRecognitionPage;
        ObsPage obsPage;
        SubtitlesPage subtitlesPage;

        // translation
        TranslationPage translationPage;
        AzureTranslationPage azureTranslationPage;
        GoogleBasicTranslationPage googleBasicTranslationPage;
        GoogleAppsScriptTranslationPage googleAppsScriptTranslationPage;
        DeepLTranslationPage deeplTranslationPage;

        // OCR
        OcrPage ocrPage;
        TesseractOcrPage tesseractOcrPage;
        // AzureOcrPage azureOcrPage;
        // GoogleVisionOcrPage googleVisionOcrPage;

        // Shortcut
        ShortcutPage shortcutPage;

        public MainForm() {
            InitializeComponent();

            // Speech Recognition
            speechRecognitionPage = new SpeechRecognitionPage(serviceManager);
            chromeSpeechRecognitionPage = new ChromeSpeechRecognitionPage(serviceManager);
            azureSpeechRecognitionPage = new AzureSpeechRecognitionPage(serviceManager);
            amiVoiceSpeechRecognitionPage = new AmiVoiceSpeechRecognitionPage(serviceManager);

            // OBS
            obsPage = new ObsPage(serviceManager);
            subtitlesPage = new SubtitlesPage(serviceManager);

            // translation
            translationPage = new TranslationPage(serviceManager);
            azureTranslationPage = new AzureTranslationPage(serviceManager);
            googleBasicTranslationPage = new GoogleBasicTranslationPage(serviceManager);
            googleAppsScriptTranslationPage = new GoogleAppsScriptTranslationPage(serviceManager);
            deeplTranslationPage = new DeepLTranslationPage(serviceManager);

            // OCR
            ocrPage = new OcrPage(serviceManager);
            tesseractOcrPage = new TesseractOcrPage(serviceManager);
            // azureOcrPage = new AzureOcrPage(serviceManager);
            // googleVisionOcrPage = new GoogleVisionOcrPage(serviceManager);

            // Shortcut
            shortcutPage = new ShortcutPage(serviceManager);

            this.pages = new SettingPage[] {
                this.speechRecognitionPage,
                this.chromeSpeechRecognitionPage,
                this.azureSpeechRecognitionPage,
                this.amiVoiceSpeechRecognitionPage,
                this.obsPage,
                this.subtitlesPage,
                this.translationPage,
                this.azureTranslationPage,
                this.googleBasicTranslationPage,
                this.googleAppsScriptTranslationPage,
                this.deeplTranslationPage,
                this.ocrPage,
                this.tesseractOcrPage,
                // this.azureOcrPage,
                // this.googleVisionOcrPage,
                this.shortcutPage,
            };

            foreach (var page in this.pages) {
                this.panel1.Controls.Add(page);
            }

            this.serviceManager.Update();
            this.serviceManager.Init();
        }

        private void Form1_Load(object sender, EventArgs e) {
            this.menuView.ExpandAll();
            foreach (var page in this.pages) {
                page.Dock = DockStyle.Fill;
                page.LoadSettings();
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e) {
            foreach (var page in this.pages) {
                page.SaveSettings();
                page.Unload();
            }
            Properties.Settings.Default.Save();
        }

        private void menuView_AfterSelect(object sender, TreeViewEventArgs e) {
            if (e.Node == null)
                return;

            foreach (var page in this.pages) {
                if (e.Node.Name == page.Name) {
                    this.ShowChildPage(page);
                }
            }
        }

        private void ShowChildPage(UserControl view) {
            foreach (var page in this.pages) {
                if (view == page) {
                    page.Visible = true;
                } else {
                    page.Visible = false;
                }
            }
        }

        private void speechRecognitionCheckBox_CheckedChanged(object sender, EventArgs e) {
            this.SetCheckBoxButtonColor(this.speechRecognitionCheckBox);

            if (this.speechRecognitionCheckBox.Checked) {
                this.SpeechRecognitionStart();
            } else {
                this.SpeechRecognitionStop();
            }
        }

        /**
         * 音声認識を開始する
         */
        private void SpeechRecognitionStart() {
            var recognitionStarted = false;

            var commonService = this.serviceManager.GetService<SpeechRecognitionCommonService>();
            if (commonService == null || commonService.Device == null) {
                MessageBox.Show("マイクを設定してください。");
            } else {
                var service = this.serviceManager.GetActiveService<SpeechRecognitionService>();
                if (service != null) {
                    if (service.Start()) {
                        speechRecognitionService = service;
                        service.Recognizing += Recognizing;
                        service.Recognized += Recognized;
                        recognitionStarted = true;
                    }
                }
            }

            // 音声認識を開始できなかった場合、音声認識ボタンのチェックを外す。
            if (recognitionStarted == false) {
                this.speechRecognitionCheckBox.Checked = false;
            }
        }

        /**
         * 音声認識を終了する
         */
        private void SpeechRecognitionStop() {
            if (speechRecognitionService != null) {
                speechRecognitionService.Recognizing -= Recognizing;
                speechRecognitionService.Recognized -= Recognized;
                speechRecognitionService.Stop();
                speechRecognitionService = null;
            }
        }

        private void Recognizing(Object? sender, SpeechRecognitionEventArgs args) {
            this.SetRecognitionResultText(args.Text);
        }

        private void Recognized(Object? sender, SpeechRecognitionEventArgs args) {
            this.SetRecognitionResultText(args.Text);
        }

        async private void obsCheckBox_CheckedChanged(object sender, EventArgs e) {
            this.SetCheckBoxButtonColor(this.obsCheckBox);

            var service = serviceManager.GetService<ObsService>();
            if (service != null) {
                if (this.obsCheckBox.Checked) {
                    if (await service.ConnectAsync() == false) {
                        this.obsCheckBox.Checked = false;
                    }
                } else if (service.IsConnected) {
                    await service.DisconnectAsync();
                }
            }
        }

        private void Translate(string text) {
/*
            // 翻訳する
            TranslationResult? result = null;
            if (this.translationPage.Service == TranslationPage.ServiceType.Azure)
                result = await this.azureTranslationPage.TranslateAsync(text, this.azureTranslationPage.From, this.azureTranslationPage.To.ToArray());
            else if (this.translationPage.Service == TranslationPage.ServiceType.GoogleBasic)
                result = await this.googleBasicTranslationPage.TranslateAsync(text, this.googleBasicTranslationPage.From, this.googleBasicTranslationPage.To.ToArray());
            else if (this.translationPage.Service == TranslationPage.ServiceType.GoogleAppsScript)
                result = await this.googleAppsScriptTranslationPage.TranslateAsync(text, this.googleAppsScriptTranslationPage.From, this.googleAppsScriptTranslationPage.To.ToArray());
            else if (this.translationPage.Service == TranslationPage.ServiceType.DeepL)
                result = await this.deeplTranslationPage.TranslateAsync(text, this.deeplTranslationPage.From, this.deeplTranslationPage.To.ToArray());

            // 翻訳できなければ処理を終わる。
            // そうでなければ翻訳結果を画面に表示
            if (result == null)
                return;
            else if (result.Error == false) {
                foreach (var translation in result.Translations) {
                    if (translation.Text != null) {
                        this.AddRecognitionResultText(translation.Text);
                    }
                }
            }

            // 翻訳結果を取得
            string?[] texts = { null, null };
            int i = 0;
            if (this.translationPage.Service == TranslationPage.ServiceType.Azure)
                i = this.azureTranslationPage.IsTo1 ? 0 : 1;
            else if (this.translationPage.Service == TranslationPage.ServiceType.GoogleBasic)
                i = this.googleBasicTranslationPage.IsTo1 ? 0 : 1;
            else if (this.translationPage.Service == TranslationPage.ServiceType.GoogleAppsScript)
                i = this.googleAppsScriptTranslationPage.IsTo1 ? 0 : 1;
            else if (this.translationPage.Service == TranslationPage.ServiceType.DeepL)
                i = this.deeplTranslationPage.IsTo1 ? 0 : 1;

            for (var j = 0; j < result.Translations.Count && i < texts.Length; ++i, ++j) {
                texts[i] = result.Translations[j].Text;
            }

            // 翻訳結果をOBSに表示
            string[] target = { this.subtitlesPage.Translation1, this.subtitlesPage.Translation2 };
            for (var j = 0; j < texts.Length; j++) {
                string? str = texts[j];
                if (str != null) {
                    Debug.WriteLine("Translated: " + str);
                    this.obsPage.SetSubtitles(str, target[j], true, this.subtitlesPage.ClearInterval, this.subtitlesPage.AdditionalTime);
                }
            }
*/
        }

        /**
         * チェックボックスボタンの状態に合わせて色を変更する
         * （デフォルトの色は分かりにくいので）
         */
        private void SetCheckBoxButtonColor(CheckBox checkbox) {
            if (checkbox.Checked) {
                checkbox.BackColor = SystemColors.Highlight;
                checkbox.ForeColor = SystemColors.HighlightText;
            } else {
                checkbox.BackColor = SystemColors.ButtonHighlight;
                checkbox.ForeColor = SystemColors.ControlText;
            }
        }

        private void SetRecognitionResultText(string text) {
            if (this.recognitionResultTextBox.InvokeRequired) {
                Action act = delegate { this.recognitionResultTextBox.Text = text; };
                this.recognitionResultTextBox.Invoke(act);
            } else {
                this.recognitionResultTextBox.Text = text;
            }
        }

        private void AddRecognitionResultText(string text) {
            if (this.recognitionResultTextBox.InvokeRequired) {
                Action act = delegate { this.recognitionResultTextBox.Text += "\r\n" + text; };
                this.recognitionResultTextBox.Invoke(act);
            } else {
                this.recognitionResultTextBox.Text += "\r\n" + text;
            }
        }
    }
}