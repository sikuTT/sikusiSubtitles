using Newtonsoft.Json.Linq;
using sikusiSubtitles.Shortcut;
using sikusiSubtitles.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.OCR {
    class Properties {
        public string OcrEngine = "";
        public string OcrLanguage = "";
        public string TranslationEngine = "";
        public string TranslationLanguage = "";
        public string OcrShortcutKey = "";
        public string ClearObsTextKeyShortcutKey = "";
    }

    public class OcrServiceManager : Service{
        public static new string ServiceName = "OCR";

        public string OcrEngine { get; set; } = "";
        public string OcrLanguage { get; set; } = "";
        public string TranslationEngine { get; set; } = "";
        public string TranslationLanguage { get; set; } = "";

        private Shortcut.Shortcut ocrShortcut = new Shortcut.Shortcut("execute-ocr", "OCR", "画面から文字を取得し翻訳する", "");
        private Shortcut.Shortcut clearObsTextShortcut = new Shortcut.Shortcut("clear-obs-text", "OCR", "OBSに表示されているOCR結果をクリアする", "");

        public OcrServiceManager(ServiceManager serviceManager) : base(serviceManager, ServiceName, "OcrServiceManager", "OCR", 400, true) {
        }

        public override UserControl? GetSettingPage() {
            return new OcrPage(ServiceManager, this);
        }

        public override void Load(JToken token) {
            var props = token.ToObject<Properties>();
            if (props != null) {
                OcrEngine = props.OcrEngine;
                OcrLanguage = props.OcrLanguage;
                TranslationEngine = props.TranslationEngine;
                TranslationLanguage = props.TranslationLanguage;
                ocrShortcut.ShortcutKey = props.OcrShortcutKey;
                clearObsTextShortcut.ShortcutKey = props.ClearObsTextKeyShortcutKey;
            }
        }

        public override JObject Save() {
            return new JObject(Name, new Properties() {
                OcrEngine = OcrEngine,
                OcrLanguage = OcrLanguage,
                TranslationEngine = TranslationEngine,
                TranslationLanguage = TranslationLanguage,
                OcrShortcutKey = ocrShortcut.ShortcutKey,
                ClearObsTextKeyShortcutKey = clearObsTextShortcut.ShortcutKey,
            });
        }

        public override void Init() {
            var shortcutService = this.ServiceManager.GetService<ShortcutService>();
            if (shortcutService != null) {
                shortcutService.Shortcuts.Add(ocrShortcut);
                shortcutService.Shortcuts.Add(clearObsTextShortcut);
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
