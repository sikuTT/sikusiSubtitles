using sikusiSubtitles.Translation;
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
         * このページの表示状態が変わった
         *
         * このページが表示された場合、ウィンドウ一覧を更新する。
         * このページが表示されている間のみ、一定間隔でウィンドウ一覧を更新するタイマーを作成する。
         */
        private void OcrPage_VisibleChanged(object sender, EventArgs e) {
            if (Visible == true) {
                this.UpdateWindowList();
            }
            refreshTimer.Enabled = Visible;
        }

        /** 一定間隔でウィンドウ一覧を更新する */
        private void refreshTimer_Tick(object sender, EventArgs e) {
            this.UpdateWindowList();
        }

        /**
         * ウィンドウ一覧の更新
         */
        private void UpdateWindowList() {
            // ウィンドウ一覧をクリア
            this.windowListView.Items.Clear();
            this.processIdList = new List<int>();

            // プロセス一覧を取得する
            var processList = Process.GetProcesses();
            foreach (var process in processList) {
                if (process.Id != Process.GetCurrentProcess().Id && process.MainWindowTitle != "") {
                    // プロセスを追加1
                    this.processIdList.Add(process.Id);
                    // プロセスのメインウィンドウを一覧に追加
                    var item = new ListViewItem(new String[] { process.MainWindowTitle, process.ProcessName });
                    this.windowListView.Items.Add(item);

                    // プロセスが選択中のプロセスだった場合、再選択
                    if (process.Id == this.selectedProcessId) {
                        item.Selected = true;
                    }
                }
            }
        }
    }
}
