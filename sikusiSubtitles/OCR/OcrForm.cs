using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static sikusiSubtitles.OCR.ScreenCapture;

namespace sikusiSubtitles.OCR {
    public partial class OcrForm : Form {
        private int processId;
        private Rectangle captureArea;

        public OcrForm(int processId, Rectangle captureArea) {
            InitializeComponent();

            this.processId = processId;
            this.captureArea = captureArea;
        }

        public void a() {
            Process process = Process.GetProcessById(processId);

            // 画面をキャプチャーする
            RECT rect;
            if (GetWindowRect(process.MainWindowHandle, out rect)) {
                var left = rect.left + ((Rectangle)captureArea).Left;
                var top = rect.top + ((Rectangle)captureArea).Top;
                var width = ((Rectangle)captureArea).Width;
                var height = ((Rectangle)captureArea).Height;

                Bitmap bitmap = new Bitmap(width, height);
                using (Graphics g = Graphics.FromImage(bitmap)) {
                    g.CopyFromScreen(left, top, 0, 0, new Size(width, height));
                    // Ocrs[0].Execute(bitmap);
                }
            }
        }

        private void ocrButton_Click(object sender, EventArgs e) {

        }

        private void translateButton_Click(object sender, EventArgs e) {

        }
    }
}
