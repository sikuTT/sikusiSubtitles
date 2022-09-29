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
        }

        private void translationToDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
        }

        private void ObsSubtitlesPage_VisibleChanged(object sender, EventArgs e) {
            if (this.Visible == true) {
                SetTranslationTarget();
            }
        }

        private void SetTranslationTarget() {
            if (subtitlesService != null) {
                for (var i = 0; i < subtitlesService.TranslationLanguageToList.Count; i++) {
                    if (service.TranslateTargetList.Count < i + 1) {
                        service.TranslateTargetList.Add(new TranslateTarget());
                    }
                    service.TranslateTargetList[i].Language = subtitlesService.TranslationLanguageToList[i];

                    if (translationToGridView.Rows.Count < i + 1) {
                        translationToGridView.Rows.Add();
                    }
                    translationToGridView.Rows[i].Cells[0].Value = subtitlesService.TranslationLanguageToList[i];
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
