using sikusiSubtitles.Subtitles;
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
    public partial class ObsSubtitlesPage : UserControl {
        private ServiceManager serviceManager;
        private ObsSubtitlesService service;
        private SubtitlesService? subtitlesService;

        public ObsSubtitlesPage(ServiceManager serviceManager, ObsSubtitlesService service) {
            this.serviceManager = serviceManager;
            this.service = service;

            InitializeComponent();
        }

        private void ObsSubtitlesPage_Load(object sender, EventArgs e) {
            subtitlesService = serviceManager.GetService<SubtitlesService>();
            SetTranslationTarget();
        }

        /** 音声の字幕表示先を設定 */
        private void voiceTextBox_TextChanged(object sender, EventArgs e) {
            service.VoiceTarget = voiceTextBox.Text;
        }

        /** 翻訳結果の字幕表示先を設定 */
        private void translationToDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
            if(e.RowIndex >= 0 && e.ColumnIndex == 1) {
                var text = translationToGridView[e.ColumnIndex, e.RowIndex].Value.ToString();
                if (text != null) {
                    service.TranslateTargetList[e.RowIndex].Target = text;
                }
            }
        }

        private void ObsSubtitlesPage_VisibleChanged(object sender, EventArgs e) {
            if (this.Visible == true) {
                SetTranslationTarget();
            }
        }

        private void SetTranslationTarget() {
            if (subtitlesService != null) {
                var translationService = serviceManager.GetServices<TranslationService>().Find(service => service.Name == subtitlesService.TranslationEngine);
                if (translationService != null) {
                    var languages = translationService.GetLanguages();

                    for (var i = 0; i < subtitlesService.TranslationLanguageToList.Count; i++) {
                        if (service.TranslateTargetList.Count < i + 1) {
                            service.TranslateTargetList.Add(new TranslateTarget());
                        }
                        service.TranslateTargetList[i].Language = subtitlesService.TranslationLanguageToList[i];

                        if (translationToGridView.Rows.Count < i + 1) {
                            translationToGridView.Rows.Add();
                        }

                        var language = languages.Find(lang => lang.Item1 == service.TranslateTargetList[i].Language);
                        if (language != null) {
                            translationToGridView.Rows[i].Cells[0].Value = language.Item2;
                        }
                        translationToGridView.Rows[i].Cells[1].Value = service.TranslateTargetList[i].Target;
                    }

                    if (subtitlesService.TranslationLanguageToList.Count < service.TranslateTargetList.Count) {
                        for (var i = service.TranslateTargetList.Count; i > subtitlesService.TranslationLanguageToList.Count; i--) {
                            service.TranslateTargetList.RemoveAt(i - 1);
                        }
                    }

                    if (subtitlesService.TranslationLanguageToList.Count < translationToGridView.Rows.Count) {
                        for (var i = translationToGridView.Rows.Count; i > subtitlesService.TranslationLanguageToList.Count; i--) {
                            translationToGridView.Rows.RemoveAt(i - 1);
                        }

                    }
                }
            }
        }
    }
}
