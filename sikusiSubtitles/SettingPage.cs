using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sikusiSubtitles {
    public partial class SettingPage : UserControl {
        public SettingPage() {
            InitializeComponent();
        }

        virtual public void SaveSettings() { }
        virtual public void LoadSettings() { }
        virtual public void Unload() { }


        byte[] entropy = { 4, 9, 4, 9, 152, 61, 13, 213 };

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
    }
}
