using ObsWebSocket5;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.OBS {
    public class ObsServiceManager : sikusiSubtitles.Service {
        public static new string ServiceName = "OBS";

        public ObsServiceManager(ServiceManager serviceManager) : base(serviceManager, ServiceName, ServiceName, "OBS", 100, true) {
        }
    }
}
