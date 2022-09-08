using sikusiSubtitles.Translation;
using System.Diagnostics;

namespace sikusiSubtitles.OCR {
    public partial class OcrPage : SettingPage {
        private OcrServiceManager service;
        private List<OcrService> ocrServices = new List<OcrService>();
        private List<TranslationService> translationServices = new List<TranslationService>();

        private List<int> processIdList = new List<int>();
        private int selectedProcessId = 0;
        private Rectangle? captureArea;

        public OcrPage(Service.ServiceManager serviceManager) : base(serviceManager) {
            this.service = new OcrServiceManager(serviceManager);

            InitializeComponent();
        }

        /**
         * フォームロード
         */
        private void OcrPage_Load(object sender, EventArgs e) {
            // OCRサービス一覧をコンボボックスに設定
            this.ocrServices = this.serviceManager.GetServices<OcrService>();
            this.ocrServices.ForEach(service => this.ocrComboBox.Items.Add(service.DisplayName));

            // 翻訳サービス一覧をコンボボックスに設定
            this.translationServices = this.serviceManager.GetServices<TranslationService>();
            this.translationServices.ForEach(service => this.translationEngineComboBox.Items.Add(service.DisplayName));

            // ウィンドウ一覧を更新する
            UpdateWindowList();
        }

        /**
         * OCRエンジンを選択
         */
        private void ocrComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (ocrComboBox.SelectedIndex != -1) {
                service.OcrEngine = this.ocrServices[this.ocrComboBox.SelectedIndex].Name;
            } else {
                service.OcrEngine = "";
            }

            // OCRの読み取り言語を、選択したOCRが対応している言語一覧で設定しなおす
            ocrLangComboBox.Items.Clear();
            var ocrService = service.GetEngine();
            if (ocrService != null) {
                var langs = ocrService.GetLanguages();
                langs.ForEach(lang => this.ocrLangComboBox.Items.Add(lang));
                ocrLangComboBox.SelectedIndex = langs.FindIndex(lang => lang == service.OcrLanguage);
            }
        }

        /** OCR読み取り言語を選択 */
        private void ocrLangComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            var ocrService = service.GetEngine();
            if (ocrService != null) {
                if (this.ocrLangComboBox.SelectedIndex != -1) {
                    var langs = ocrService.GetLanguages();
                    service.OcrLanguage = langs[ocrLangComboBox.SelectedIndex];
                } else {
                    service.OcrLanguage = "";
                }
            }
        }

        /** 翻訳エンジンを選択 */
        private void translationEngineComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.translationEngineComboBox.SelectedIndex != -1) {
                service.TranslationService = this.translationServices[this.translationEngineComboBox.SelectedIndex];
            } else {
                service.TranslationService = null;
            }

            // 翻訳先の言語を選択した翻訳エンジンの対応している言語一覧で設定しなおす
            this.translationLangComboBox.Items.Clear();
            if (service.TranslationService != null) {
                var langs = service.TranslationService.GetLanguages();
                langs.ForEach(lang => this.translationLangComboBox.Items.Add(lang.Item2));
                this.translationLangComboBox.SelectedIndex = langs.FindIndex(lang => lang.Item1 == service.TranslationLanguage);
            }
        }

        /** 翻訳先の言語を選択 */
        private void translationLangComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (service.TranslationService != null) {
                if (this.translationLangComboBox.SelectedIndex != -1) {
                    var langs = service.TranslationService.GetLanguages();
                    service.TranslationLanguage = langs[translationLangComboBox.SelectedIndex].Item1;
                } else {
                    service.TranslationLanguage = "";
                }
            }
        }

        /**
         * ウィンドウ一覧の更新ボタンをクリック
         */
        private void updateWidowListButton_Click(object sender, EventArgs e) {
            // ウィンドウ一覧を更新する
            UpdateWindowList();
        }

        /**
         * ウィンドウ一覧からウィンドウが選択された
         */
        private void windowListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e) {
            if (e.IsSelected == true) {
                selectedProcessId = this.processIdList[e.ItemIndex];
                this.setAreaButton.Enabled = true;
                this.ocrButton.Enabled = captureArea != null;
            } else {
                selectedProcessId = 0;
                this.setAreaButton.Enabled = false;
                this.ocrButton.Enabled = false;
            }
            this.captureArea = null;
        }

        /**
         * キャプチャーエリアの選択ボタンをクリック
         */
        private void setAreaButton_Click(object sender, EventArgs e) {
            // ウィンドウ一覧が選択されている場合、ウィンドウをキャプチャーする
            if (selectedProcessId > 0) {
                Microsoft.VisualBasic.Interaction.AppActivate(selectedProcessId);
                Process process = Process.GetProcessById(selectedProcessId);
                ScreenCapture(process.MainWindowHandle);
            }
        }

        /**
         * 翻訳ウィンドウを表示する
         */
        private void ocrButton_Click(object sender, EventArgs e) {
            if (captureArea != null) {
                OcrForm ocrForm = new OcrForm(this.serviceManager, selectedProcessId, (Rectangle)captureArea);
                ocrForm.Visible = true;
            }
        }

        /**
         * ウィンドウ一覧の更新
         */
        private void UpdateWindowList() {
            this.windowListView.Items.Clear();
            var processList = Process.GetProcesses();
            this.processIdList = new List<int>();
            foreach (var process in processList) {
                if (process.Id != Process.GetCurrentProcess().Id && process.MainWindowTitle != "") {
                    this.processIdList.Add(process.Id);
                    var item = new ListViewItem(new String[] { process.MainWindowTitle, process.ProcessName });
                    this.windowListView.Items.Add(item);
                }
            }
        }

        /**
         * 画面のキャプチャ
         */
        private void ScreenCapture(IntPtr handle) {
            // キャプチャーするウィンドウの上に、キャプチャー画面を表示する。
            CaptureForm captureForm = new CaptureForm(handle);
            captureForm.AreaSelected += AreaSelected;
            captureForm.FormClosed += (object? sender, FormClosedEventArgs e) => {
                captureForm.AreaSelected -= AreaSelected;
            };
        }

        /**
         * キャプチャー画面で範囲が選択された
         */
        private void AreaSelected(Object? sender, Rectangle rect) {
            captureArea = rect;
            this.ocrButton.Enabled = true;
        }
    }
}
