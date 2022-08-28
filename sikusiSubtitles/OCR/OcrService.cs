using sikusiSubtitles.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.OCR {
    public abstract class OcrService : Service.Service {
        public static string SERVICE_NAME = "OCR";

        public event EventHandler<OcrResult>? OcrFinished;

        public OcrService(ServiceManager serviceManager, string name, string displayName, int index) : base(serviceManager, SERVICE_NAME, name, displayName, index) {
        }

        public abstract void Execute(object obj, Bitmap bitmap);

        protected void InvokeOcrFinished(OcrResult result) {
            this.OcrFinished?.Invoke(this, result);
        }
    }
}