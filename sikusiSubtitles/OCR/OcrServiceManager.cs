using sikusiSubtitles.Shortcut;
using sikusiSubtitles.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.OCR {
    public class OcrServiceManager : sikusiSubtitles.Service{
        public static new string ServiceName = "OCR";

        public string OcrEngine { get; set; } = "";
        public string OcrLanguage { get; set; } = "";
        public string TranslationEngine { get; set; } = "";
        public string TranslationLanguage { get; set; } = "";

        private Shortcut.Shortcut ocrShortcut = new Shortcut.Shortcut("execute-ocr", "OCR", "画面から文字を取得し翻訳する", "");
        private Shortcut.Shortcut clearOcrTranslatedTextShortcut = new Shortcut.Shortcut("clear-ocr-translated-text", "OCR", "OCRの翻訳結果をクリアする", "");

        public OcrServiceManager(ServiceManager serviceManager) : base(serviceManager, ServiceName, ServiceName, "OCR", 400, true) {
            SettingPage = new OcrPage(serviceManager, this);
        }

        public override void Load() {
            OcrEngine = Properties.Settings.Default.OcrEngine;
            OcrLanguage = Properties.Settings.Default.OcrLanguage;
            TranslationEngine = Properties.Settings.Default.OcrTranslationEngine;
            TranslationLanguage = Properties.Settings.Default.OcrTranslationLanguage;
            ocrShortcut.ShortcutKey = Properties.Settings.Default.OcrShortcutKey;
            clearOcrTranslatedTextShortcut.ShortcutKey = Properties.Settings.Default.ClearOcrTraslatedTextShortcutKey;
        }

        public override void Save() {
            Properties.Settings.Default.OcrEngine = OcrEngine;
            Properties.Settings.Default.OcrLanguage = OcrLanguage;
            Properties.Settings.Default.OcrTranslationEngine = TranslationEngine;
            Properties.Settings.Default.OcrTranslationLanguage = TranslationLanguage;
            Properties.Settings.Default.OcrShortcutKey = ocrShortcut.ShortcutKey;
            Properties.Settings.Default.ClearOcrTraslatedTextShortcutKey = clearOcrTranslatedTextShortcut.ShortcutKey;
        }

        public override void Init() {
            var shortcutService = this.ServiceManager.GetService<ShortcutService>();
            if (shortcutService != null) {
                shortcutService.Shortcuts.Add(ocrShortcut);
                shortcutService.Shortcuts.Add(clearOcrTranslatedTextShortcut);
            }
        }

        public OcrService? GetOcrEngine() {
            var services = this.ServiceManager.GetServices<OcrService>();
            return services.Where(service => service.Name == OcrEngine).FirstOrDefault();
        }

        public TranslationService? GetTranslationEngine() {
            var services = this.ServiceManager.GetServices<TranslationService>();
            return services.Where(service => service.Name == TranslationEngine).FirstOrDefault();
        }
    }
}
