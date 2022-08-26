using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.Service {
    public class ServiceManager {
        public static readonly string ServiceName = "Root";
        public virtual string Name { get { return ServiceName; } }
        public List<Service> Services { get; set; }
        public Service? ActiveService { get; set; }

        private Dictionary<string, ServiceManager> ChildServiceManagers = new Dictionary<string, ServiceManager>();

        public ServiceManager() {
            this.ChildServiceManagers = new Dictionary<string, ServiceManager>();
            this.Services = new List<Service>();
        }

        public void AddService(Service service) {
            this.Services.Add(service);
        }

        public void AddServiceManager(ServiceManager serviceManager) {
            this.ChildServiceManagers[serviceManager.Name] = serviceManager;
        }

        public ServiceManager? GetServiceManager(string serviceName) {
            if (this.ChildServiceManagers.ContainsKey(serviceName)) {
                return this.ChildServiceManagers[serviceName];
            } else {
                return null;
            }
        }

        public void UpdateChildServiceManagers() {
            foreach (var service in this.Services) {
                if (this.ChildServiceManagers.ContainsKey(service.ServiceName)) {
                    this.ChildServiceManagers[service.ServiceName].AddService(service);
                }
            }
        }
    }
}
