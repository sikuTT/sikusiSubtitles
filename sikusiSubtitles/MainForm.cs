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
                page.Dock = DockStyle.Fill;
            }

            this.serviceManager.Init();
        }

        private void Form1_Load(object sender, EventArgs e) {
            this.menuView.ExpandAll();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e) {
            foreach (var page in this.pages) {
                page.Unload();
            }
            this.serviceManager.Save();
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

            var manager = this.serviceManager.GetManager<SpeechRecognitionServiceManager>();
            if (manager?.Device == null) {
                MessageBox.Show("マイクを設定してください。");
            } else {
                var service = manager.GetEngine();
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