using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace sikusiSubtitles {
    public class ServiceManager {
        class OrderedControl {
            public Control Control { get; set; }
            public int Index { get; set; }

            public OrderedControl(Control control, int index) {
                this.Control = control;
                this.Index = index;
            }
        }

        public List<Service> Managers { get; set; }
        public List<Service> Services { get; set; }
        public List<Control> TopFlowControls {
            get {
                return this.topFlowControls.Select(oc => oc.Control).ToList();
            }
        }

        private string SaveFilePath;
        private List<OrderedControl> topFlowControls = new List<OrderedControl>();

        private Dictionary<string, Service> activeServices = new Dictionary<string, Service>();

        public ServiceManager() {
            Managers = new List<Service>();
            Services = new List<Service>();

            // Save file
            SaveFilePath = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\sikusiku\sikusiSubtitles\";
            Directory.CreateDirectory(SaveFilePath);
            SaveFilePath += @"settings.json";
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
            var manager = GetManager<Type>();
            if (manager != null) {
                return manager;
            } else {
                foreach (var service in Services) {
                    var type = service as Type;
                    if (type != null) {
                        return type;
                    }
                }
                return null;
            }
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
            Managers.Sort((a, b) => a.Index - b.Index);
            Services.Sort((a, b) => a.Index - b.Index);
            topFlowControls.Sort((a, b) => a.Index - b.Index);
        }

        // 設定の読み込み
        public JObject? Load() {
            try {
                var jsonObj = JObject.Parse(File.ReadAllText(SaveFilePath));
                var servicesObj = jsonObj.GetValue("Services")?.ToObject<JObject>();

                if (servicesObj != null) {
                    foreach (var obj in servicesObj) {
                        var manager = Managers.Find(service => service.Name == obj.Key);
                        if (manager != null && obj.Value != null) manager.Load(obj.Value);

                        var service = Services.Find(service => service.Name == obj.Key);
                        if (service != null && obj.Value != null) service.Load(obj.Value);
                    }
                }
                return jsonObj.GetValue("MainWindow")?.ToObject<JObject>();
            } catch (Exception ex) {
                Debug.WriteLine("ServiceManager: Load settings failed: " + ex.Message);
            }
            return null;
        }

        // 設定の保存
        public void Save(JObject mainWindowObj) {
            try {
                JObject saveObj = new JObject();
                JObject servicesObj = new JObject();
                saveObj.Add(new JProperty("Services", servicesObj));
                saveObj.Add(new JProperty("MainWindow", mainWindowObj));

                // Managerを保存
                foreach (var service in Managers) {
                    var obj = service.Save();
                    if (obj != null) servicesObj.Add(new JProperty(service.Name, obj));
                }

                // Servieを保存
                foreach (var service in Services) {
                    var obj = service.Save();
                    if (obj != null) servicesObj.Add(new JProperty(service.Name, obj));
                }
                File.WriteAllText(SaveFilePath, saveObj.ToString());
            } catch (Exception ex) {
                Debug.WriteLine("ServiceManager: Save settings failed: " + ex.Message);
            }
        }

        /**
         * すべてのサービスの作成後に、各サービスの初期化を呼び出す
         */
        public void Init() {
            Sort();
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
        }

        public void AddTopFlowControl(Control control, int index) {
            topFlowControls.Add(new OrderedControl(control, index));
        }
    }
}