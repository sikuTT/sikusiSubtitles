using sikusiSubtitles.Shortcut;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace sikusiSubtitles.OCR {
    public class TesseractOcrService : OcrService {
        public TesseractOcrService(Service.ServiceManager serviceManager) : base(serviceManager, "Tesseract", "Tesseract", 100) {
        }

        public override void Execute(object obj, Bitmap bitmap) {
            var path = GetDataPath();
            if (path == null) {
                MessageBox.Show("言語データが取得できません。", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var tesseract = new TesseractEngine(path, "eng")) {
                try {
                    // BitmapをTesseract用に変換
                    var image = BitmapToImage(bitmap);

                    // OCRの実行
                    Tesseract.Page page = tesseract.Process(image);
                    Debug.WriteLine("TesseractOcrService: " + page.GetText());
                    this.InvokeOcrFinished(new OcrResult(obj, page.GetText()));
                } catch (Exception ex) {
                    Debug.WriteLine("TesseractOcrService.Execute: " + ex.Message);
                }
            }
        }

        private Pix BitmapToImage(Bitmap bitmap) {
            MemoryStream memStream = new MemoryStream();
            bitmap.Save(memStream, System.Drawing.Imaging.ImageFormat.Bmp);
            return Pix.LoadFromMemory(memStream.GetBuffer());
        }

        private string? GetDataPath() {
            var myAssembly = Assembly.GetEntryAssembly();
            var path = myAssembly?.Location;
            if (path == null)
                return null;

            int index = path.LastIndexOf('\\');
            if (index == -1)
                return null;

            path = path.Substring(0, index);
            path += "\\tessdata";
            return path;
        }
    }
}
