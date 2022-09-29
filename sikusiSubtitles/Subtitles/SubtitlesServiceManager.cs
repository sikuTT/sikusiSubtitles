using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.Subtitles {
    public class SubtitlesServiceManager : sikusiSubtitles.Service {
        public static new string ServiceName = "Subtitles";

        public SubtitlesServiceManager(ServiceManager serviceManager) : base(serviceManager, ServiceName, "SubtitlesServiceManager", "字幕", 200, true) {
        }
    }
}
