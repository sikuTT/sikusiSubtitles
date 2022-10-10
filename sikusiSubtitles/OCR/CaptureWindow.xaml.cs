﻿using System;
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

        IntPtr windowHandle;
        System.Drawing.Rectangle captureArea;

        // 描画用ビットマップ
        Bitmap? originalBitmap;
        Bitmap? adjustedBitmap;

        // ドラッグ処理
        System.Windows.Point? dragStart;
        System.Windows.Point? dragEnd;

        public event EventHandler<System.Drawing.Rectangle>? AreaSelected;

        public CaptureWindow(IntPtr windowHandle, System.Drawing.Rectangle captureArea) {
            InitializeComponent();

            this.windowHandle = windowHandle;
            this.captureArea = captureArea;

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
                var l = Math.Min((int)start.X, (int)end.X);
                var t = Math.Min((int)start.Y, (int)end.Y);
                var r = Math.Max((int)start.X, (int)end.X);
                var b = Math.Max((int)start.Y, (int)end.Y);
                captureArea = new System.Drawing.Rectangle(l, t, r - l, b - t);

                CreateBitmapSource();
            }
        }

        /**
         * 画面のキャプチャ
         */
        private void ScreenCapture() {
            RECT rect;
            if (GetWindowRect(windowHandle, out rect)) {
                var width = rect.right - rect.left;
                var height = rect.bottom - rect.top;

                // 調整前（オリジナル）の状態のビットマップを作成
                originalBitmap = new Bitmap(width, height);
                using var g = Graphics.FromImage(originalBitmap);
                g.CopyFromScreen(rect.left, rect.top, 0, 0, originalBitmap.Size);

                // 選択前の状態のビットマップを作成（暗いビットマップ）
                adjustedBitmap = AdjustBrightness(originalBitmap, 0.3f);

                CreateBitmapSource();

                // キャプチャーするウィンドウの上に、キャプチャー画面を表示する。
                this.Left = rect.left;
                this.Top = rect.top;
                this.Width = width;
                this.Height = height;
            }
        }

        /**
         * 画像の明るさを変更する
         */
        private Bitmap AdjustBrightness(System.Drawing.Image img, float brightness) {
            var bmp = new Bitmap(img.Width, img.Height);
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
            if (originalBitmap != null && adjustedBitmap != null) {
                using var bitmap = adjustedBitmap.Clone() as System.Drawing.Bitmap;
                if (bitmap != null) {
                    if (!captureArea.IsEmpty) {
                        var w = captureArea.Width == 0 ? 1 : captureArea.Width;
                        var h = captureArea.Height == 0 ? 1 : captureArea.Height;

                        // 前回のキャプチャからウィンドウサイズが小さくなった場合CaptureAreaがウィンドウ外に行ってしまう可能性があるので、ウィンドウ内に収まるように調整する
                        var x = (int)Math.Min(captureArea.Left, originalBitmap.Width - 2);
                        var y = (int)Math.Min(captureArea.Top, originalBitmap.Height - 2);
                        if (x + w >= originalBitmap.Width) w = (int)originalBitmap.Width - x;
                        if (y + h >= originalBitmap.Height) h = (int)originalBitmap.Height - y;

                        using var g = Graphics.FromImage(bitmap);

                        // 選択範囲を明るくする
                        g.DrawImage(originalBitmap, x, y, new Rectangle(x, y, w, h), GraphicsUnit.Pixel);

                        // 選択範囲に枠を付ける
                        var pen = new System.Drawing.Pen(System.Drawing.Color.LimeGreen, 3);
                        g.DrawRectangle(pen, x, y, w, h);
                    }

                    var hBitmap = bitmap.GetHbitmap();
                    image.Source = Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                    DeleteObject(hBitmap);
                }
            }
        }
    }
}