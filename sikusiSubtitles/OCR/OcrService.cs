using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.OCR {
    public abstract class OcrService : sikusiSubtitles.Service {
        public event EventHandler<OcrResult>? OcrFinished;

        public OcrService(ServiceManager serviceManager, string name, string displayName, int index) : base(serviceManager, OcrServiceManager.ServiceName, name, displayName, index) {
        }

        public abstract List<Tuple<string, string>> GetLanguages();

        public abstract Task<string?> ExecuteAsync(Bitmap bitmap, string language);

        protected string ConcatString(string text1, string text2) {
            if (text1.Length > 0) {
                if (Char.IsLetterOrDigit(text1.Last())) {
                    text1 += ' ';
                }
            }
            text1 += text2;
            return text1;
        }

        protected string RemoveLineBreak(string text) {
            text = text.Replace("\r\n", " ").Replace("\r", " ").Replace("\n", " ");
            return text;
        }

        protected void InvokeOcrFinished(OcrResult result) {
            this.OcrFinished?.Invoke(this, result);
        }
    }
}