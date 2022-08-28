using sikusiSubtitles.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.Translation {
    public abstract class TranslationService : Service.Service {
        public static string SERVICE_NAME = "Translation";

        public event EventHandler<TranslationResult>? Translated;

        public TranslationService(ServiceManager serviceManager, string name, string displayName, int index) : base(serviceManager, SERVICE_NAME, name, displayName, index) {
        }

        public abstract void Translate(object obj, string text);
        public abstract void Translate(object obj, string text, string to);

        protected void InvokeTranslated(TranslationResult result) {
            this.Translated?.Invoke(this, result);
        }
    }
}
