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
        public enum ServiceType {
            None,
            Azure,
            GoogleBasic,
            GoogleAppsScript,
            DeepL,
        }

        /**
         * 選択されている音声認識サービス
         */
        public ServiceType Service {
            get {
                if (this.azureTranslationRadioButton.Checked)
                    return ServiceType.Azure;
                else if (this.googleBasicTranslationRadioButton.Checked)
                    return ServiceType.GoogleBasic;
                else if (this.googleAppsScriptTranslationRadioButton.Checked)
                    return ServiceType.GoogleAppsScript;
                else if (this.deepLRadioButton.Checked)
                    return ServiceType.DeepL;
                else
                    return ServiceType.None;
            }
        }

        public TranslationPage() {
            InitializeComponent();
        }

        public override void LoadSettings() {
            this.noTranslationRadioButton.Checked = Properties.Settings.Default.TtranslationService == ServiceType.None.ToString();
            this.googleBasicTranslationRadioButton.Checked = Properties.Settings.Default.TtranslationService == ServiceType.GoogleBasic.ToString();
            this.googleAppsScriptTranslationRadioButton.Checked = Properties.Settings.Default.TtranslationService == ServiceType.GoogleAppsScript.ToString();
            this.azureTranslationRadioButton.Checked = Properties.Settings.Default.TtranslationService == ServiceType.Azure.ToString();
            this.deepLRadioButton.Checked = Properties.Settings.Default.TtranslationService == ServiceType.DeepL.ToString();
        }

        public override void SaveSettings() {
            Properties.Settings.Default.TtranslationService = this.Service.ToString();
        }
    }
}
