using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.Translation {
    public abstract class TranslationService : Service.Service {
        public TranslationService(string name, string displayName, int index) : base("translation", name, displayName, index) {
        }

        public override bool Start() {
            return false;
        }

        public override void Stop() {
        }

        public abstract Task<TranslationResult> Translate(string text, string? from, string[] toList);
    }
}
