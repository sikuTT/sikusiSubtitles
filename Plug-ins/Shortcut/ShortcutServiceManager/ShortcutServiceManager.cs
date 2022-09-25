using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.Shortcut {
    public class ShortcutServiceManager : sikusiSubtitles.Service {
        public static new string ServiceName = "Shortcut";
        public ShortcutServiceManager(ServiceManager serviceManager) : base(serviceManager, ServiceName, "ShortcutServiceManager", "ショートカット", 1000, true) {
        }
    }
}
