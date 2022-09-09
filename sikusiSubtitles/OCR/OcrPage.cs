using sikusiSubtitles.Translation;
using System.Diagnostics;

namespace sikusiSubtitles.OCR {
    public partial class OcrPage : SettingPage {
        private OcrServiceManager ocrManager;
        private List<OcrService> ocrServices = new List<OcrService>();
        private List<TranslationService> translationServices = new List<TranslationService>();

        private List<int> processIdList = new List<int>();
        private int selectedProcessId = 0;
        private Rectangle? captureArea;

        public OcrPage(Service.ServiceManager serviceManager) : base(serviceManager) {
            this.ocrManager = new OcrServiceManager(serviceManager);

            InitializeComponent();
        }

        /**
         * フォームロード
         */
        private void OcrPage_Load(object sender, EventArgs e) {
            // OCRサービス一覧をコンボボックスに設定
            this.ocrServices = serviceManager.GetServices<OcrService>();
            this.ocrServices.ForEach(service => {
                var i = this.ocrComboBox.Items.Add(service.DisplayName);
                if (service.Name == ocrManager.OcrEngine) ocrComboBox.SelectedIndex = i;
            });

            // 翻訳サービス一覧をコンボボックスに設定
            this.translationServices = serviceManager.GetServices<TranslationService>();
            this.translationServices.ForEach(service => {
                var i = this.translationEngineComboBox.Items.Add(service.DisplayName);
                if (service.Name == ocrManager.TranslationEngine) translationEngineComboBox.SelectedIndex = i;
            });

            // ウィンドウ一覧を更新する
            UpdateWindowList();
        }

        /**
         * OCRエンジンを選択
         */
        private void ocrComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            ocrLangComboBox.Items.Clear();

            var service = ocrManager.GetOcrEngine();
            if (service != null) {
                if (ocrComboBox.SelectedIndex != -1) {
                    ocrManager.OcrEngine = ocrServices[ocrComboBox.SelectedIndex].Name;
                } else {
                    ocrManager.OcrEngine = "";
                }

                // OCRの読み取り言語を、選択したOCRが対応している言語一覧で設定しなおす
                var langs = service.GetLanguages();
                langs.ForEach(lang => {
                    var i = this.ocrLangComboBox.Items.Add(lang.Item2);
                    if (lang.Item1 == ocrManager.OcrLanguage) ocrLangComboBox.SelectedIndex = i;
                });
            }
        }

        /** OCR読み取り言語を選択 */
        private void ocrLangComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (ocrLangComboBox.SelectedIndex != -1) {
                var service = ocrManager.GetOcrEngine();
                if (service != null) {
                    var langs = service.GetLanguages();
                    ocrManager.OcrLanguage = langs[ocrLangComboBox.SelectedIndex].Item1;
                }
            } else {
                ocrManager.OcrLanguage = "";
            }
        }

        /** 翻訳エンジンを選択 */
        private void translationEngineComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (translationEngineComboBox.SelectedIndex != -1) {
                ocrManager.TranslationEngine = translationServices[translationEngineComboBox.SelectedIndex].Name;
            } else {
                ocrManager.TranslationEngine = "";
            }

            // 翻訳先の言語を選択した翻訳エンジンの対応している言語一覧で設定しなおす
            translationLangComboBox.Items.Clear();
            var service = ocrManager.GetTranslationEngine();
            if (service != null) {
                var langs = service.GetLanguages();
                langs.ForEach(lang => {
                    var i = this.translationLangComboBox.Items.Add(lang.Item2);
                    if (lang.Item1 == ocrManager.TranslationLanguage) translationLangComboBox.SelectedIndex = i;
                });
            }
        }

        /** 翻訳先の言語を選択 */
        private void translationLangComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.translationLangComboBox.SelectedIndex != -1) {
                var service = ocrManager.GetTranslationEngine();
                if (service != null) {
                    var langs = service.GetLanguages();
                    ocrManager.TranslationLanguage = langs[translationLangComboBox.SelectedIndex].Item1;
                }
            } else {
                ocrManager.TranslationLanguage = "";
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
