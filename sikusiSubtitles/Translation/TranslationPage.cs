using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sikusiSubtitles.Translation {
    public partial class TranslationPage : SettingPage {
        private List<TranslationService> services = new List<TranslationService>();

        public TranslationPage(Service.ServiceManager serviceManager) : base(serviceManager) {
            InitializeComponent();
        }

        public override void LoadSettings() {
            this.translationComboBox.SelectedIndex = 0;
            for (var i = 0; i < this.services.Count; ++i) {
                if (this.services[i].Name == Properties.Settings.Default.TtranslationService) {
                    this.translationComboBox.SelectedIndex = i + 1; // 先頭に「翻訳しない」があるので、サービスのindexは1からになる。
                    break;
                }
            }
        }

        public override void SaveSettings() {
            var service = this.serviceManager.GetActiveService<TranslationService>();
            if (service != null) {
                // 翻訳に使用するサービス名を保存する
                Properties.Settings.Default.TtranslationService = service.Name;
            } else {
                // 「翻訳しない」の場合は、使用するサービス名を空文字で保存する。
                Properties.Settings.Default.TtranslationService = "";
            }
        }

        private void TranslationPage_Load(object sender, EventArgs e) {
            this.services = this.serviceManager.GetServices<TranslationService>();
            foreach(var service in this.services) {
                this.translationComboBox.Items.Add(service.DisplayName);
            }
        }

        private void translationComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.translationComboBox.SelectedIndex > 0) {
                var service = this.services[this.translationComboBox.SelectedIndex - 1];
                this.serviceManager.SetActiveService(service);
            } else {
                this.serviceManager.ResetActiveService(TranslationService.SERVICE_NAME);
            }
        }
    }
}
