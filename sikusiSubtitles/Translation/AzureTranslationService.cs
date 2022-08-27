﻿using Newtonsoft.Json;
using sikusiSubtitles.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.Translation {
    public class AzureTranslationService : TranslationService {
        public string Key { get; set; }
        public string Region { get; set; }
        public string? From { get; set; }
        public string? To1 { get; set; }
        public string? To2 { get; set; }

        private static readonly string endpoint = "https://api.cognitive.microsofttranslator.com/";
        private HttpClient HttpClient = new HttpClient();

        public AzureTranslationService(ServiceManager serviceManager) : base(serviceManager, "Azure", "Azure Cognitive Services", 300) {
            this.Key = "";
            this.Region = "";
        }

        public override async void Translate(string text) {
            var from = this.From;
            var toList = new List<string>();
            if (this.To1 != null) toList.Add(this.To1);
            if (this.To2 != null) toList.Add(this.To2);
            var result = await TranslateAsync(text, from, toList.ToArray());
            this.InvokeTranslated(result);
        }

        private async Task<TranslationResult> TranslateAsync(string text, string? from, string[] toList) {
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
                            Debug.WriteLine("AzureTranslationService: " + translation.text);
                            var t = new TranslationResult.Translation() {
                                Text = translation.text ?? "",
                                Language = translation.to
                            };
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
}
