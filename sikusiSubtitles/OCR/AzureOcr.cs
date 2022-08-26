using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.OCR {
    public class AzureOcr : Ocr {
        override public Result Execute(Bitmap bitmap) {
            return new Result(0);
        }
    }
}
