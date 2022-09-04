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
    public partial class CaptureForm : Form {
        private Bitmap? originalBitmap;
        private Bitmap? adjustBitmap;
        private Point? dragStart;
        private Point? dragEnd;

        public event EventHandler<Rectangle>? AreaSelected;

        public CaptureForm(IntPtr handle) {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            ScreenCapture(handle);
        }

        private void CaptureForm_Paint(object sender, PaintEventArgs e) {
            Graphics g = e.Graphics;
            if (adjustBitmap != null) {
                g.DrawImage(adjustBitmap, new Rectangle(0, 0, adjustBitmap.Width, adjustBitmap.Height), 0, 0, adjustBitmap.Width, adjustBitmap.Height, GraphicsUnit.Pixel);
                if (dragStart != null && dragEnd != null && originalBitmap != null) {
                    var x = ((Point)dragStart).X;
                    var y = ((Point)dragStart).Y;
                    var width = ((Point)dragEnd).X - ((Point)dragStart).X;
                    var height = ((Point)dragEnd).Y - ((Point)dragStart).Y;
                    g.DrawImage(originalBitmap, new Rectangle(x, y, width, height), x, y, width, height, GraphicsUnit.Pixel);
                }
            }
        }

        private void CaptureForm_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                this.dragStart = this.dragEnd = e.Location;
                Refresh();
            }
        }

        private void CaptureForm_MouseUp(object sender, MouseEventArgs e) {
            Refresh();

            if (this.dragStart != null && this.dragEnd != null) {
                var width = ((Point)dragEnd).X - ((Point)dragStart).X;
                var height = ((Point)dragEnd).Y - ((Point)dragStart).Y;
                Rectangle rect = new Rectangle(((Point)this.dragStart).X, ((Point)this.dragStart).Y, width, height);
                this.AreaSelected?.Invoke(this, rect);
            }
            this.dragStart = this.dragEnd = null;
            Close();
        }

        private void CaptureForm_MouseMove(object sender, MouseEventArgs e) {
            if (dragStart != null && dragEnd != null) {
                dragEnd = e.Location;
                Refresh();
            }
        }

        /**
         * 画面のキャプチャ
         */
        private void ScreenCapture(IntPtr handle) {
            RECT rect;
            if (GetWindowRect(handle, out rect)) {
                var width = rect.right - rect.left;
                var height = rect.bottom - rect.top;
                this.originalBitmap = new Bitmap(width, height);
                using (Graphics graphics = Graphics.FromImage(originalBitmap)) {
                    graphics.CopyFromScreen(rect.left, rect.top, 0, 0, originalBitmap.Size);
                }

                // 選択前の状態のビットマップを作成（暗いビットマップ）
                adjustBitmap = AdjustBrightness(originalBitmap, 0.3f);

                // キャプチャーするウィンドウの上に、キャプチャー画面を表示する。
                this.Location = new Point(rect.left, rect.top);
                this.Width = width;
                this.Height = height;
                this.Visible = true;
            }
        }

        /**
         * 画像の明るさを変更する
         */
        private Bitmap AdjustBrightness(Image img, float brightness) {
            Bitmap bmp = new Bitmap(img.Width, img.Height);
            using (Graphics g = Graphics.FromImage(bmp)) {
                System.Drawing.Imaging.ColorMatrix cm = new System.Drawing.Imaging.ColorMatrix(new float[][] {
                    new float[] {brightness, 0, 0, 0, 0},
                    new float[] {0, brightness, 0, 0, 0},
                    new float[] {0, 0, brightness, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {0, 0, 0, 0, 0}
                });

                System.Drawing.Imaging.ImageAttributes ia = new System.Drawing.Imaging.ImageAttributes();
                ia.SetColorMatrix(cm);
                g.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia);
            }
            return bmp;
        }
    }
}
