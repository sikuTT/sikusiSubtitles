using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.Translation {
    public class AzureTranslationResult {
        public Language? detectedLanguage { get; set; }
        public List<Translation>? translations { get; set; }

        public class Translation {
            public string? text { get; set; }
            public string? to { get; set; }
        }

        public class Language {
            public string? language { get; set; }
            public double? score { get; set; }
        }
    }

    public class AzureTranslationService : TranslationService {
        public string Region { get; set; }
        public string Key { get; set; }

        private static readonly string endpoint = "https://api.cognitive.microsofttranslator.com/";
        private HttpClient HttpClient = new HttpClient();

        public AzureTranslationService() : base("azure", "Azure Cognitive Services", 400) {
            Region = "";
            Key = "";
        }

        public override async Task<TranslationResult> Translate(string text, string? from, string[] toList) {
            var result = new TranslationResult();
            if (toList.Length == 0)
                return result;

            string route = String.Format("translate?api-version=3.0");
            if (from != null) {
                route = route + "&from=" + from;
            }
            foreach (var to in toList) {
                route = route + "&to=" + to;
            }

            object[] body = new object[] { new { Text = text } };
            var requestBody = JsonConvert.SerializeObject(body);
            var uri = new Uri(endpoint + route);

            using (var request = new HttpRequestMessage()) {
                try {
                    // Build the request.
                    request.Method = HttpMethod.Post;
                    request.RequestUri = uri;
                    request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                    request.Headers.Add("Ocp-Apim-Subscription-Key", this.Key);
                    request.Headers.Add("Ocp-Apim-Subscription-Region", this.Region);

                    // Send the request and get response.
                    HttpResponseMessage response = await this.HttpClient.SendAsync(request).ConfigureAwait(false);

                    // Read response.
                    string responseText = await response.Content.ReadAsStringAsync();
                    AzureTranslationResult[]? azureResults = JsonConvert.DeserializeObject<AzureTranslationResult[]>(responseText);

                    if (azureResults == null)
                        return result;
                    foreach (var azureResult in azureResults) {
                        result.DetectLanguage = azureResult.detectedLanguage?.language;

                        if (azureResult.translations == null)
                            continue;

                        foreach (var translation in azureResult.translations) {
                            var t = new TranslationResult.Translation();
                            t.Text = translation.text != null ? translation.text : "";
                            t.Language = translation.to;
                            result.Translations.Add(t);
                        }
                    }
                } catch (Exception ex) {
                    Debug.WriteLine(ex.Message);
                    result.Error = true;
                    result.Translations.Add(new TranslationResult.Translation() { Text = ex.Message });
                }

                return result;
            }
        }
    }
}
