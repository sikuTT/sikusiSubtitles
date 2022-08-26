using Google.Apis.Auth.OAuth2;
using Google.Cloud.Vision.V1;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.OCR {
    public class GoogleVisionOcr : Ocr {
        public GoogleVisionOcr() {
            var credential = GoogleCredential.FromAccessToken("AIzaSyD1JBd2ZEbjCixjmM6mhJjEEpX7t-Ub84g");
        }

        public override Result Execute(Bitmap bitmap) {
            /*
            ImageConverter converter = new ImageConverter();
            Object? o = converter.ConvertTo(bitmap, typeof(byte[]));
            if (o != null) {
                Google.Cloud.Vision.V1.Image image = Google.Cloud.Vision.V1.Image.FromBytes((byte[])o); 
            var credential = GoogleCredential.FromAccessToken("AIzaSyD1JBd2ZEbjCixjmM6mhJjEEpX7t-Ub84g");
                ImageAnnotatorClient client = ImageAnnotatorClient.Create(credential);
                TextAnnotation text = client.DetectDocumentText(image);
                Debug.WriteLine($"Text: {text.Text}");
                foreach (var page in text.Pages) {
                    foreach (var block in page.Blocks) {
                        string box = string.Join(" - ", block.BoundingBox.Vertices.Select(v => $"({v.X}, {v.Y})"));
                        Debug.WriteLine($"Block {block.BlockType} at {box}");
                        foreach (var paragraph in block.Paragraphs) {
                            box = string.Join(" - ", paragraph.BoundingBox.Vertices.Select(v => $"({v.X}, {v.Y})"));
                            Debug.WriteLine($"  Paragraph at {box}");
                            foreach (var word in paragraph.Words) {
                                Debug.WriteLine($"    Word: {string.Join("", word.Symbols.Select(s => s.Text))}");
                            }
                        }
                    }
                }
            }
            */

            return new Result(0);
        }
    }
}
