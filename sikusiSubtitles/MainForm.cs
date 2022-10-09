using Newtonsoft.Json.Linq;
using sikusiSubtitles.OBS;
using sikusiSubtitles.OCR;
using sikusiSubtitles.Shortcut;
using sikusiSubtitles.Speech;
using sikusiSubtitles.SpeechRecognition;
using sikusiSubtitles.Subtitles;
using sikusiSubtitles.Translation;
using System.Diagnostics;

namespace sikusiSubtitles {
    public partial class MainForm : Form {
        ServiceManager serviceManager = new ServiceManager();

        public MainForm() {
            InitializeComponent();

            // Speech Recognition Service
            new SpeechRecognitionServiceManager(this.serviceManager);
            new ChromeSpeechRecognitionService(this.serviceManager);
            new AzureSpeechRecognitionService(this.serviceManager);
            new AmiVoiceSpeechRecognitionServie(this.serviceManager);

            // Subtitles Service
            new SubtitlesServiceManager(this.serviceManager);
            new SubtitlesService(this.serviceManager);

            // OBS Service
            new ObsServiceManager(this.serviceManager);
            new ObsService(this.serviceManager);
            new ObsSubtitlesService(this.serviceManager);

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
            var obj = this.serviceManager.Load();
            if (obj != null) {
                var width = obj.Value<int?>("Width");
                if (width != null) Width = (int)width;

                var height = obj.Value<int?>("Height");
                if (height != null) Height = (int)height;

                var menuPaneWidth = obj.Value<int?>("MenuPaneWidth");
                if (menuPaneWidth != null) splitContainer1.SplitterDistance = (int)menuPaneWidth;
            }
            this.serviceManager.Init();
        }

        private void Form1_Load(object sender, EventArgs e) {
            foreach (var control in serviceManager.TopFlowControls) {
                topLayoutPanel.Controls.Add(control);
            }

            this.serviceManager.Managers.ForEach(service => {
                var page = service.GetSettingPage();
                // サービスに設定ページが存在する場合、フォームに設定ページを追加する
                // ツリービューに設定ページを表示するメニューを作成
                var node = new TreeNode(service.DisplayName) { Name = service.ServiceName };
                this.menuView.Nodes.Add(node);

                if (page != null) {
                    // 設定ページを作成する
                    page.Name = service.Name;
                    page.Dock = DockStyle.Fill;
                    this.splitContainer1.Panel2.Controls.Add(page);
                }
            });

            this.serviceManager.Services.ForEach(service => {
                var page = service.GetSettingPage();
                // サービスに設定ページが存在する場合、フォームに設定ページを追加する
                if (page != null) {
                    // 親のメニューを取得
                    var parentNodes = menuView.Nodes.Find(service.ServiceName, false);
                    if (parentNodes.Length > 0 && parentNodes[0].Name != service.Name) {
                        var node = new TreeNode(service.DisplayName) { Name = service.Name };
                        parentNodes[0].Nodes.Add(node);
                    }

                    // 設定ページを作成する
                    page.Name = service.Name;
                    page.Dock = DockStyle.Fill;
                    this.splitContainer1.Panel2.Controls.Add(page);
                }
            });

            this.menuView.ExpandAll();
            if (menuView.Nodes.Count > 0) menuView.SelectedNode = menuView.Nodes[0];

            // 音声認識した内容をフォームに表示するために監視する
            var speechRecognitionServiceManager = serviceManager.GetService<SpeechRecognitionServiceManager>();
            if (speechRecognitionServiceManager != null) {
                speechRecognitionServiceManager.Recognized += SpeechRecognizedHandler;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e) {
            this.serviceManager.Finish();

            var obj = new JObject();
            obj.Add(new JProperty("Width", Width));
            obj.Add(new JProperty("Height", Height));
            obj.Add(new JProperty("MenuPaneWidth", splitContainer1.SplitterDistance));
            this.serviceManager.Save(obj);
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

        private void SpeechRecognizedHandler(object? sender, SpeechRecognitionEventArgs args) {
            if (this.recognitionResultTextBox.InvokeRequired) {
                Action act = delegate { this.recognitionResultTextBox.Text = args.Text; };
                this.recognitionResultTextBox.Invoke(act);
            } else {
                this.recognitionResultTextBox.Text = args.Text;
            }
        }
    }
}