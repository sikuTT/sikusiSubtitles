using sikusiSubtitles.Service;
using sikusiSubtitles.Shortcut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.OCR {
    public class OcrCommonService : Service.Service{
        public OcrCommonService(ServiceManager serviceManager) : base(serviceManager, OcrService.SERVICE_NAME, "OCR", "OCR", 500) {
        }

        public override void Init() {
            var shortcutService = this.ServiceManager.GetService<ShortcutService>();
            if (shortcutService != null) {
                shortcutService.Shortcuts.Add(new Shortcut.Shortcut("ExecuteOCR", "OCR", "画面から文字を取得し翻訳する", ""));
            }
        }
    }
}
