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

namespace sikusiSubtitles.Subtitles {
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
            // 翻訳先一覧を必要な数だけ作成する
            // 翻訳サービス設定時に翻訳先一覧のコンボボックスにもエンジンがサポートする言語一覧が挿入されるので、
            // 翻訳サービスの設定前にコンボボックスを作成しておく必要がある。
            service.TranslationLanguageToList.ForEach(lang => {
                translationToGridView.Rows.Add();
            });

            // 翻訳サービス一覧
            translationServices = serviceManager.GetServices<TranslationService>();
            translationServices.ForEach(service => {
                var i = translationEngineComboBox.Items.Add(service.DisplayName);
                if (service.Name == this.service.TranslationEngine) translationEngineComboBox.SelectedIndex = i;
            });

            // 字幕を消すまでの時間
            clearCheckBox.Checked = service.ClearInterval;
            clearIntervalNumericUpDown.Value = service.ClearIntervalTime;
            additionalCheckBox.Checked = service.AdditionalClear;
            additionalTrackBar.Value = service.AdditionalClearTime;
        }

        /** 翻訳エンジンの変更 */
        private void translationEngineComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            translationFromComboBox.Items.Clear();
            if (translationEngineComboBox.SelectedIndex == 0) {
                translationService = null;
                service.TranslationEngine = "";
            } else if (translationEngineComboBox.SelectedIndex > 0) {
                translationService = translationServices[translationEngineComboBox.SelectedIndex - 1];
                service.TranslationEngine = translationService.Name;

                translationLanguages = translationService.GetLanguages();

                // 翻訳元言語一覧
                translationLanguages.ForEach(lang => {
                    var i = translationFromComboBox.Items.Add(lang.Item2);
                    if (lang.Item1 == service.TranslationLanguageFrom) translationFromComboBox.SelectedIndex = i;
                });

                // 翻訳先言語一覧
                foreach (DataGridViewRow row in translationToGridView.Rows) {
                    var cell = row.Cells[0] as DataGridViewComboBoxCell;
                    if (cell != null) {
                        cell.Value = "";
                        cell.Items.Clear();
                        translationLanguages.ForEach(lang => {
                            cell.Items.Add(lang.Item2);
                            if (lang.Item1 == service.TranslationLanguageToList[row.Index]) cell.Value = lang.Item2;
                        });
                    }
                }
            }
        }

        /** 翻訳元言語の変更 */
        private void translationFromComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (translationFromComboBox.SelectedIndex != -1) {
                service.TranslationLanguageFrom = this.translationLanguages[translationFromComboBox.SelectedIndex].Item1;
            }
        }

        /** 翻訳先言語の変更 */
        private void translationToGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex != -1 && e.ColumnIndex != -1) {
                var cell = translationToGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewComboBoxCell;
                if (cell != null) {
                    var lang = translationLanguages.Find(lang => lang.Item2 == cell.Value.ToString());
                    if (lang != null) {
                        service.TranslationLanguageToList[e.RowIndex] = lang.Item1;
                    }
                }
            }
        }

        private void translateTargetAddButton_Click(object sender, EventArgs e) {
            service.TranslationLanguageToList.Add("");
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

        private void translateTargetRemove_Click(object sender, EventArgs e) {
            if (this.translationToGridView.SelectedRows.Count > 0) {
                foreach (DataGridViewRow row in this.translationToGridView.SelectedRows) {
                    this.service.TranslationLanguageToList.RemoveAt(row.Index);
                    translationToGridView.Rows.Remove(row);
                }
            } else {
                foreach (DataGridViewCell cell in this.translationToGridView.SelectedCells) {
                    this.service.TranslationLanguageToList.RemoveAt(cell.RowIndex);
                    translationToGridView.Rows.RemoveAt(cell.RowIndex);
                }
            }
        }

        /** 字幕を一定時間で削除する */
        private void clearCheckBox_CheckedChanged(object sender, EventArgs e) {
            service.ClearInterval = clearCheckBox.Checked;
        }

        /** 字幕を一定時間で削除する場合の時間 */
        private void clearIntervalNumericUpDown_ValueChanged(object sender, EventArgs e) {
            service.ClearIntervalTime = (int)clearIntervalNumericUpDown.Value;
        }

        /** 文章の長さで字幕をクリアする時間を伸ばす */
        private void additionalCheckBox_CheckedChanged(object sender, EventArgs e) {
            service.AdditionalClear = additionalCheckBox.Checked;
        }

        /** 文章の長さで字幕をクリアする時間を伸ばす場合、どれだけ伸ばすかを設定 */
        private void additionalTrackBar_Scroll(object sender, EventArgs e) {
            service.AdditionalClearTime = additionalTrackBar.Value;
        }

        /** このページの表示状態の変更*/
        private void SubtitlesPage_VisibleChanged(object sender, EventArgs e) {
            // 非表示になった場合、翻訳先言語に未設定の項目があった場合削除する
            if (this.Visible == false) {
                var i = 0;
                foreach (DataGridViewRow row in this.translationToGridView.Rows) {
                    if (row.Cells[0].Value != null && row.Cells[0].ToString() != "") {
                        i++;
                    } else {
                        this.service.TranslationLanguageToList.RemoveAt(i);
                        this.translationToGridView.Rows.Remove(row);
                    }
                }
            }
        }
    }
}
