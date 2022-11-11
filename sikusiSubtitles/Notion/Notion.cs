using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        public async void AddOcrResult(string databaseId, string game, string ocrText, string translatedText, string translationEngine) {
            using var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.RequestUri = new Uri($"{Url}/pages");

            var body = new JObject{
                new JProperty("parent", new JObject{
                    new JProperty("type", "database_id"),
                    new JProperty("database_id", databaseId),
                }),
                new JProperty("properties", new JObject {
                    new JProperty("ゲーム", new JObject {
                        new JProperty("title", new JArray(new []{
                            new JObject {
                                new JProperty("text", new JObject {
                                    new JProperty("content", game),
                                }),
                            },
                        }))
                    }),
                    new JProperty("テキスト", new JObject {
                        new JProperty("rich_text", new JArray(new []{
                            new JObject {
                                new JProperty("text", new JObject {
                                    new JProperty("content", ocrText),
                                }),
                            },
                        })),
                    }),
                    new JProperty("翻訳結果", new JObject {
                        new JProperty("rich_text", new JArray(new []{
                            new JObject {
                                new JProperty("text", new JObject {
                                    new JProperty("content", translatedText),
                                }),
                            },
                        })),
                    }),
                    new JProperty("翻訳エンジン", new JObject {
                        new JProperty("rich_text", new JArray(new []{
                            new JObject {
                                new JProperty("text", new JObject {
                                    new JProperty("content", translationEngine),
                                }),
                            },
                        })),
                    }),
                }),
            }.ToString();
            request.Content = new StringContent(body , Encoding.UTF8 , "application/json");
            request.Headers.Add("Authorization" , $"Bearer {Key}");
            request.Headers.Add("Notion-Version" , NotionVersion);

            // Send the request and get response.
            HttpResponseMessage response = await this.HttpClient.SendAsync(request);
            var str = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK) {
            } else {
            }
        }
    }
}
