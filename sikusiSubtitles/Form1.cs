using sikusiSubtitles.OBS;
using sikusiSubtitles.OCR;
using sikusiSubtitles.SpeechRecognition;
using sikusiSubtitles.Translation;
using System.Diagnostics;

namespace sikusiSubtitles {
    public partial class Form1 : Form {
        Service.ServiceManager serviceManager = new Service.ServiceManager();
        SettingPage[] pages;

        // Speech Recognition
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
        AzureOcrPage azureOcrPage;
        GoogleVisionOcrPage googleVisionOcrPage;

        public Form1() {
            InitializeComponent();

            // Speech Recognition
            speechRecognitionPage = new SpeechRecognitionPage(serviceManager) { Name = "speechRecognitionPage" };
            chromeSpeechRecognitionPage = new ChromeSpeechRecognitionPage(serviceManager) { Name = "chromeSpeechRecognitionPage" };
            azureSpeechRecognitionPage = new AzureSpeechRecognitionPage() { Name = "azureSpeechRecognitionPage" };
            amiVoiceSpeechRecognitionPage = new AmiVoiceSpeechRecognitionPage() { Name = "amiVoiceSpeechRecognitionPage" };
            obsPage = new ObsPage() { Name = "obsPage" };
            subtitlesPage = new SubtitlesPage() { Name = "subtitlesPage" };

            // translation
            translationPage = new TranslationPage() { Name = "translationPage" };
            azureTranslationPage = new AzureTranslationPage(serviceManager) { Name = "azureTranslationPage" };
            googleBasicTranslationPage = new GoogleBasicTranslationPage() { Name = "googleBasicTranslationPage" };
            googleAppsScriptTranslationPage = new GoogleAppsScriptTranslationPage() { Name = "googleAppsScriptTranslationPage" };
            deeplTranslationPage = new DeepLTranslationPage() { Name = "deeplTranslationPage" };

            // OCR
            ocrPage = new OcrPage() { Name = "ocrPage" };
            tesseractOcrPage = new TesseractOcrPage() { Name = "tesseractOcrPage" };
            azureOcrPage = new AzureOcrPage() { Name = "azureOcrPage" };
            googleVisionOcrPage = new GoogleVisionOcrPage() { Name = "googleVisionOcrPage" };

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
                this.azureOcrPage,
                this.googleVisionOcrPage,
            };

            foreach (var page in this.pages) {
                this.panel1.Controls.Add(page);
            }

            this.serviceManager.UpdateChildServiceManagers();
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
         * �����F�����J�n����
         */
        private void SpeechRecognitionStart() {
            var recognitionStarted = false;
            if (this.speechRecognitionPage.Mic == null) {
                MessageBox.Show("�}�C�N��ݒ肵�Ă��������B");
            } else {
                var serviceManager = this.serviceManager.GetServiceManager(SpeechRecognitionServiceManager.ServiceName);
                if (serviceManager != null) {
                    var service = serviceManager.ActiveService as SpeechRecognitionService;
                    if (service != null) {
                        if (service.Start()) {
                            service.Recognizing += Recognizing;
                            service.Recognized += Recognized;
                            recognitionStarted = true;
                        }
                    }
                }
            }

            // �����F�����J�n�ł��Ȃ������ꍇ�A�����F���{�^���̃`�F�b�N���O���B
            if (recognitionStarted == false) {
                this.speechRecognitionCheckBox.Checked = false;
            }
        }

        /**
         * �����F�����I������
         */
        private void SpeechRecognitionStop() {
            var serviceManager = this.serviceManager.GetServiceManager(SpeechRecognitionServiceManager.ServiceName);
            if (serviceManager != null) {
                var service = serviceManager.ActiveService as SpeechRecognitionService;
                if (service != null) {
                    service.Recognizing -= Recognizing;
                    service.Recognized -= Recognized;
                    service.Stop();
                }
            }
        }

        private void Recognizing(Object? sender, SpeechRecognitionEventArgs args) {
            this.SetRecognitionResultText(args.Text);
        }

        private void Recognized(Object? sender, SpeechRecognitionEventArgs args) {
            this.SetRecognitionResultText(args.Text);
        }

        private void obsCheckBox_CheckedChanged(object sender, EventArgs e) {
            this.SetCheckBoxButtonColor(this.obsCheckBox);

            if (this.obsCheckBox.Checked) {
                if (this.obsPage.Connect() == false) {
                    this.obsCheckBox.Checked = false;
                }
            } else if (this.obsPage.IsConnected) {
                this.obsPage.Disconnect();
            }
        }

        private void Translate(string text) {
/*
            // �|�󂷂�
            TranslationResult? result = null;
            if (this.translationPage.Service == TranslationPage.ServiceType.Azure)
                result = await this.azureTranslationPage.TranslateAsync(text, this.azureTranslationPage.From, this.azureTranslationPage.To.ToArray());
            else if (this.translationPage.Service == TranslationPage.ServiceType.GoogleBasic)
                result = await this.googleBasicTranslationPage.TranslateAsync(text, this.googleBasicTranslationPage.From, this.googleBasicTranslationPage.To.ToArray());
            else if (this.translationPage.Service == TranslationPage.ServiceType.GoogleAppsScript)
                result = await this.googleAppsScriptTranslationPage.TranslateAsync(text, this.googleAppsScriptTranslationPage.From, this.googleAppsScriptTranslationPage.To.ToArray());
            else if (this.translationPage.Service == TranslationPage.ServiceType.DeepL)
                result = await this.deeplTranslationPage.TranslateAsync(text, this.deeplTranslationPage.From, this.deeplTranslationPage.To.ToArray());

            // �|��ł��Ȃ���Ώ������I���B
            // �����łȂ���Ζ|�󌋉ʂ���ʂɕ\��
            if (result == null)
                return;
            else if (result.Error == false) {
                foreach (var translation in result.Translations) {
                    if (translation.Text != null) {
                        this.AddRecognitionResultText(translation.Text);
                    }
                }
            }

            // �|�󌋉ʂ��擾
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

            // �|�󌋉ʂ�OBS�ɕ\��
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
         * �`�F�b�N�{�b�N�X�{�^���̏�Ԃɍ��킹�ĐF��ύX����
         * �i�f�t�H���g�̐F�͕�����ɂ����̂Łj
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