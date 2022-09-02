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

        public string OcrLanguage { get; set; }
        public TranslationService? TranslationService { get; set; }
        public string TranslationLanguage { get; set; }

        public OcrCommonService(ServiceManager serviceManager) : base(serviceManager, OcrService.SERVICE_NAME, "OCR", "OCR", 500) {
            OcrLanguage = "";
            TranslationLanguage = "";
        }

        public override void Init() {
            var shortcutService = this.ServiceManager.GetService<ShortcutService>();
            if (shortcutService != null) {
                shortcutService.Shortcuts.Add(new Shortcut.Shortcut("ExecuteOCR", "OCR", "画面から文字を取得し翻訳する", ""));
            }
        }
    }
}
