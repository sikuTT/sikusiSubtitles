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
        public string OcrLanguage { get; set; }
        public string TranslationEngine { get; set; }
        public string TranslationLanguage { get; set; }

        public TesseractOcrService(Service.ServiceManager serviceManager) : base(serviceManager, "Tesseract", "Tesseract", 100) {
            OcrLanguage = "";
            TranslationEngine = "";
            TranslationLanguage = "";
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

        public List<string> GetLanguageDatas() {
            List<string> langs = new List<string>();

            var path = GetDataPath();
            if (path != null) {
                var files = Directory.GetFiles(path, "*.traineddata");
                foreach (var file in files) {
                    var i = file.LastIndexOf('\\');
                    if (i != -1) {
                        var lang = file.Substring(i + 1);
                        lang = lang.Substring(0, lang.Length - 12);
                        langs.Add(lang);
                    }
                }
            }

            return langs;
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
