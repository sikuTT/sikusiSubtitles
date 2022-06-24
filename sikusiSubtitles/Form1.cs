using sikusiSubtitles.SpeechRecognition;
using sikusiSubtitles.Translation;
using System.Diagnostics;

namespace sikusiSubtitles {
    public partial class Form1 : Form {
        SettingPage[] pages;
        bool IsRecognitionWorking = false;

        public Form1() {
            InitializeComponent();
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
            };
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
            Debug.WriteLine("menuView_AfterSelect: " + (e.Node != null ? e.Node.Name : "null"));
            if (e.Node == null)
                return;

            foreach (var page in this.pages) {
                Debug.WriteLine(page.Name);
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
            } else if (this.IsRecognitionWorking) {
                this.SpeechRecognitionStop();
            }
        }

        /**
         * 音声認識を開始する
         */
        private void SpeechRecognitionStart() {
            if (this.speechRecognitionPage.Mic == null) {
                MessageBox.Show("マイクを設定してください。");
                return;
            }

            // 音声認識を開始する
            switch (this.speechRecognitionPage.Service) {
                case SpeechRecognitionPage.ServiceType.Chrome:
                    this.chromeSpeechRecognitionPage.Recognizing += Recognizing;
                    this.chromeSpeechRecognitionPage.Recognized += Recognized;
                    this.IsRecognitionWorking = this.chromeSpeechRecognitionPage.SpeechRecognitionStart();
                    break;
                case SpeechRecognitionPage.ServiceType.Azure:
                    this.azureSpeechRecognitionPage.Recognizing += Recognizing;
                    this.azureSpeechRecognitionPage.Recognized += Recognized;
                    this.IsRecognitionWorking = this.azureSpeechRecognitionPage.SpeechRecognitionStart(this.speechRecognitionPage.Mic);
                    break;
                case SpeechRecognitionPage.ServiceType.AmiVoice:
                    this.amiVoiceSpeechRecognitionPage.Recognizing += Recognizing;
                    this.amiVoiceSpeechRecognitionPage.Recognized += Recognized;
                    this.IsRecognitionWorking = this.amiVoiceSpeechRecognitionPage.SpeechRecognitionStart(this.speechRecognitionPage.Mic);
                    break;
            }

            // 音声認識を開始できなかった場合、音声認識ボタンのチェックを外す
            if (this.IsRecognitionWorking == false) {
                this.speechRecognitionCheckBox.Checked = false;
            }
        }

        /**
         * 音声認識を終了する
         */
        private void SpeechRecognitionStop() {
            this.IsRecognitionWorking = false;
            switch (this.speechRecognitionPage.Service) {
                case SpeechRecognitionPage.ServiceType.Chrome:
                    this.chromeSpeechRecognitionPage.Recognizing -= Recognizing;
                    this.chromeSpeechRecognitionPage.Recognized -= Recognized;
                    this.chromeSpeechRecognitionPage.SpeechRecognitionStop();
                    break;
                case SpeechRecognitionPage.ServiceType.Azure:
                    this.azureSpeechRecognitionPage.Recognizing -= Recognizing;
                    this.azureSpeechRecognitionPage.Recognized -= Recognized;
                    this.azureSpeechRecognitionPage.SpeechRecognitionStop();
                    break;
                case SpeechRecognitionPage.ServiceType.AmiVoice:
                    this.amiVoiceSpeechRecognitionPage.Recognizing -= Recognizing;
                    this.amiVoiceSpeechRecognitionPage.Recognized -= Recognized;
                    this.amiVoiceSpeechRecognitionPage.SpeechRecognitionStop();
                    break;
            }
        }

        private void Recognizing(Object? sender, SpeechRecognitionEventArgs args) {
            this.SetRecognitionResultText(args.Text);

            if (System.Threading.Thread.CurrentThread.ManagedThreadId == 1)
                Recognized(args.Text, false);
            else
                Invoke(delegate { Recognized(args.Text, false); });

            /*
            if (this.obsPage.IsConnected) {
                if (args.Text != "") {
                    this.obsPage.SetSubtitles(args.Text, this.subtitlesPage.Voice, false);
                }
            }
            */
        }

        private void Recognized(Object? sender, SpeechRecognitionEventArgs args) {
            this.SetRecognitionResultText(args.Text);

            if (System.Threading.Thread.CurrentThread.ManagedThreadId == 1)
                Recognized(args.Text, true);
            else
                Invoke(delegate { Recognized(args.Text, true); });

            /*
            if (this.obsCheckBox.Checked) {
                if (args.Text != "") {
                    this.obsPage.SetSubtitles(args.Text, this.subtitlesPage.Voice, true, this.SubtitlesClearInterval, this.SubtitlesAdditionalTime);
                    if (System.Threading.Thread.CurrentThread.ManagedThreadId == 1)
                        this.Translate(args.Text);
                    else
                        Invoke(delegate { this.Translate(args.Text); });
                }
            }
            */
        }

        private void Recognized(string text, bool recognized) {
            if (this.obsCheckBox.Checked && text != "") {
                this.obsPage.SetSubtitles(text, this.subtitlesPage.Voice, recognized, this.subtitlesPage.ClearInterval, this.subtitlesPage.AdditionalTime);
                if (recognized) {
                    this.Translate(text);
                }
            }
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

        private async void Translate(string text) {
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
        }

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