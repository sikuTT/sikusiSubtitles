using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.OCR {
    public class AzureOcrService : OcrService {
        public AzureOcrService(Service.ServiceManager serviceManager) : base(serviceManager, "Azure", "Azure Cognitive Services", 300) {
        }

        public override void Execute(object obj, Bitmap bitmap) {
        }
    }
}
