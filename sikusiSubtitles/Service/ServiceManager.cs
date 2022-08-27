using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.Service {
    public class ServiceManager {
        public List<Service> Services { get; set; }

        private Dictionary<string, Service> activeServices = new Dictionary<string, Service>();

        public ServiceManager() {
            this.Services = new List<Service>();
        }

        public void AddService(Service service) {
            this.Services.Add(service);
        }

        public List<Type> GetServices<Type>() where Type : Service {
            var services = new List<Type>();
            foreach (var service in this.Services) {
                var type = service as Type;
                if (type != null) {
                    services.Add(type);
                }
            }
            return services;
        }

        public List<Type> GetServices<Type>(string serviceName) where Type : Service {
            var services = new List<Type>();
            foreach (var service in this.Services) {
                if (service.ServiceName == serviceName) {
                    var type = service as Type;
                    if (type != null) {
                        services.Add(type);
                    }
                }
            }
            return services;
        }

        public List<Service> GetServices(string serviceName) {
            return GetServices<Service>(serviceName);
        }

        public Type? GetService<Type>() where Type : Service {
            foreach (var service in this.Services) {
                var type = service as Type;
                if (type != null) {
                    return type;
                }
            }
            return null;
        }

        public Type? GetService<Type>(string serviceName, string name) where Type : Service {
            foreach (var service in this.Services) {
                if (service.ServiceName == serviceName && service.Name == name) {
                    var type = service as Type;
                    if (type != null) {
                        return type;
                    }
                }
            }
            return null;
        }

        public Service? GetService(string serviceName, string name) {
            return GetService<Service>(serviceName, name);
        }

        public void SetActiveService(Service service) {
            this.activeServices[service.ServiceName] = service;
        }

        public void ResetActiveService(string serviceName) {
            this.activeServices.Remove(serviceName);
        }

        public Type? GetActiveService<Type>() where Type : Service {
            foreach(var service in this.activeServices.Values) {
                var type = service as Type;
                if (type != null) {
                    return type;
                }
            }
            return null;
        }

        public Service? GetActiveService(string serviceName) {
            if (this.activeServices.ContainsKey(serviceName)) {
                return this.activeServices[serviceName];
            } else {
                return null;
            }
        }
    }
}
