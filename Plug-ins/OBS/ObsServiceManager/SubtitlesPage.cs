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
    public partial class SubtitlesPage : UserControl {
        private ServiceManager serviceManager;
        private SubtitlesService service;
        private List<TranslationService> translationServices = new List<TranslationService>();
        private TranslationService? translationService;

        public SubtitlesPage(ServiceManager serviceManager, SubtitlesService service) {
            this.serviceManager = serviceManager;
            this.service = service;

            InitializeComponent();
        }

        private void SubtitlesPage_Load(object sender, EventArgs e) {
            // 翻訳サービス一覧
            translationServices = serviceManager.GetServices<TranslationService>();
            translationServices.ForEach(service => {
                var i = translationEngineComboBox.Items.Add(service.DisplayName);
                if (service.Name == this.service.Props.TranslationEngine) translationEngineComboBox.SelectedIndex = i;
            });

            // 翻訳先
            this.translationCheckBox1.Checked = service.Props.Translation[0];
            this.translationCheckBox2.Checked = service.Props.Translation[1];

            // 字幕表示先
            this.voiceTextBox.Text = service.Props.VoiceTarget;
            this.translationTextBox1.Text = service.Props.TranslationTargets[0];
            this.translationTextBox2.Text = service.Props.TranslationTargets[1];

            // 表示時間
            this.clearCheckBox.Checked = service.Props.IsClearInterval;
            try {
                this.clearIntervalNumericUpDown.Value = service.Props.ClearInterval;
            } catch (Exception) {
            }
            this.additionalCheckBox.Checked = service.Props.IsAdditionalTime;
            try {
                this.additionalTrackBar.Value = service.Props.AdditionalTime;
            } catch (Exception) {
            }
        }

        /** 翻訳エンジンが選択された */
        private void translationEngineComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (translationEngineComboBox.SelectedIndex != -1) {
                translationService = translationServices[translationEngineComboBox.SelectedIndex];
                service.Props.TranslationEngine = translationService.Name;

                // 翻訳元言語、翻訳先言語を選択されたエンジンがサポートする言語一覧にする
                var languages = translationService.GetLanguages();
                translationFromComboBox.Items.Clear();
                translationToComboBox1.Items.Clear();
                translationToComboBox2.Items.Clear();
                languages.ForEach(lang => {
                    var i = translationFromComboBox.Items.Add(lang.Item2);
                    if (lang.Item1 == service.Props.TranslationLanguageFrom) translationFromComboBox.SelectedIndex = i;

                    i = translationToComboBox1.Items.Add(lang.Item2);
                    if (lang.Item1 == service.Props.TranslationLanguageTo[0]) translationToComboBox1.SelectedIndex = i;

                    i = translationToComboBox2.Items.Add(lang.Item2);
                    if (lang.Item1 == service.Props.TranslationLanguageTo[1]) translationToComboBox2.SelectedIndex = i;
                });
            } else {
                service.Props.TranslationEngine = "";
            }

        }

        /** 翻訳元言語が変更された */
        private void translationFromComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (translationFromComboBox.SelectedIndex != -1) {
                if (translationService != null) {
                    var languages = translationService.GetLanguages();
                    service.Props.TranslationLanguageFrom = languages[translationFromComboBox.SelectedIndex].Item1;
                }
            }
        }

        /** 翻訳先1を翻訳する・しないの切り替え */
        private void translationCheckBox1_CheckedChanged(object sender, EventArgs e) {
            service.Props.Translation[0] = translationCheckBox1.Checked;
        }

        /** 翻訳先言語1が変更された */
        private void translationToComboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            if (translationToComboBox1.SelectedIndex != -1) {
                if (translationService != null) {
                    var languages = translationService.GetLanguages();
                    service.Props.TranslationLanguageTo[0] = languages[translationToComboBox1.SelectedIndex].Item1;
                }
            }
        }

        /** 翻訳先2を翻訳する・しないの切り替え */
        private void translationCheckBox2_CheckedChanged(object sender, EventArgs e) {
            service.Props.Translation[1] = translationCheckBox2.Checked;
        }

        /** 翻訳先言語2が変更された */
        private void translationToComboBox2_SelectedIndexChanged(object sender, EventArgs e) {
            if (translationToComboBox2.SelectedIndex != -1) {
                if (translationService != null) {
                    var languages = translationService.GetLanguages();
                    service.Props.TranslationLanguageTo[1] = languages[translationToComboBox2.SelectedIndex].Item1;
                }
            }
        }

        /** 音声の字幕表示先を設定 */
        private void voiceTextBox_TextChanged(object sender, EventArgs e) {
            this.service.Props.VoiceTarget = voiceTextBox.Text;
        }

        /** 翻訳1の字幕表示先を設定 */
        private void translationTextBox1_TextChanged(object sender, EventArgs e) {
            this.service.Props.TranslationTargets[0] = translationTextBox1.Text;
        }

        /** 翻訳2の字幕表示先を設定 */
        private void translationTextBox2_TextChanged(object sender, EventArgs e) {
            this.service.Props.TranslationTargets[1] = translationTextBox2.Text;
        }

        /** 一定時間で字幕を消す */
        private void clearCheckBox_CheckedChanged(object sender, EventArgs e) {
            this.service.Props.IsClearInterval = clearCheckBox.Checked;
        }

        private void clearIntervalNumericUpDown_ValueChanged(object sender, EventArgs e) {
            this.service.Props.ClearInterval = (int)this.clearIntervalNumericUpDown.Value;
        }

        private void additionalCheckBox_CheckedChanged(object sender, EventArgs e) {
            this.service.Props.IsAdditionalTime = additionalCheckBox.Checked;
        }

        private void additionalTrackBar_Scroll(object sender, EventArgs e) {
            this.service.Props.AdditionalTime = this.additionalTrackBar.Value;
        }
    }
}
