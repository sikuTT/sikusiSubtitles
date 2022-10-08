using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles {
    public abstract class Service {
        protected ServiceManager ServiceManager;

        public event EventHandler<bool>? ServiceStoped;

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

        public virtual JObject? Save() { return null; }
        public virtual void Load(JToken token) { }
        public virtual void Init() { }
        public virtual void Finish() { }

        public virtual UserControl? GetSettingPage() { return null; }

        public void InvokeServiceStoped() {
            ServiceStoped?.Invoke(this, true);
        }

        /**
         * チェックボックスボタンの状態に合わせて色を変更する
         * （デフォルトの色は分かりにくいので）
         */
        protected void SetCheckBoxButtonColor(CheckBox checkbox) {
            if (checkbox.Checked) {
                checkbox.BackColor = SystemColors.Highlight;
                checkbox.ForeColor = SystemColors.HighlightText;
            } else {
                checkbox.BackColor = SystemColors.ButtonHighlight;
                checkbox.ForeColor = SystemColors.ControlText;
            }
        }

        protected string Encrypt(string text) {
            if (text == "")
                return "";
            else
                return Convert.ToBase64String(ProtectedData.Protect(Encoding.UTF8.GetBytes(text), entropy, DataProtectionScope.CurrentUser));
        }

        protected string Decrypt(string text) {
            if (text == "")
                return "";
            else
                return Encoding.UTF8.GetString(ProtectedData.Unprotect(Convert.FromBase64String(text), entropy, DataProtectionScope.CurrentUser));
        }

        private byte[] entropy = { 4, 9, 4, 9, 152, 61, 13, 213 };
    }
}
