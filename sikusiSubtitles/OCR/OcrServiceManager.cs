using sikusiSubtitles.Service;
using sikusiSubtitles.Shortcut;
using sikusiSubtitles.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.OCR {
    public class OcrServiceManager : Service.Service{
        public static new string ServiceName = "OCR";

        public string OcrEngine { get; set; } = "";
        public string OcrLanguage { get; set; } = "";
        public TranslationService? TranslationService { get; set; }
        public string TranslationEngine { get; set; } = "";
        public string TranslationLanguage { get; set; } = "";

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

        public OcrServiceManager(ServiceManager serviceManager) : base(serviceManager, ServiceName, "OCR", "OCR", 500) {
        }

        public override void Init() {
            var shortcutService = this.ServiceManager.GetService<ShortcutService>();
            if (shortcutService != null) {
                shortcutService.Shortcuts.Add(ocrShortcut);
            }
        }

        public OcrService GetEngine() {
            var services =  this.ServiceManager.GetServices<OcrService>();
            return services.Where(service => service.Name == OcrEngine).First();
        }
    }
}
