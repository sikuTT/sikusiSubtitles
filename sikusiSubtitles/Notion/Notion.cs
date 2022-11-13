using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Polly;
using sikusiSubtitles.OCR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.Notion {
    public class Notion {
        private string Url = "https://api.notion.com/v1";
        private string NotionVersion = "2022-06-28";

        private HttpClient HttpClient = new HttpClient();
        private string Key;

        public Notion(string key) {
            Key = key;
        }

        public async Task<JObject?> Search() {
            using var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.RequestUri = new Uri($"{Url}/search");

            var body = new JObject{
                new JProperty("filter", new JObject{
                    new JProperty("property", "object"),
                    new JProperty("value", "database"),
                }),
            }.ToString();
            request.Content = new StringContent(body, Encoding.UTF8 , "application/json");
            request.Headers.Add("Authorization" , $"Bearer {Key}");
            request.Headers.Add("Notion-Version" , NotionVersion);

            // Send the request and get response.
            HttpResponseMessage response = await this.HttpClient.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK) {
                var str = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<JObject>(str);
            }
            return null;
        }

        public async Task<string?> AddOcrResult(OcrServiceManager manager, string game, string ocrText, string translatedText, string translationEngine) {
            using var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.RequestUri = new Uri($"{Url}/pages");

            var body = new JObject{
                new JProperty("parent", new JObject{
                    new JProperty("type", "database_id"),
                    new JProperty("database_id", manager.NotionDatabaseId),
                }),
                new JProperty("properties", CreateOcrProperty(manager, game, ocrText, translatedText, translationEngine)),
            }.ToString();
            request.Content = new StringContent(body , Encoding.UTF8 , "application/json");
            request.Headers.Add("Authorization" , $"Bearer {Key}");
            request.Headers.Add("Notion-Version" , NotionVersion);

            // Send the request and get response.
            HttpResponseMessage response = await this.HttpClient.SendAsync(request);
            var str = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK) {
                var res = JsonConvert.DeserializeObject<JObject>(str);
                if (res != null) {
                    var id = res.Value<string>("id");
                    return id;
                }
            }
            return null;
        }

        public async Task UpdateOcrResult(OcrServiceManager manager, string pageId, string game, string ocrText, string translatedText, string translationEngine) {
            using var request = new HttpRequestMessage();
            request.Method = HttpMethod.Patch;
            request.RequestUri = new Uri($"{Url}/pages/{pageId}");

            var body = new JObject{
                new JProperty("properties", CreateOcrProperty(manager, game, ocrText, translatedText, translationEngine)),
            }.ToString();
            request.Content = new StringContent(body, Encoding.UTF8, "application/json");
            request.Headers.Add("Authorization", $"Bearer {Key}");
            request.Headers.Add("Notion-Version", NotionVersion);

            // Send the request and get response.
            HttpResponseMessage response = await this.HttpClient.SendAsync(request);
            var str = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK) {
            }
        }

        private JObject CreateOcrProperty(OcrServiceManager manager, string game, string ocrText, string translatedText, string translationEngine) {
            var properties = new JObject();

            if (manager.NotionTitleSaveTarget != null) {
                properties.Add(new JProperty(manager.NotionTitleSaveTarget, new JObject {
                    new JProperty("title", new JArray(new []{
                        new JObject {
                            new JProperty("text", new JObject {
                                new JProperty("content", game),
                            }),
                        },
                    }))
                }));
            }

            if (manager.NotionTextSaveTarget != null) {
                properties.Add(new JProperty(manager.NotionTextSaveTarget, new JObject {
                    new JProperty("rich_text", new JArray(new []{
                        new JObject {
                            new JProperty("text", new JObject {
                                new JProperty("content", ocrText),
                            }),
                        },
                    })),
                }));
            }

            if (manager.NotionTranslatedTextSaveTarget != null) {
                properties.Add(new JProperty(manager.NotionTranslatedTextSaveTarget, new JObject {
                    new JProperty("rich_text", new JArray(new []{
                        new JObject {
                            new JProperty("text", new JObject {
                                new JProperty("content", translatedText),
                            }),
                        },
                    })),
                }));
            }

            if (manager.NotionTranslationEngineSaveTarget != null) {
                properties.Add(new JProperty(manager.NotionTranslationEngineSaveTarget, new JObject {
                    new JProperty("rich_text", new JArray(new []{
                        new JObject {
                            new JProperty("text", new JObject {
                                new JProperty("content", translationEngine),
                            }),
                        },
                    })),
                }));
            }

            return properties;
        }
    }
}
