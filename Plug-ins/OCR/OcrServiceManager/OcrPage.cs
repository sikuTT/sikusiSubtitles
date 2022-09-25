using System.Diagnostics;

namespace sikusiSubtitles.OCR {
    public partial class OcrPage : UserControl {
        private ServiceManager serviceManager;
        private OcrServiceManager ocrManager;
        private List<int> processIdList = new List<int>();
        private int selectedProcessId = 0;

        public OcrPage(ServiceManager serviceManager, OcrServiceManager service) {
            this.serviceManager = serviceManager;
            this.ocrManager = service;

            InitializeComponent();
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
                ocrButton.Enabled = true;
            } else {
                selectedProcessId = 0;
                ocrButton.Enabled = false;
            }
        }

        /**
         * 翻訳ウィンドウを表示する
         */
        private void ocrButton_Click(object sender, EventArgs e) {
            OcrForm ocrForm = new OcrForm(this.serviceManager, this.ocrManager, selectedProcessId);
            ocrForm.Visible = true;
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
    }
}
