using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.Service {
    public abstract class ServiceSettings {
        public abstract Service GetActiveService();
        public abstract List<Service> GetActiveServices();
    }
}
