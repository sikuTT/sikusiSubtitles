using sikusiSubtitles.Shortcut;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace sikusiSubtitles.OCR {
    public class TesseractOcrService : OcrService {
        public TesseractOcrService(Service.ServiceManager serviceManager) : base(serviceManager, "Tesseract", "Tesseract", 100) {
        }

        public override void Execute(object obj, Bitmap bitmap) {
            using (var tesseract = new TesseractEngine(@"c:\tessdata-main", "eng")) {
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
    }
}
