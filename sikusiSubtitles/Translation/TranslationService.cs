using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.Translation {
    public abstract class TranslationService : Service.Service {
        public static string SERVICE_NAME = "Translation";

        public event EventHandler<TranslationResult>? Translated;

        public TranslationService(string name, string displayName, int index) : base(SERVICE_NAME, name, displayName, index) {
        }

        public override bool Start() {
            return false;
        }

        public override void Stop() {
        }

        public abstract void Translate(string text);

        protected void InvokeTranslated(TranslationResult result) {
            this.Translated?.Invoke(this, result);
        }
    }
}
