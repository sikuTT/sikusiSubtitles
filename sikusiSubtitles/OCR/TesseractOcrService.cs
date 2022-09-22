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
        public TesseractOcrService(ServiceManager serviceManager) : base(serviceManager, "Tesseract", "Tesseract", 100) {
            SettingPage = new TesseractOcrPage(serviceManager, this);
        }

        public override List<Tuple<string, string>> GetLanguages() {
            List<Tuple<string, string>> langs = new List<Tuple<string, string>>();

            try {
                var path = GetDataPath();
                if (path != null) {
                    var files = Directory.GetFiles(path, "*.traineddata");
                    foreach (var file in files) {
                        var i = file.LastIndexOf('\\');
                        if (i != -1) {
                            var lang = file.Substring(i + 1);
                            lang = lang.Substring(0, lang.Length - 12);
                            langs.Add(new Tuple<string, string>(lang, lang));
                        }
                    }
                }
            } catch (Exception) {
            }

            return langs;
        }

        public async override Task<OcrResult> ExecuteAsync(Bitmap bitmap, string language) {
            var path = GetDataPath();
            if (path == null) {
                MessageBox.Show("言語データが取得できません。", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new OcrResult() { Error = "言語データが取得できません。" };
            }

            try {
                using var tesseract = new TesseractEngine(path, language);

                // BitmapをTesseract用に変換
                var image = BitmapToImage(bitmap);

                // OCRの実行
                Page? page = null;
                await Task.Run(() => page = tesseract.Process(image));

                Debug.WriteLine("TesseractOcrService: " + page?.GetText());
                if (page != null) {
                    string text = RemoveLineBreak(page.GetText());
                    return new OcrResult() { Text = text };
                }
            } catch (Exception ex) {
                Debug.WriteLine("TesseractOcrService.Execute: " + ex.Message);
                return new OcrResult() { Error = ex.Message };
            }
            return new OcrResult() { Error = "文字が取得できません" };
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
