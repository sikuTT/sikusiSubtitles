using sikusiSubtitles.Translation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sikusiSubtitles.OBS {
    public partial class SubtitlesPage : SettingPage {
        private SubtitlesService service;
        private List<TranslationService> translationServices = new List<TranslationService>();
        private TranslationService? translationService;

        public SubtitlesPage(ServiceManager serviceManager, SubtitlesService service) : base(serviceManager) {
            this.service = service;

            InitializeComponent();
        }

        private void SubtitlesPage_Load(object sender, EventArgs e) {
            // 翻訳サービス一覧
            translationServices = serviceManager.GetServices<TranslationService>();
            translationServices.ForEach(service => {
                var i = translationEngineComboBox.Items.Add(service.DisplayName);
                if (service.Name == this.service.TranslationEngine) translationEngineComboBox.SelectedIndex = i;
            });

            // 翻訳先
            this.translationCheckBox1.Checked = service.Translation[0];
            this.translationCheckBox2.Checked = service.Translation[1];

            // 字幕表示先
            this.voiceTextBox.Text = service.VoiceTarget;
            this.translationTextBox1.Text = service.TranslationTargets[0];
            this.translationTextBox2.Text = service.TranslationTargets[1];

            // 表示時間
            this.clearCheckBox.Checked = service.IsClearInterval;
            this.clearIntervalNumericUpDown.Value = service.ClearInterval;
            this.additionalCheckBox.Checked = service.IsAdditionalTime;
            this.additionalTrackBar.Value = service.AdditionalTime;
        }

        /** 翻訳エンジンが選択された */
        private void translationEngineComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (translationEngineComboBox.SelectedIndex != -1) {
                translationService = translationServices[translationEngineComboBox.SelectedIndex];
                service.TranslationEngine = translationService.Name;

                // 翻訳元言語、翻訳先言語を選択されたエンジンがサポートする言語一覧にする
                var languages = translationService.GetLanguages();
                translationFromComboBox.Items.Clear();
                translationToComboBox1.Items.Clear();
                translationToComboBox2.Items.Clear();
                languages.ForEach(lang => {
                    var i = translationFromComboBox.Items.Add(lang.Item2);
                    if (lang.Item1 == service.TranslationLanguageFrom) translationFromComboBox.SelectedIndex = i;

                    i = translationToComboBox1.Items.Add(lang.Item2);
                    if (lang.Item1 == service.TranslationLanguageTo[0]) translationToComboBox1.SelectedIndex = i;

                    i = translationToComboBox2.Items.Add(lang.Item2);
                    if (lang.Item1 == service.TranslationLanguageTo[1]) translationToComboBox2.SelectedIndex = i;
                });
            } else {
                service.TranslationEngine = "";
            }

        }

        /** 翻訳元言語が変更された */
        private void translationFromComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (translationFromComboBox.SelectedIndex != -1) {
                if (translationService != null) {
                    var languages = translationService.GetLanguages();
                    service.TranslationLanguageFrom = languages[translationFromComboBox.SelectedIndex].Item1;
                }
            }
        }

        /** 翻訳先1を翻訳する・しないの切り替え */
        private void translationCheckBox1_CheckedChanged(object sender, EventArgs e) {
            service.Translation[0] = translationCheckBox1.Checked;
        }

        /** 翻訳先言語1が変更された */
        private void translationToComboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            if (translationToComboBox1.SelectedIndex != -1) {
                if (translationService != null) {
                    var languages = translationService.GetLanguages();
                    service.TranslationLanguageTo[0] = languages[translationToComboBox1.SelectedIndex].Item1;
                }
            }
        }

        /** 翻訳先2を翻訳する・しないの切り替え */
        private void translationCheckBox2_CheckedChanged(object sender, EventArgs e) {
            service.Translation[1] = translationCheckBox2.Checked;
        }

        /** 翻訳先言語2が変更された */
        private void translationToComboBox2_SelectedIndexChanged(object sender, EventArgs e) {
            if (translationToComboBox2.SelectedIndex != -1) {
                if (translationService != null) {
                    var languages = translationService.GetLanguages();
                    service.TranslationLanguageTo[1] = languages[translationToComboBox2.SelectedIndex].Item1;
                }
            }
        }

        /** 音声の字幕表示先を設定 */
        private void voiceTextBox_TextChanged(object sender, EventArgs e) {
            this.service.VoiceTarget = voiceTextBox.Text;
        }

        /** 翻訳1の字幕表示先を設定 */
        private void translationTextBox1_TextChanged(object sender, EventArgs e) {
            this.service.TranslationTargets[0] = translationTextBox1.Text;
        }

        /** 翻訳2の字幕表示先を設定 */
        private void translationTextBox2_TextChanged(object sender, EventArgs e) {
            this.service.TranslationTargets[1] = translationTextBox2.Text;
        }

        /** 一定時間で字幕を消す */
        private void clearCheckBox_CheckedChanged(object sender, EventArgs e) {
            this.service.IsClearInterval = clearCheckBox.Checked;
        }

        private void clearIntervalNumericUpDown_ValueChanged(object sender, EventArgs e) {
            this.service.ClearInterval = (int)this.clearIntervalNumericUpDown.Value;
        }

        private void additionalCheckBox_CheckedChanged(object sender, EventArgs e) {
            this.service.IsAdditionalTime = additionalCheckBox.Checked;
        }

        private void additionalTrackBar_Scroll(object sender, EventArgs e) {
            this.service.AdditionalTime = this.additionalTrackBar.Value;
        }
    }
}
