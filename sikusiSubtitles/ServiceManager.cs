using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles {
    public class ServiceManager {
        public List<Service> Managers { get; set; }
        public List<Service> Services { get; set; }

        private string SaveFilePath;

        private Dictionary<string, Service> activeServices = new Dictionary<string, Service>();

        public ServiceManager() {
            Managers = new List<Service>();
            Services = new List<Service>();

            // Save file
            SaveFilePath = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            SaveFilePath += @"\sikusiku\sikusiSubtitles\settings.json";
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
            try {
                JObject jobj = JObject.Parse(File.ReadAllText(SaveFilePath));
                foreach (var obj in jobj) {
                    var manager = Managers.Find(service => service.Name == obj.Key);
                    if (manager != null && obj.Value != null) manager.Load(obj.Value);

                    var service = Services.Find(service => service.Name == obj.Key);
                    if (service != null && obj.Value != null) service.Load(obj.Value);
                }
            } catch (Exception ex) {
                Debug.WriteLine("ServiceManager: Load settings failed: " + ex.Message);
            }
        }

        // 設定の保存
        public void Save() {
            JObject saveObj = new JObject();
            foreach (var service in Managers) {
                var obj = service.Save();
                if (obj != null) saveObj.Add(new JProperty(service.Name, obj));
            }
            foreach (var service in Services) {
                var obj = service.Save();
                if (obj != null) saveObj.Add(new JProperty(service.Name, obj));
            }
            File.WriteAllText(SaveFilePath, saveObj.ToString());
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
