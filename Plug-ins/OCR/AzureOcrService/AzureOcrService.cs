using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.OCR {
    class Properties {
        public string Key = "";
        public string Endpoint = "";
    }

    public class AzureOcrService : OcrService{
        public string Key { get; set; } = "";
        public string Endpoint { get; set; } = "";


        public AzureOcrService(ServiceManager serviceManager) : base(serviceManager, "AzureOcr", "Azure Cognitive Service - Computer Vision", 200) {
        }

        public override UserControl? GetSettingPage() {
            return new AzureOcrPage(ServiceManager, this);
        }

        public override void Load(JToken token) {
            var props = token.ToObject<Properties>();
            if (props != null) {
                Key = Decrypt(props.Key);
                Endpoint = props.Endpoint;
            }
        }

        public override JObject Save() {
            return new JObject(Name, new Properties() {
                Key = Encrypt(Key),
                Endpoint = Endpoint,
            });
        }

        public override List<Tuple<string, string>> GetLanguages() {
            return this.languages;
        }

        public async override Task<OcrResult> ExecuteAsync(Bitmap bitmap, string language) {
            try {
                ComputerVisionClient client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(Key)) { Endpoint = this.Endpoint };

                MemoryStream memoryStream = new MemoryStream();
                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                memoryStream.Position = 0;

                var headers = await client.ReadInStreamAsync(memoryStream, language == "" ? null : language);
                string operationLocation = headers.OperationLocation;

                // OperationLocationの最後の / 以降がID
                var strList = operationLocation.Split('/');
                string operationId;
                if (strList.Length > 0) {
                    operationId = strList.Last();
                } else {
                    return new OcrResult() { Error = "OperationIDが取得できません" };
                }

                // 処理結果を取得
                ReadOperationResult results;
                do {
                    results = await client.GetReadResultAsync(Guid.Parse(operationId));
                    if (results.Status == OperationStatusCodes.Running || results.Status == OperationStatusCodes.NotStarted) {
                        Thread.Sleep(100);
                    }
                }
                while ((results.Status == OperationStatusCodes.Running || results.Status == OperationStatusCodes.NotStarted));

                string resultString = "";
                if (results.AnalyzeResult?.ReadResults != null) {
                    var analyzeResult = results.AnalyzeResult.ReadResults;
                    foreach (ReadResult page in analyzeResult) {
                        foreach (Line line in page.Lines) {
                            resultString = ConcatString(resultString, line.Text);
                        }
                    }
                }

                return new OcrResult() { Text = resultString };
            } catch (Exception ex) {
                Debug.WriteLine("AzureOcrService: " + ex.Message);
                return new OcrResult() { Error = ex.Message };
            }
        }

        List<Tuple<string, string>> languages = new List<Tuple<string, string>>() {
            new Tuple<string, string>("", "（自動）"),
        };
    }
}
