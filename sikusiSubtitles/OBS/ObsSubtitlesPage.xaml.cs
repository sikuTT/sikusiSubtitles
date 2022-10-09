using sikusiSubtitles.Subtitles;
using sikusiSubtitles.Translation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace sikusiSubtitles.OBS {
    public class TranslateTarget : INotifyPropertyChanged {
        public event PropertyChangedEventHandler? PropertyChanged;

        public Language TranslateTo { get; set; } = new Language("", "");

        public string DisplayTarget {
            get { return displayTarget; }
            set {
                displayTarget = value;
                NotifyPropertyChanged();
            }
        }
        string displayTarget = "";

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    /// <summary>
    /// ObsSubtitlesPage.xaml の相互作用ロジック
    /// </summary>
    public partial class ObsSubtitlesPage : UserControl {
        ObservableCollection<TranslateTarget> translateTargets = new ObservableCollection<TranslateTarget>();

        ServiceManager serviceManager;
        ObsSubtitlesService service;
        SubtitlesService? subtitlesService;

        public ObsSubtitlesPage(ServiceManager serviceManager, ObsSubtitlesService service) {
            this.serviceManager = serviceManager;
            this.service = service;
            this.subtitlesService = serviceManager.GetService<SubtitlesService>();

            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            targetListView.ItemsSource = translateTargets;

            voiceTextBox.Text = service.VoiceTarget;
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e) {
            if (IsVisible) {
                if (subtitlesService != null) {
                    var translationService = serviceManager.GetServices<TranslationService>().Find(service => service.Name == subtitlesService.TranslationEngine);
                    var languages = translationService != null ? translationService.GetLanguages() : null;

                    for (var i = 0; i < subtitlesService.TranslationLanguageToList.Count; i++) {
                        var langCode = subtitlesService.TranslationLanguageToList[i];
                        var langName = languages?.Find(l => l.Code == langCode)?.Name;

                        // OBSの字幕表示先のリストが字幕のリストより小さければ、表示先を作成する
                        if (service.TranslateTargetList.Count <= i) {
                            service.TranslateTargetList.Add("");
                        }

                        // 字幕表示先リストビューのアイテムを更新
                        TranslateTarget target = new TranslateTarget() {
                            TranslateTo = new Language(langCode, langName ?? langCode),
                            DisplayTarget = service.TranslateTargetList[i],
                        };
                        if (i < translateTargets.Count) {
                            translateTargets[i] = target;
                        } else {
                            translateTargets.Add(target);
                        }
                    }

                    // 字幕の表示件数が減っていたら、OBSの表示先も削除する
                    if (service.TranslateTargetList.Count > subtitlesService.TranslationLanguageToList.Count) {
                        service.TranslateTargetList.RemoveRange(subtitlesService.TranslationLanguageToList.Count, service.TranslateTargetList.Count - subtitlesService.TranslationLanguageToList.Count);
                    }
                    for (var i = subtitlesService.TranslationLanguageToList.Count; i < translateTargets.Count;) {
                        translateTargets.RemoveAt(i);
                    }
                }
            }
        }

        /** 音声字幕の表示先が入力された */
        private void voiceTextBox_TextChanged(object sender, TextChangedEventArgs e) {
            service.VoiceTarget = voiceTextBox.Text;
        }

        /** 翻訳結果の字幕の表示先が入力された */
        private void translateTargetTextBox_LostFocus(object sender, RoutedEventArgs e) {
            var textBox = sender as TextBox;
            if (textBox != null) {
                var index = targetListView.Items.IndexOf(textBox.DataContext);
                service.TranslateTargetList[index] = translateTargets[index].DisplayTarget;
            }
        }
    }
}
