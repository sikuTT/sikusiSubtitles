using Newtonsoft.Json.Linq;
using sikusiSubtitles.Shortcut;
using sikusiSubtitles.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.OCR {
    public class OcrServiceManager : Service{
        public static new string ServiceName = "OCR";

        public string OcrEngine { get; set; } = "";
        public string OcrLanguage { get; set; } = "";
        public string TranslationEngine { get; set; } = "";
        public string TranslationLanguage { get; set; } = "";

        private Shortcut.Shortcut OcrShortcut = new Shortcut.Shortcut("execute-ocr", "OCR", "画面から文字を取得し翻訳する", "");
        private Shortcut.Shortcut ClearObsTextShortcut = new Shortcut.Shortcut("clear-obs-text", "OCR", "OCRの翻訳結果をクリアする", "");

        public OcrServiceManager(ServiceManager serviceManager) : base(serviceManager, ServiceName, ServiceName, "OCR", 400, true) {
            SettingPage = new OcrPage(serviceManager, this);
        }

        public override void Load(JToken token) {
            OcrEngine = token.Value<string>("OcrEngine") ?? "";
            OcrLanguage = token.Value<string>("OcrLanguage") ?? "";
            TranslationEngine = token.Value<string>("TranslationEngine") ?? "";
            TranslationLanguage = token.Value<string>("TranslationLanguage") ?? "";
            OcrShortcut.ShortcutKey = token.Value<string>("OcrShortcutKey") ?? "";
            ClearObsTextShortcut.ShortcutKey = token.Value<string>("ClearObsTextShortcutKey") ?? "";
        }

        public override JObject Save() {
            return new JObject {
                new JProperty("OcrEngine", OcrEngine),
                new JProperty("OcrLanguage", OcrLanguage),
                new JProperty("TranslationEngine", TranslationEngine),
                new JProperty("TranslationLanguage", TranslationLanguage),
                new JProperty("OcrShortcutKey", OcrShortcut.ShortcutKey),
                new JProperty("ClearObsTextShortcutKey", ClearObsTextShortcut.ShortcutKey)
            };
        }

        public override void Init() {
            var shortcutService = this.ServiceManager.GetService<ShortcutService>();
            if (shortcutService != null) {
                shortcutService.Shortcuts.Add(OcrShortcut);
                shortcutService.Shortcuts.Add(ClearObsTextShortcut);
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
