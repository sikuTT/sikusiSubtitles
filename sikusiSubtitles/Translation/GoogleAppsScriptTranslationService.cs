using sikusiSubtitles.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.Translation {
    public class GoogleAppsScriptTranslationService : TranslationService {
        private HttpClient HttpClient = new HttpClient();

        public string? Key { get; set; }
        public string? From { get; set; }
        public string? To1 { get; set; }
        public string? To2 { get; set; }

        public GoogleAppsScriptTranslationService(ServiceManager serviceManager) : base(serviceManager, "GoogleAppsScript", "Google Apps Script", 200) {
        }

        public override async void Translate(string text) {
            if (CheckParameters() == false) {
                return;
            }

            var toList = new List<string>();
            if (To1 != null) toList.Add(To1);
            if (To2 != null) toList.Add(To2);
            var result = await TranslateAsync(text, From, toList.ToArray());
            this.InvokeTranslated(result);
        }

        public override async void Translate(string text, string to) {
            if (CheckParameters() == false) {
                return;
            }

            var toList = new string[] { to };
            var result = await TranslateAsync(text, null, toList);
            this.InvokeTranslated(result);
        }

        public async Task<TranslationResult> TranslateAsync(string text, string? from, string[] toList) {
            TranslationResult result = new TranslationResult();
            if (toList.Length == 0)
                return result;

            try {
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
                Debug.WriteLine(ex.Message);
                result.Translations.Add(new TranslationResult.Translation() { Text = ex.Message });
                result.Error = true;
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
