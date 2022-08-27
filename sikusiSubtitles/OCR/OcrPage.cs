using System.Diagnostics;

namespace sikusiSubtitles.OCR {
    public partial class OcrPage : SettingPage {
        public OcrPage(Service.ServiceManager serviceManager) : base(serviceManager) {
            InitializeComponent();
        }

        public void SetOcrs(Ocr[] ocrs) {
            this.Ocrs = ocrs;
        }

        /**
         * フォームロード
         */
        private void OcrPage_Load(object sender, EventArgs e) {
            // ウィンドウ一覧を更新する
            UpdateWindowList();
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
                this.translateButton.Enabled = captureArea != null;
            } else {
                selectedProcessId = 0;
                this.setAreaButton.Enabled = false;
                this.translateButton.Enabled = false;
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
        private void translateButton_Click(object sender, EventArgs e) {
            if (captureArea != null) {
                OcrForm ocrForm = new OcrForm(selectedProcessId, (Rectangle)captureArea);
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
            this.translateButton.Enabled = true;
        }

        Ocr[] Ocrs = new Ocr[] {};
        List<int> processIdList = new List<int>();
        int selectedProcessId = 0;
        Rectangle? captureArea;
    }
}
