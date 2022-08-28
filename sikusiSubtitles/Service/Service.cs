using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.Service {
    public abstract class Service {
        protected ServiceManager ServiceManager;
        public string ServiceName { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public int Index { get; set; }

        public Service(ServiceManager serviceManager, string serviceName, string name, string displayName, int index) {
            ServiceManager = serviceManager;
            ServiceName = serviceName;
            Name = name;
            DisplayName = displayName;
            Index = index;
            serviceManager.AddService(this);
        }

        public virtual void Init() {}
    }
}
