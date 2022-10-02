using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.Translation {
    public class TranslationServiceManager : sikusiSubtitles.Service {
        public static new string ServiceName = "Translation";

        public TranslationServiceManager(ServiceManager serviceManager) : base(serviceManager, ServiceName, ServiceName, "翻訳", 300, true) {
        }
    }
}
