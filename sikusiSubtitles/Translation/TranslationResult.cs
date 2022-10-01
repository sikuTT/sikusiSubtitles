using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.Translation {
    public class TranslationResult {
        public TranslationResult() {
            Translations = new List<Translation>();
            Error = false;
        }

        public string? DetectLanguage { get; set; }

        public List<Translation> Translations { get; set; }

        public bool Error { get; set; }

        public class Translation {
            public string Text { get; set; } = "";
            public string? Language { get; set; }
        }

    }
}
