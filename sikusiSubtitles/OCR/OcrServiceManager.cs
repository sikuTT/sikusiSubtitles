using Newtonsoft.Json.Linq;
using sikusiSubtitles.Shortcut;
using sikusiSubtitles.Translation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace sikusiSubtitles.OCR {
    public enum OcrArchives {
        [Description("使用しない")]
        None,

        [Description("Notion")]
        Notion,
    }

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
        public OcrArchives Archive { get; set; } = OcrArchives.None;
        public string NotionToken { get; set; } = "";
        public string NotionDatabaseId { get; set; } = "";
        public string NotionTitleSaveTarget { get; set; } = "";
        public string NotionTextSaveTarget { get; set; } = "";
        public string NotionTranslatedTextSaveTarget { get; set; } = "";
        public string NotionTranslationEngineSaveTarget { get; set; } = "";


        private Shortcut.Shortcut ocrShortcut = new Shortcut.Shortcut("execute-ocr", "OCR", "画面から文字を取得し翻訳する", "");
        private Shortcut.Shortcut clearObsTextShortcut = new Shortcut.Shortcut("clear-obs-text", "OCR", "OCRの翻訳結果をクリアする", "");

        public OcrServiceManager(ServiceManager serviceManager) : base(serviceManager, ServiceName, ServiceName, "OCR", 500, true) {
            settingsPage = new OcrPage(ServiceManager, this);
        }

        public override void Init() {
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
            var archive = token.Value<string?>("Archive");
            if (archive == null) {
                Archive = OcrArchives.None;
            } else {
                OcrArchives a;
                if (Enum.TryParse(archive , out a) == true) {
                    Archive = a;
                }
            }

            var notion = token.Value<JObject>("Notion");
            if (notion != null) {
                NotionToken = Decrypt(notion.Value<string>("Token") ?? "");
                NotionDatabaseId = notion.Value<string>("DatabaseId") ?? "";
                var save = notion.Value<JObject>("Save");
                if (save != null) {
                    NotionTitleSaveTarget = save.Value<string>("Title") ?? "";
                    NotionTextSaveTarget = save.Value<string>("Text") ?? "";
                    NotionTranslatedTextSaveTarget = save.Value<string>("TranslatedText") ?? "";
                    NotionTranslationEngineSaveTarget = save.Value<string>("TranslationEngine") ?? "";
                }
            }
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
                new JProperty("ClearObsTextShortcutKey", clearObsTextShortcut.ShortcutKey),
                new JProperty("Archive", Archive.ToString()),
                new JProperty("Notion", new JObject{
                    new JProperty("Token", Encrypt(NotionToken)),
                    new JProperty("DatabaseId", NotionDatabaseId),
                    new JProperty("Save", new JObject{
                        new JProperty("Title", NotionTitleSaveTarget),
                        new JProperty("Text", NotionTextSaveTarget),
                        new JProperty("TranslatedText", NotionTranslatedTextSaveTarget),
                        new JProperty("TranslationEngine", NotionTranslationEngineSaveTarget),
                    }),
                }),
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