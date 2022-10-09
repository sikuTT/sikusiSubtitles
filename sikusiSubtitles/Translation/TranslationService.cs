using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.Translation {
    public abstract class TranslationService : sikusiSubtitles.Service {
        public TranslationService(ServiceManager serviceManager, string name, string displayName, int index) : base(serviceManager, TranslationServiceManager.ServiceName, name, displayName, index) {
        }

        public abstract Task<TranslationResult> TranslateAsync(string text, string? from, string to);
        public abstract Task<TranslationResult> TranslateAsync(string text, string? from, string[] toList);

        public abstract List<Language> GetLanguages();
    }
}
