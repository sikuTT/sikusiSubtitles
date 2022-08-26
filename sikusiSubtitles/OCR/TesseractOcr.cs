using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.OCR {
    internal class TesseractOcr : Ocr {
        override public Result Execute(Bitmap bitmap) {
            MemoryStream memStream = new MemoryStream();
            bitmap.Save(memStream, ImageFormat.Bmp);
            var image = Tesseract.Pix.LoadFromMemory(memStream.GetBuffer());

            using (var tesseract = new Tesseract.TesseractEngine(@"c:\tessdata-main", "eng")) {
                // OCRの実行
                Tesseract.Page page = tesseract.Process(image);
                Debug.WriteLine("Ocr Text: " + page.GetText());
                return new Result(0, page.GetText());
            }
        }
    }
}
