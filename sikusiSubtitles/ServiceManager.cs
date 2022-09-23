using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles {
    public class ServiceManager {
        public List<Service> Managers { get; set; }
        public List<Service> Services { get; set; }

        private Dictionary<string, Service> activeServices = new Dictionary<string, Service>();

        public ServiceManager() {
            Managers = new List<Service>();
            Services = new List<Service>();
        }

        public void AddService(Service service) {
            if (service.IsManager) {
                Managers.Add(service);
            } else {
                Services.Add(service);
            }
        }

        public List<Type> GetServices<Type>() where Type : Service {
            var services = new List<Type>();
            foreach (var service in Services) {
                var type = service as Type;
                if (type != null) {
                    services.Add(type);
                }
            }
            return services;
        }

        public List<Type> GetServices<Type>(string serviceName) where Type : Service {
            var services = new List<Type>();
            foreach (var service in Services) {
                if (service.ServiceName == serviceName) {
                    var type = service as Type;
                    if (type != null) {
                        services.Add(type);
                    }
                }
            }
            return services;
        }

        public Type? GetService<Type>() where Type : Service {
            foreach (var service in Services) {
                var type = service as Type;
                if (type != null) {
                    return type;
                }
            }
            return null;
        }

        public Type? GetManager<Type>() where Type : Service {
            foreach (var service in Managers) {
                var type = service as Type;
                if (type != null) {
                    return type;
                }
            }
            return null;
        }

        /**
         * サービスの順番をindex順にする
         */
        public void Sort() {
            Managers.Sort((a, b) => {
                return a.Index - b.Index;
            });
            Services.Sort((a, b) => {
                return a.Index - b.Index;
            });
        }

        // 設定の読み込み
        public void Load() {
            foreach (var service in Managers) {
                service.Load();
            }
            foreach (var service in Services) {
                service.Load();
            }
        }

        // 設定の保存
        public void Save() {
            foreach (var service in Managers) {
                service.Save();
            }
            foreach (var service in Services) {
                service.Save();
            }
        }

        /**
         * すべてのサービスの作成後に、各サービスの初期化を呼び出す
         */
        public void Init() {
            Sort();
            Load();
            foreach (var service in Managers) {
                service.Init();
            }
            foreach (var service in Services) {
                service.Init();
            }
        }

        /**
         * すべてのサービスを終了する
         */
        public void Finish() {
            foreach (var service in Managers) {
                service.Finish();
            }
            foreach (var service in Services) {
                service.Finish();
            }
            Save();
        }
    }
}
