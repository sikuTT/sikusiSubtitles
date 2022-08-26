using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.Service {
    public abstract class Service {
        public string ServiceName { get; set; }
        public string Name { get; set; }
        public int Index { get; set; }

        public string DisplayName { get; set; }

        public Service(string serviceName, string name, string displayName, int index) {
            ServiceName = serviceName;
            Name = name;
            DisplayName = displayName;
            Index = index;
        }

        public abstract bool Start();
        public abstract void Stop();
    }
}
