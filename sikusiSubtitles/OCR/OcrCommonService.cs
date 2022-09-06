using sikusiSubtitles.Service;
using sikusiSubtitles.Shortcut;
using sikusiSubtitles.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.OCR {
    public class OcrCommonService : Service.Service{
        public OcrService? OcrService {
            get { return ocrService; }
            set {
                this.ocrService = value;
                if (value != null) {
                    this.ServiceManager.SetActiveService(value);
                } else {
                    this.ServiceManager.ResetActiveService(OcrService.SERVICE_NAME);
                }
            }
        }
        OcrService? ocrService;

        public string OcrEngine { get; set; }
        public string OcrLanguage { get; set; }
        public TranslationService? TranslationService { get; set; }
        public string TranslationEngine { get; set; }
        public string TranslationLanguage { get; set; }

        public Shortcut.Shortcut OcrShortcut { get { return ocrShortcut; } }

        private Shortcut.Shortcut ocrShortcut = new Shortcut.Shortcut("ExecuteOCR", "OCR", "画面から文字を取得し翻訳する", "");

        public override void Load() {
            OcrEngine = Properties.Settings.Default.OcrEngine;
            OcrLanguage = Properties.Settings.Default.OcrLanguage;
            TranslationEngine = Properties.Settings.Default.OcrTranslationEngine;
            TranslationLanguage = Properties.Settings.Default.OcrTranslationLanguage;
            OcrShortcut.ShortcutKey = Properties.Settings.Default.OcrShortcutKey;
        }

        public override void Save() {
            Properties.Settings.Default.OcrEngine = OcrEngine;
            Properties.Settings.Default.OcrLanguage = OcrLanguage;
            Properties.Settings.Default.OcrTranslationEngine = TranslationEngine;
            Properties.Settings.Default.OcrTranslationLanguage = TranslationLanguage;
            Properties.Settings.Default.OcrShortcutKey = OcrShortcut.ShortcutKey;
        }

        public OcrCommonService(ServiceManager serviceManager) : base(serviceManager, OcrService.SERVICE_NAME, "OCR", "OCR", 500) {
            OcrEngine = "";
            OcrLanguage = "";
            TranslationEngine = "";
            TranslationLanguage = "";
        }

        public override void Init() {
            var shortcutService = this.ServiceManager.GetService<ShortcutService>();
            if (shortcutService != null) {
                shortcutService.Shortcuts.Add(ocrShortcut);
            }
        }
    }
}
