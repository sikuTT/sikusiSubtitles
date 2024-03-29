﻿using sikusiSubtitles.Translation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace sikusiSubtitles.Subtitles {
    /** 字幕表示先モデル */
    public class LanguageModel {
        public string Code { get; set; } = "";
        public string Language { get; set; } = "";
    }


    /** 字幕表示先モデル */
    public class LanguageListModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<LanguageModel> LanguageList { get; set; } = new ObservableCollection<LanguageModel>();
        public string SelectedCode {
            get => selectedCode;
            set {
                selectedCode = value;
                NotifyPropertyChanged();
            }
        }
        private string selectedCode {get; set; } ="";

        private void NotifyPropertyChanged([CallerMemberName]String propertyName = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    /// <summary>
    /// SubtitlesPage.xaml の相互作用ロジック
    /// </summary>
    public partial class SubtitlesPage : UserControl {
        // モデル
        ObservableCollection<LanguageListModel> translationLanguageList = new ObservableCollection<LanguageListModel>();

        // サービス
        ServiceManager serviceManager;
        SubtitlesService service;


        // 翻訳サービス
        List<TranslationService> translationServices = new List<TranslationService>();
        TranslationService? translationService;
        List<Language> translationLanguages = new List<Language>();

        public SubtitlesPage(ServiceManager serviceManager, SubtitlesService service) {
            this.serviceManager = serviceManager;
            this.service = service;

            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            // 翻訳サービス一覧
            translationServices = serviceManager.GetServices<TranslationService>();

            // 翻訳先一覧を必要な数だけ作成する
            listBox.DataContext = translationLanguageList;
            foreach (var lang in service.TranslationLanguageToList) {
                var languageList = new LanguageListModel { SelectedCode = lang };
                translationLanguageList.Add(languageList);
            }

            // 翻訳サービス一覧
            var translationServiceList = translationServices.Select(service => service.DisplayName).ToList();
            translationServiceList.Insert(0, "（翻訳しない）");
            translationServiceComboBox.ItemsSource = translationServiceList;
            var i = translationServices.FindIndex(service => service.Name == this.service.TranslationEngine);
            translationServiceComboBox.SelectedIndex = i != -1 ? i + 1 : 0;

            // 字幕を消すまでの時間
            clearIntervalCheckBox.IsChecked = service.ClearInterval;
            clearIntervalNumericEditBox.Value = service.ClearIntervalTime;
            additionalClearTimeCheckBox.IsChecked = service.AdditionalClear;
            additionalClearTimeSlider.Value = service.AdditionalClearTime;
        }

        /**
         * コントロールの表示状態が変わった
         * 設定画面を表示したか、設定画面から出て行った
         */
        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e) {
            if ((bool)e.NewValue == false) {
                // 設定画面から出ていくとき、選択されていない翻訳先は削除する
                for (var i = translationLanguageList.Count - 1; i >= 0; i--) {
                    if (translationLanguageList[i].SelectedCode == "") {
                        translationLanguageList.RemoveAt(i);
                        service.TranslationLanguageToList.RemoveAt(i);
                    }
                }
            }
        }

        /** 翻訳サービスが変更された */
        private void translationServiceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            translationLanguageFromComboBox.ItemsSource = null;
            foreach (var item in translationLanguageList) {
                item.LanguageList.Clear();
                item.SelectedCode = "";
            }

            if (translationServiceComboBox.SelectedIndex > 0) {
                // 使用する翻訳サービスを変更
                translationService = translationServices[translationServiceComboBox.SelectedIndex - 1];
                service.TranslationEngine = translationService.Name;

                // 翻訳元言語を選択された翻訳サービスがサポートする言語一覧で更新する
                translationLanguages = translationService.GetLanguages();
                translationLanguageFromComboBox.ItemsSource = translationLanguages;
                translationLanguageFromComboBox.SelectedIndex = translationLanguages.FindIndex(lang => lang.Code == service.TranslationLanguageFrom);

                // 翻訳先言語を選択された翻訳サービスがサポートする言語一覧で更新する
                for (var i = 0; i < service.TranslationLanguageToList.Count; i++) {
                    var item = translationLanguageList[i];
                    foreach (var lang in translationLanguages) {
                        var langModel = new LanguageModel() { Code = lang.Code, Language = lang.Name };
                        item.LanguageList.Add(langModel);
                        if (service.TranslationLanguageToList[i] == lang.Code) item.SelectedCode = lang.Code;
                    }
                }
            } else {
                service.TranslationEngine = "";
            }
        }

        /** 翻訳元言語が変更された */
        private void translationLanguageFromComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var lang = translationLanguageFromComboBox.SelectedItem as Language;
            if (lang != null) {
                service.TranslationLanguageFrom = lang.Code;
            }
        }

        /** 翻訳先言語が変更された */
        private void translationLanguageToComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var comboBox = sender as ComboBox;
            if (comboBox != null) {
                var item = comboBox.DataContext as LanguageListModel;
                if (item?.SelectedCode != null) {
                    var index = listBox.Items.IndexOf(item);
                    if (index != -1) {
                        service.TranslationLanguageToList[index] = item.SelectedCode;
                    }
                }
            }
        }

        /** 翻訳先言語が追加された */
        private void addTranslateToLanguageButton_Click(object sender, RoutedEventArgs e) {
            var item = new LanguageListModel();
            foreach (var lang in translationLanguages) {
                item.LanguageList.Add(new LanguageModel() { Code = lang.Code, Language = lang.Name });
            }
            translationLanguageList.Add(item);
            service.TranslationLanguageToList.Add("");
        }

        /** 翻訳先言語が削除された */
        private void deleteTranslationLanguageToButton_Click(object sender, RoutedEventArgs e) {
            var control = sender as Control;
            if (control != null) {
                object item = control.DataContext;
                var index = listBox.Items.IndexOf(item);
                if (index != -1) {
                    translationLanguageList.RemoveAt(index);
                    service.TranslationLanguageToList.RemoveAt(index);
                }
            }
        }

        /** 「一定時間で字幕を消す」を設定 */
        private void clearIntervalCheckBox_Checked(object sender, RoutedEventArgs e) {
            if (IsLoaded) {
                service.ClearInterval = true;
            }
        }

        /** 「一定時間で字幕を消す」の設定を解除 */
        private void clearIntervalCheckBox_Unchecked(object sender, RoutedEventArgs e) {
            if (IsLoaded) {
                service.ClearInterval = false;
            }
        }

        /** 字幕を消すまでの時間の設定が変更された */
        private void clearIntervalNumericEditBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e) {
            if (IsLoaded) {
                service.ClearIntervalTime = e.NewValue;
            }
        }

        /** 文字場長い場合、字幕を消すまでの時間を長くするオプションがチェックされた */
        private void additionalClearTimeCheckBox_Checked(object sender, RoutedEventArgs e) {
            if (IsLoaded) {
                service.AdditionalClear = true;
            }
        }

        /** 文字場長い場合、字幕を消すまでの時間を長くするオプションが解除された */
        private void additionalClearTimeCheckBox_Unchecked(object sender, RoutedEventArgs e) {
            if (IsLoaded) {
                service.AdditionalClear = false;
            }
        }

        /** 文字が長い場合、字幕を消すまでの時間をどれだけ長くするかの設定が変更された */
        private void additionalClearTimeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            if (IsLoaded) {
                service.AdditionalClearTime = (int)additionalClearTimeSlider.Value;
            }
        }
    }
}
