using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.OCR {
    public class OcrResult {
        public object Obj { get; }
        public string Text { get; }

        public OcrResult(object obj, string text) {
            this.Obj = obj;
            this.Text = text;
        }
    }
}
