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

        public bool IsManager { get; set; }

        public Service(ServiceManager serviceManager, string serviceName, string name, string displayName, int index, bool manager = false) {
            ServiceManager = serviceManager;
            ServiceName = serviceName;
            Name = name;
            DisplayName = displayName;
            Index = index;
            IsManager = manager;
            serviceManager.AddService(this);
        }

        public virtual void Save() {}
        public virtual void Load() {}
        public virtual void Init() {}
    }
}
