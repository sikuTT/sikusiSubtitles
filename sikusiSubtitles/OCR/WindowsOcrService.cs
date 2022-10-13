using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using Windows.Graphics.Imaging;
using Windows.Media.Ocr;

namespace sikusiSubtitles.OCR {
    public class WindowsOcrService : OcrService{
        List<Language> languages;
        IReadOnlyList<Windows.Globalization.Language> availableLanguages;

        public WindowsOcrService(ServiceManager serviceManager) : base(serviceManager, "WIndowsOCR", "Windows標準", 100) {
            availableLanguages = OcrEngine.AvailableRecognizerLanguages;
            languages = availableLanguages.Select(lang => new Language(lang.LanguageTag, lang.DisplayName)).ToList();

            settingsPage = new WindowsOcrPage(ServiceManager, this);
        }

        public override async Task<OcrResult> ExecuteAsync(System.Drawing.Bitmap bitmap, string language) {
            OcrResult ocrResult = new OcrResult();

            var ocrLang = availableLanguages.Where(lang => lang.LanguageTag == language).FirstOrDefault();
            if (ocrLang != null) {
                var engine = OcrEngine.TryCreateFromLanguage(ocrLang);

                var softwareBitmap = await BitmapToSoftwareBitmap(bitmap);

                if (softwareBitmap != null) {
                    var result = await engine.RecognizeAsync(softwareBitmap);
                    if (result != null) {
                        ocrResult.Text = result.Text;
                    }
                }
            }
            return ocrResult;
        }

        public override List<Language> GetLanguages() {
            return languages;
        }

        private async Task<SoftwareBitmap> BitmapToSoftwareBitmap(System.Drawing.Bitmap bitmap) {
            var memStream = new MemoryStream();
            bitmap.Save(memStream, System.Drawing.Imaging.ImageFormat.Bmp);
            memStream.Position = 0;

            var decoder = await Windows.Graphics.Imaging.BitmapDecoder.CreateAsync(memStream.AsRandomAccessStream());
            var softwareBitmap = await decoder.GetSoftwareBitmapAsync(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied);

            return softwareBitmap;
        }
    }
}
