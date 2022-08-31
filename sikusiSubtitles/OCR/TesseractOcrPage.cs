using sikusiSubtitles.Translation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sikusiSubtitles.OCR {
    public partial class TesseractOcrPage : SettingPage {
        TesseractOcrService service;
        List<TranslationService> translationServices = new List<TranslationService>();
        List<string> ocrLanguages = new List<string>();
        Tuple<string, string>[] translationLanguages = new Tuple<string, string>[] { };

        public override void LoadSettings() {
            // 読み取り言語の初期設定
            var i = this.ocrLanguages.FindIndex(lang => lang == Properties.Settings.Default.TesseractOcrLanguage);
            this.ocrLangComboBox.SelectedIndex = i != -1 ? i : this.ocrLanguages.Count > 0 ? 0 : -1;

            // 翻訳エンジンの設定
            i = this.translationServices.FindIndex(service => service.Name == Properties.Settings.Default.TesseractTranslationEngine);
            if (i == -1) {
                // OCRで使用する翻訳エンジンが設定されていない場合、デフォルトの翻訳エンジンを取得する。
                var activeTranslationService = this.serviceManager.GetActiveService<TranslationService>();
                if (activeTranslationService != null) {
                    i = this.translationServices.FindIndex(service => service.Name == activeTranslationService.Name);
                }

                // デフォルトの翻訳エンジンも見つからない場合、翻訳エンジンリストの先頭を選択する
                if (i == -1 && this.translationServices.Count > 0) {
                    i = 0;
                }
            }
            this.translationEngineComboBox.SelectedIndex = i;
        }

        public override void SaveSettings() {
            Properties.Settings.Default.TesseractOcrLanguage = ocrLangComboBox.SelectedIndex != -1 ? ocrLanguages[ocrLangComboBox.SelectedIndex] : "";
            Properties.Settings.Default.TesseractTranslationEngine = translationEngineComboBox.SelectedIndex != -1 ? translationServices[translationEngineComboBox.SelectedIndex].Name : "";
            Properties.Settings.Default.TesseractTranslationLanguage = translationLangComboBox.SelectedIndex != -1 ? translationLanguages[translationLangComboBox.SelectedIndex].Item1 : "";
        }

        public TesseractOcrPage(Service.ServiceManager serviceManager) : base(serviceManager) {
            this.service = new TesseractOcrService(serviceManager);

            InitializeComponent();
        }

        private void TesseractOcrPage_Load(object sender, EventArgs e) {
            // OCRが読み込み対応している言語一覧
            this.ocrLanguages = service.GetLanguageDatas();
            for (var i = 0; i < ocrLanguages.Count; i++) {
                this.ocrLangComboBox.Items.Add(ocrLanguages[i]);
            }

            // 翻訳エンジンの一覧
            this.translationServices = this.serviceManager.GetServices<TranslationService>();
            foreach (var service in translationServices) {
                this.translationEngineComboBox.Items.Add(service.DisplayName);
            }
        }

        /** 読み取り言語を変更 */
        private void ocrLangComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (ocrLangComboBox.SelectedIndex >= 0) {
                service.OcrLanguage = ocrLanguages[ocrLangComboBox.SelectedIndex];
            } else {
                service.OcrLanguage = "";
            }
        }

        /** 翻訳エンジンを変更 */
        private void translationEngineComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.translationEngineComboBox.SelectedIndex >= 0) {
                this.translationLangComboBox.Items.Clear();

                var translationService = this.translationServices[this.translationEngineComboBox.SelectedIndex];
                this.translationLanguages = translationService.GetLanguages();
                for (var i = 0; i < this.translationLanguages.Length; i++) {
                    var lang = this.translationLanguages[i];
                    this.translationLangComboBox.Items.Add(lang.Item2);
                    if (lang.Item1 == this.service.TranslationLanguage) {
                        this.translationLangComboBox.SelectedIndex = i;
                    }
                }
            } else {
                this.service.TranslationEngine = "";
            }
        }

        /** 翻訳先言語を変更 */
        private void translationLangComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.translationLangComboBox.SelectedIndex != -1) {
                this.service.TranslationLanguage = this.translationLanguages[this.translationLangComboBox.SelectedIndex].Item1;
            }
        }
    }
}
