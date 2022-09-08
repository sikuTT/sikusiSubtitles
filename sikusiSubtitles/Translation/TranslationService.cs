using sikusiSubtitles.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.Translation {
    public abstract class TranslationService : Service.Service {

        public event EventHandler<TranslationResult>? Translated;

        public TranslationService(ServiceManager serviceManager, string name, string displayName, int index) : base(serviceManager, TranslationServiceManager.ServiceName, name, displayName, index) {
        }

        public abstract Task<TranslationResult> TranslateAsync(string text, string? from, string to);
        public abstract Task<TranslationResult> TranslateAsync(string text, string? from, string[] toList);

        public abstract List<Tuple<string, string>> GetLanguages();

        protected void InvokeTranslated(TranslationResult result) {
            this.Translated?.Invoke(this, result);
        }
    }
}
