using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.OCR {
    public class Result {
        public int code { get; set; }
        public string? text { get; set; }

        public Result(int code) {
            this.code = code;
        }

        public Result(int code, string text) {
            this.code = code;
            this.text = text;
        }
    }

    abstract public class Ocr {
        abstract public Result Execute(Bitmap bitmap);
    }
}
