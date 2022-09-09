using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.OCR {
    public class OcrResult {
        public string Text { get; }

        public OcrResult(string text) {
            this.Text = text;
        }
    }
}
