using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static sikusiSubtitles.OCR.ScreenCapture;

namespace sikusiSubtitles.OCR {
    /// <summary>
    /// CaptureWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class CaptureWindow : Window {
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        static extern bool DeleteObject(IntPtr hObject);

        [System.Runtime.InteropServices.DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory")]
        public static extern void CopyMemory(IntPtr dest, IntPtr source, int Length);

        // ターゲットのウィンドウ
        IntPtr targetProcess;
        IntPtr targetWindowHandle;
        double scale;

        // キャプチャーエリア
        System.Drawing.Rectangle captureArea;

        // 描画用ビットマップ
        WriteableBitmap? writeableBitmap;
        Bitmap? originalBitmap;
        Bitmap? adjustedBitmap;

        // ドラッグ処理
        System.Windows.Point? dragStart;
        System.Windows.Point? dragEnd;

        public event EventHandler<System.Drawing.Rectangle>? AreaSelected;

        public CaptureWindow(IntPtr targetProcess, IntPtr targetWindowHandle, System.Drawing.Rectangle captureArea) {
            InitializeComponent();

            this.targetProcess = targetProcess;
            this.targetWindowHandle = targetWindowHandle;
            this.captureArea = captureArea;

            // キャプチャー用のウィンドウをキャプチャー対象のウィンドウの上に移動する
            // ロード時にモニターのDPIを取得したいので、Loadedイベント前に移動する
            var interop = new WindowInteropHelper(this);
            interop.EnsureHandle();
            interop.Owner = targetWindowHandle;

            RECT rect;
            if (GetWindowRect(targetWindowHandle, out rect)) {
                Left = rect.left;
                Top = rect.top;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            ScreenCapture();
        }

        private void Window_Closed(object sender, EventArgs e) {
            if (originalBitmap != null) originalBitmap.Dispose();
            if (adjustedBitmap != null) adjustedBitmap.Dispose();
        }

        /** マウスのボタンが押された */
        private void Window_MouseDown(object sender, MouseButtonEventArgs e) {
            if (e.ChangedButton == MouseButton.Left) {
                dragStart = dragEnd = e.GetPosition(image);
                CreateBitmapSource();
            }
        }

        /** マウスのボタンが離された */
        private void Window_MouseUp(object sender, MouseButtonEventArgs e) {
            if (dragStart != null && dragEnd != null && dragStart != dragEnd) {
                AreaSelected?.Invoke(this, captureArea);
                this.dragStart = this.dragEnd = null;
            }
            Close();
        }

        /** マウスが移動された */
        private void Window_MouseMove(object sender, MouseEventArgs e) {
            if (this.dragStart != null) {
                dragEnd = e.GetPosition(image);

                var start = (System.Windows.Point)dragStart;
                var end = (System.Windows.Point)dragEnd;
                var l = (int)(Math.Min((int)start.X, (int)end.X) * scale);
                var t = (int)(Math.Min((int)start.Y, (int)end.Y) * scale);
                var r = (int)(Math.Max((int)start.X, (int)end.X) * scale);
                var b = (int)(Math.Max((int)start.Y, (int)end.Y) * scale);
                captureArea = new System.Drawing.Rectangle(l, t, r - l, b - t);

                CreateBitmapSource();
            }
        }

        /**
         * 画面のキャプチャ
         */
        private void ScreenCapture() {
            var helper = new System.Windows.Interop.WindowInteropHelper(this);
            uint dpi = GetDpiForWindow(helper.Handle);
            scale = dpi / 96.0;

            RECT rect;
            if (GetWindowRect(targetWindowHandle, out rect)) {
                var width = rect.right - rect.left;
                var height = rect.bottom - rect.top;

                // 調整前（オリジナル）の状態のビットマップを作成
                originalBitmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                using var g = Graphics.FromImage(originalBitmap);
                g.CopyFromScreen(rect.left, rect.top, 0, 0, originalBitmap.Size);

                // 選択前の状態のビットマップを作成（暗いビットマップ）
                adjustedBitmap = AdjustBrightness(originalBitmap, 0.3f);

                // イメージのソースになるWriteableBitmapを作成
                writeableBitmap = new WriteableBitmap(width, height, 96, 96, PixelFormats.Pbgra32, null);
                image.Source = writeableBitmap;

                CreateBitmapSource();

                // キャプチャーするウィンドウの上に、キャプチャー画面を表示する。
                this.Left = rect.left / scale;
                this.Top = rect.top / scale;
                this.Width = width / scale;
                this.Height = height / scale;
            }

            // SetThreadDpiAwarenessContext(old);
        }

        /**
         * 画像の明るさを変更する
         */
        private Bitmap AdjustBrightness(System.Drawing.Image img, float brightness) {
            var bmp = new Bitmap(img.Width, img.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            using (var g = Graphics.FromImage(bmp)) {
                var cm = new ColorMatrix(new float[][] {
                    new float[] {brightness, 0, 0, 0, 0},
                    new float[] {0, brightness, 0, 0, 0},
                    new float[] {0, 0, brightness, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {0, 0, 0, 0, 0}
                });

                var ia = new ImageAttributes();
                ia.SetColorMatrix(cm);
                g.DrawImage(img, new System.Drawing.Rectangle(0, 0, img.Width, img.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia);
            }
            return bmp;
        }

        private void CreateBitmapSource() {
            if (originalBitmap != null && adjustedBitmap != null && writeableBitmap != null) {
                using var bitmap = adjustedBitmap.Clone() as System.Drawing.Bitmap;
                if (bitmap != null) {
                    if (!captureArea.IsEmpty) {
                        using var g = Graphics.FromImage(bitmap);

                        // 選択範囲を明るくする
                        g.DrawImage(originalBitmap, captureArea.Left, captureArea.Top, captureArea, GraphicsUnit.Pixel);

                        // 選択範囲に枠を付ける
                        var pen = new System.Drawing.Pen(System.Drawing.Color.LimeGreen, 2);
                        g.DrawRectangle(pen, captureArea.Left - 1, captureArea.Top - 1, captureArea.Width + 2, captureArea.Height + 2);
                    }


                    BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                    try {
                        writeableBitmap.Lock();
                        try {
                            CopyMemory(writeableBitmap.BackBuffer, data.Scan0, (writeableBitmap.BackBufferStride * bitmap.Height));
                            writeableBitmap.AddDirtyRect(new Int32Rect(0, 0, bitmap.Width, bitmap.Height));
                        } finally {
                            writeableBitmap.Unlock();
                        }
                    } finally {
                        bitmap.UnlockBits(data);
                    }
                }
            }
        }
    }
}
