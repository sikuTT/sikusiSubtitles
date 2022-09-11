﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.Translation {
    public class GoogleAppsScriptTranslationService : TranslationService {
        private HttpClient HttpClient = new HttpClient();
        private List<Tuple<string, string>> languages = new GoogleTranslationLanguages().Languages;

        public string Key { get; set; } = "";

        public GoogleAppsScriptTranslationService(ServiceManager serviceManager) : base(serviceManager, "GoogleAppsScript", "Google Apps Script", 100) {
            SettingPage = new GoogleAppsScriptTranslationPage(serviceManager, this);
        }

        public override void Load() {
            Key = Decrypt(Properties.Settings.Default.GoogleAppsScriptTranslationKey);
        }

        public override void Save() {
            Properties.Settings.Default.GoogleAppsScriptTranslationKey = Encrypt(Key);
        }

        public override List<Tuple<string, string>> GetLanguages() {
            return this.languages;
        }

        public override async Task<TranslationResult> TranslateAsync(string text, string? from, string to) {
            var result = await TranslateAsync(text, from, new string[] { to });
            return result;
        }

        public override async Task<TranslationResult> TranslateAsync(string text, string? from, string[] toList) {
            var result = new TranslationResult();

            try {
                if (CheckParameters() == false) return result;

                foreach (var to in toList) {
                    var url = "https://script.google.com/macros/s/";
                    url += this.Key + "/exec?text=" + text;
                    if (from != null)
                        url += "&source=" + from;
                    url += "&target=" + to;
                    using var request = new HttpRequestMessage();
                    request.Method = HttpMethod.Get;
                    request.RequestUri = new Uri(url);
                    HttpResponseMessage response = await this.HttpClient.SendAsync(request).ConfigureAwait(false);
                    string responseText = await response.Content.ReadAsStringAsync();
                    result.Translations.Add(new TranslationResult.Translation() { Text = responseText });
                    Debug.WriteLine("GoogleAppsScriptTranslationService: " + responseText);
                }
            } catch (Exception ex) {
                Debug.WriteLine("GoogleAppsScriptTranslationService: " + ex.Message);
                result.Error = true;
                result.Translations.Add(new TranslationResult.Translation() { Text = ex.Message });
            } finally {
                InvokeTranslated(result);
            }
            return result;
        }

        private bool CheckParameters() {
            if (this.Key == null || this.Key == "") {
                MessageBox.Show("APIキーが設定されていません。", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            } else {
                return true;
            }
        }
    }
}
