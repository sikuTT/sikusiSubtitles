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

        IntPtr windowHandle;
        System.Drawing.Rectangle captureArea;

        // 描画用ビットマップ
        DrawingGroup drawingGroup = new DrawingGroup();
        BitmapSource? originalBitmapSource;
        BitmapSource? adjustedBitmapSource;
        ImageDrawing? originalImageDrawing;
        GeometryDrawing? borderGeometryDrawing;

        // ドラッグ処理
        System.Windows.Point? dragStart;
        System.Windows.Point? dragEnd;

        public event EventHandler<System.Drawing.Rectangle>? AreaSelected;

        public CaptureWindow(IntPtr windowHandle, System.Drawing.Rectangle captureArea) {
            this.windowHandle = windowHandle;
            this.captureArea = captureArea;

            InitializeComponent();

            DrawingImage drawingImageSource = new DrawingImage(drawingGroup);
            image.Source = drawingImageSource;

            ScreenCapture();
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
            if (this.dragStart != null) {
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
                var originalBitmap = new Bitmap(width, height);
                using (var g = Graphics.FromImage(originalBitmap)) {
                    g.CopyFromScreen(rect.left, rect.top, 0, 0, originalBitmap.Size);
                }
                var hBitmap = originalBitmap.GetHbitmap();
                originalBitmapSource = Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                DeleteObject(hBitmap);

                // 選択前の状態のビットマップを作成（暗いビットマップ）
                var adjustedBitmap = AdjustBrightness(originalBitmap, 0.3f);
                hBitmap = adjustedBitmap.GetHbitmap();
                adjustedBitmapSource = Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                DeleteObject(hBitmap);

                // 描画する画像の一番下に暗い画像を配置する
                ImageDrawing adjustedImage = new ImageDrawing();
                adjustedImage.Rect = new Rect(0, 0, adjustedBitmap.Width, adjustedBitmap.Height);
                adjustedImage.ImageSource = adjustedBitmapSource;
                if (adjustedImage.CanFreeze) adjustedImage.Freeze();
                drawingGroup.Children.Add(adjustedImage);

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
            if (!captureArea.IsEmpty) {
                var w = captureArea.Width == 0 ? 1 : captureArea.Width;
                var h = captureArea.Height == 0 ? 1 : captureArea.Height;

                // 選択範囲にオリジナルの画像を表示
                if (originalImageDrawing == null) {
                    originalImageDrawing = new ImageDrawing();
                    drawingGroup.Children.Add(originalImageDrawing);
                }
                originalImageDrawing.Rect = new Rect(captureArea.Left, captureArea.Top, w, h);
                originalImageDrawing.ImageSource = new CroppedBitmap(originalBitmapSource, new Int32Rect(captureArea.Left, captureArea.Top, w, h));

                // 選択範囲に枠線を表示
                if (borderGeometryDrawing == null) {
                    var brush = System.Windows.Media.Brushes.Transparent;
                    var pen = new System.Windows.Media.Pen(System.Windows.Media.Brushes.LimeGreen, 4);
                    var rect = new RectangleGeometry();
                    borderGeometryDrawing = new GeometryDrawing(brush, pen, rect);
                    drawingGroup.Children.Add(borderGeometryDrawing);
                }
                var rectGeo = borderGeometryDrawing.Geometry as RectangleGeometry;
                if (rectGeo != null) {
                    rectGeo.Rect = new Rect(captureArea.Left, captureArea.Top, w, h);
                }
            }
        }
    }
}
