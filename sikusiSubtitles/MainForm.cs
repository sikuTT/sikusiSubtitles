using sikusiSubtitles.OBS;
using sikusiSubtitles.OCR;
using sikusiSubtitles.Shortcut;
using sikusiSubtitles.Speech;
using sikusiSubtitles.SpeechRecognition;
using sikusiSubtitles.Translation;
using System.Diagnostics;

namespace sikusiSubtitles {
    public partial class MainForm : Form {
        ServiceManager serviceManager = new ServiceManager();
        SpeechRecognitionService? speechRecognitionService;

        public MainForm() {
            InitializeComponent();

            // OBS Service
            new ObsServiceManager(this.serviceManager);
            new ObsService(this.serviceManager);
            new SubtitlesService(this.serviceManager);

            // Speech Recognition Service
            new SpeechRecognitionServiceManager(this.serviceManager);
            new ChromeSpeechRecognitionService(this.serviceManager);
            new AzureSpeechRecognitionService(this.serviceManager);
            new AmiVoiceSpeechRecognitionServie(this.serviceManager);

            // Translation Service
            new TranslationServiceManager(this.serviceManager);
            new GoogleAppsScriptTranslationService(this.serviceManager);
            new GoogleBasicTranslationService(this.serviceManager);
            new AzureTranslationService(this.serviceManager);
            new DeepLTranslationService(this.serviceManager);

            // OCR Service
            new OcrServiceManager(this.serviceManager);
            new TesseractOcrService(this.serviceManager);
            new AzureOcrService(this.serviceManager);

            // Shortcut Service
            new ShortcutServiceManager(this.serviceManager);
            new ShortcutService(this.serviceManager);

            // Speecht Service
            new SpeechServiceManager(this.serviceManager);
            new SystemSpeechService(this.serviceManager);
            new VoiceVoxSpeechService(this.serviceManager);

            // Init all services
            this.serviceManager.Init();
        }

        private void Form1_Load(object sender, EventArgs e) {
            this.serviceManager.Managers.ForEach(service => {
                var page = service.GetSettingPage();
                // �T�[�r�X�ɐݒ�y�[�W�����݂���ꍇ�A�t�H�[���ɐݒ�y�[�W��ǉ�����
                // �c���[�r���[�ɐݒ�y�[�W��\�����郁�j���[���쐬
                var node = new TreeNode(service.DisplayName) { Name = service.ServiceName };
                this.menuView.Nodes.Add(node);

                if (page != null) {
                    // �ݒ�y�[�W���쐬����
                    page.Name = service.Name;
                    page.Dock = DockStyle.Fill;
                    this.splitContainer1.Panel2.Controls.Add(page);
                }
            });

            this.serviceManager.Services.ForEach(service => {
                var page = service.GetSettingPage();
                // �T�[�r�X�ɐݒ�y�[�W�����݂���ꍇ�A�t�H�[���ɐݒ�y�[�W��ǉ�����
                if (page != null) {
                    // �e�̃��j���[���擾
                    var parentNodes = menuView.Nodes.Find(service.ServiceName, false);
                    if (parentNodes.Length > 0 && parentNodes[0].Name != service.Name) {
                        var node = new TreeNode(service.DisplayName) { Name = service.Name };
                        parentNodes[0].Nodes.Add(node);
                    }

                    // �ݒ�y�[�W���쐬����
                    page.Name = service.Name;
                    page.Dock = DockStyle.Fill;
                    this.splitContainer1.Panel2.Controls.Add(page);
                }
            });

            this.menuView.ExpandAll();
            if (menuView.Nodes.Count > 0) menuView.SelectedNode = menuView.Nodes[0];
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e) {
            this.serviceManager.Finish();
            Properties.Settings.Default.Save();
        }

        private void menuView_AfterSelect(object sender, TreeViewEventArgs e) {
            if (e.Node == null)
                return;

            if (SelectNode(e.Node) == false) {
                if (e.Node.Nodes.Count > 0) {
                    SelectNode(e.Node.Nodes[0]);
                }
            }
        }

        private bool SelectNode(TreeNode node) {
            if (node != null) {
                var controls = this.splitContainer1.Panel2.Controls.Find(node.Name, false);
                var page = controls.Length > 0 ? controls[0] as UserControl : null;
                if (page != null) {
                    this.ShowChildPage(page);
                    return true;
                }
            }
            return false;
        }

        private void ShowChildPage(UserControl view) {
            foreach (var control in this.splitContainer1.Panel2.Controls) {
                var page = control as UserControl;
                if (page != null) {
                    page.Visible = view == control;
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

            var manager = this.serviceManager.GetManager<SpeechRecognitionServiceManager>();
            if (manager?.Device == null) {
                MessageBox.Show("�}�C�N��ݒ肵�Ă��������B");
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

            // �����F�����J�n�ł��Ȃ������ꍇ�A�����F���{�^���̃`�F�b�N���O���B
            if (recognitionStarted == false) {
                this.speechRecognitionCheckBox.Checked = false;
            }
        }

        /**
         * �����F�����I������
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