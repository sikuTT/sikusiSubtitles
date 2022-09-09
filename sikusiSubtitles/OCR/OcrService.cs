using sikusiSubtitles.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.OCR {
    public abstract class OcrService : Service.Service {
        public event EventHandler<OcrResult>? OcrFinished;

        public OcrService(ServiceManager serviceManager, string name, string displayName, int index) : base(serviceManager, OcrServiceManager.ServiceName, name, displayName, index) {
        }

        public abstract List<Tuple<string, string>> GetLanguages();

        public abstract Task<string?> ExecuteAsync(Bitmap bitmap, string language);

        protected void InvokeOcrFinished(OcrResult result) {
            this.OcrFinished?.Invoke(this, result);
        }
    }
}