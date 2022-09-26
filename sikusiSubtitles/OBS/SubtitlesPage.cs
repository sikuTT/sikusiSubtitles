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

namespace sikusiSubtitles.OBS {
    public partial class SubtitlesPage : UserControl {
        private ServiceManager serviceManager;
        private SubtitlesService service;
        private List<TranslationService> translationServices = new List<TranslationService>();
        private TranslationService? translationService;
        private List<Tuple<string, string>> translationLanguages = new List<Tuple<string, string>>();

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
                if (service.Name == this.service.TranslationEngine) translationEngineComboBox.SelectedIndex = i;
            });

            // 翻訳先
            service.TranslationToList.ForEach(to => {
                var i = translationToGridView.Rows.Add();
                // translationToGridView.Rows[i].Cells[0];
                translationToGridView.Rows[i].Cells[1].Value = to.Target;
            });
            UpdateAllTranslationToLanguages();

            // 字幕表示先
            this.voiceTextBox.Text = service.VoiceTarget;

            // 表示時間
            this.clearCheckBox.Checked = service.ClearInterval;
            this.clearIntervalNumericUpDown.Value = service.ClearIntervalTime;
            this.additionalCheckBox.Checked = service.Additional;
            this.additionalTrackBar.Value = service.AdditionalTime;
        }

        /** 翻訳エンジンが選択された */
        private void translationEngineComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (translationEngineComboBox.SelectedIndex != -1) {
                translationService = translationServices[translationEngineComboBox.SelectedIndex];
                service.TranslationEngine = translationService.Name;

                // 翻訳元言語、翻訳先言語を選択されたエンジンがサポートする言語一覧にする
                translationFromComboBox.Items.Clear();
                translationLanguages = translationService.GetLanguages();
                translationLanguages.ForEach(lang => {
                    var i = translationFromComboBox.Items.Add(lang.Item2);
                    if (lang.Item1 == service.TranslationLanguageFrom) translationFromComboBox.SelectedIndex = i;
                });
                UpdateAllTranslationToLanguages();
            } else {
                service.TranslationEngine = "";
            }

        }

        /** 翻訳元言語が変更された */
        private void translationFromComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (translationFromComboBox.SelectedIndex != -1) {
                if (translationService != null) {
                    service.TranslationLanguageFrom = translationLanguages[translationFromComboBox.SelectedIndex].Item1;
                }
            }
        }

        /** 音声の字幕表示先を設定 */
        private void voiceTextBox_TextChanged(object sender, EventArgs e) {
            this.service.VoiceTarget = voiceTextBox.Text;
        }

        /** 一定時間で字幕を消す */
        private void clearCheckBox_CheckedChanged(object sender, EventArgs e) {
            this.service.ClearInterval = clearCheckBox.Checked;
        }

        private void clearIntervalNumericUpDown_ValueChanged(object sender, EventArgs e) {
            this.service.ClearIntervalTime = (int)this.clearIntervalNumericUpDown.Value;
        }

        private void additionalCheckBox_CheckedChanged(object sender, EventArgs e) {
            this.service.Additional = additionalCheckBox.Checked;
        }

        private void additionalTrackBar_Scroll(object sender, EventArgs e) {
            this.service.AdditionalTime = this.additionalTrackBar.Value;
        }

        /** 翻訳先を追加 */
        private void translateTargetAddButton_Click(object sender, EventArgs e) {
            this.service.TranslationToList.Add(new SubtitlesService.TranslationTo());
            var i = this.translationToGridView.Rows.Add();
            if (translationService != null) {
                var cell = translationToGridView.Rows[i].Cells[0] as DataGridViewComboBoxCell;
                if (cell != null) {
                    var languages = translationService.GetLanguages();
                    languages.ForEach(lang => {
                        var i = cell.Items.Add(lang.Item2);
                    });
                }
            }
        }

        /** 翻訳先を削除 */
        private void translateTargetRemove_Click(object sender, EventArgs e) {
            foreach (DataGridViewRow item in this.translationToGridView.SelectedRows) {
                this.service.TranslationToList.RemoveAt(item.Index);
                translationToGridView.Rows.RemoveAt(item.Index);
            }
            foreach (DataGridViewCell cell in this.translationToGridView.SelectedCells) {
                this.service.TranslationToList.RemoveAt(cell.RowIndex);
                translationToGridView.Rows.RemoveAt(cell.RowIndex);
            }
        }

        private void translationToDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex >= 0) {
                if (e.ColumnIndex == 0) {
                    var str = this.translationToGridView[0, e.RowIndex].Value as string;
                    if (str != null) {
                        var lang = translationLanguages.Find(lang => lang.Item2 == str);
                        if (lang != null) this.service.TranslationToList[e.RowIndex].Language = lang.Item1;
                    }
                } else {
                    var str = this.translationToGridView[1, e.RowIndex].Value as string;
                    if (str != null) this.service.TranslationToList[e.RowIndex].Target = str;
                }
            }
        }

        private void UpdateAllTranslationToLanguages() {
            var i = 0;
            foreach (DataGridViewRow row in translationToGridView.Rows) {
                var combobox = row.Cells[0] as DataGridViewComboBoxCell;
                if (combobox != null) {
                    combobox.Value = "";
                    combobox.Items.Clear();
                    translationLanguages.ForEach(lang => {
                        combobox.Items.Add(lang.Item2);
                        if (lang.Item1 == service.TranslationToList[i].Language) {
                            combobox.Value = lang.Item2;
                        }
                    });
                }
                i++;
            }
        }
    }
}
