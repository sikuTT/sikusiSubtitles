using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.OCR {
    public class AzureOcrService : OcrService{
        private static string endpoint = "https://siku-stream-vision.cognitiveservices.azure.com/";

        public string Key { get; set; } = "";


        public AzureOcrService(ServiceManager serviceManager) : base(serviceManager, "AzureOcr", "Azure Cognitive Service - Computer Vision", 200) {
            SettingPage = new AzureOcrPage(serviceManager, this);
        }

        public override void Load() {
            Key = Decrypt(Properties.Settings.Default.AzureOcrKey);
        }

        public override void Save() {
            Properties.Settings.Default.AzureOcrKey = Encrypt(Key);
        }

        public override List<Tuple<string, string>> GetLanguages() {
            return this.languages;
        }

        public async override Task<string?> ExecuteAsync(Bitmap bitmap, string language) {
            try {
                ComputerVisionClient client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(Key)) { Endpoint = endpoint };

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
                    return null;
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
                this.InvokeOcrFinished(new OcrResult(resultString));
                return resultString;
            } catch (Exception ex) {
                Debug.WriteLine("AzureOcrService: " + ex.Message);
            }
            Debug.WriteLine("End");

            return null;
        }

        List<Tuple<string, string>> languages = new List<Tuple<string, string>>() {
            new Tuple<string, string>("", "（自動）"),
        };
    }
}
