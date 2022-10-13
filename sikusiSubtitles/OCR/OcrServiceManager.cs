using Newtonsoft.Json.Linq;
using sikusiSubtitles.Shortcut;
using sikusiSubtitles.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace sikusiSubtitles.OCR {
    public class OcrServiceManager : Service {
        public static new string ServiceName = "OCR";

        public string OcrEngine { get; set; } = "";
        public string OcrLanguage { get; set; } = "";
        public string TranslationEngine { get; set; } = "";
        public string TranslationLanguage { get; set; } = "";
        public string OcrSpeechEngine { get; set; } = "";
        public string OcrSpeechVoice { get; set; } = "";
        public bool SpeechWhenOcrRun { get; set; } = false;
        public Shortcut.Shortcut OcrShortcut { get { return ocrShortcut; } }
        public Shortcut.Shortcut ClearObsTextShortcut { get { return clearObsTextShortcut; } }

        private Shortcut.Shortcut ocrShortcut = new Shortcut.Shortcut("execute-ocr", "OCR", "画面から文字を取得し翻訳する", "");
        private Shortcut.Shortcut clearObsTextShortcut = new Shortcut.Shortcut("clear-obs-text", "OCR", "OCRの翻訳結果をクリアする", "");

        public OcrServiceManager(ServiceManager serviceManager) : base(serviceManager, ServiceName, ServiceName, "OCR", 500, true) {
        }

        public override void Init() {
            settingsPage = new OcrPage(ServiceManager, this);
            var shortcutService = this.ServiceManager.GetService<ShortcutService>();
            if (shortcutService != null) {
                shortcutService.Shortcuts.Add(ocrShortcut);
                shortcutService.Shortcuts.Add(clearObsTextShortcut);
            }
        }

        public override void Load(JToken token) {

            OcrEngine = token.Value<string>("OcrEngine") ?? "";
            OcrLanguage = token.Value<string>("OcrLanguage") ?? "";
            TranslationEngine = token.Value<string>("TranslationEngine") ?? "";
            TranslationLanguage = token.Value<string>("TranslationLanguage") ?? "";
            OcrSpeechEngine = token.Value<string>("OcrSpeechEngine") ?? "";
            OcrSpeechVoice = token.Value<string>("OcrSpeechVoice") ?? "";
            SpeechWhenOcrRun = token.Value<bool>("SpeechWhenOcrRun");
            ocrShortcut.ShortcutKey = token.Value<string>("OcrShortcutKey") ?? "";
            clearObsTextShortcut.ShortcutKey = token.Value<string>("ClearObsTextShortcutKey") ?? "";
        }

        public override JObject Save() {
            return new JObject {
                new JProperty("OcrEngine", OcrEngine),
                new JProperty("OcrLanguage", OcrLanguage),
                new JProperty("TranslationEngine", TranslationEngine),
                new JProperty("TranslationLanguage", TranslationLanguage),
                new JProperty("OcrSpeechEngine", OcrSpeechEngine),
                new JProperty("OcrSpeechVoice", OcrSpeechVoice),
                new JProperty("SpeechWhenOcrRun", SpeechWhenOcrRun),
                new JProperty("OcrShortcutKey", ocrShortcut.ShortcutKey),
                new JProperty("ClearObsTextShortcutKey", clearObsTextShortcut.ShortcutKey)
            };
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