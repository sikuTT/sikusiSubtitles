using NAudio.CoreAudioApi;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace sikusiSubtitles.SpeechRecognition {
    public class AmiVoiceSpeechRecognitionService : SpeechRecognitionService {
        private WasapiCapture? MicCapture;
        private com.amivoice.wrp.Wrp? Wrp;
        private WrpListener? Listener;

        public string Key { get; set; } = "";
        public bool Log { get; set; } = true;

        public override List<Language> GetLanguages() {
            return engines;
        }

        public AmiVoiceSpeechRecognitionService(ServiceManager serviceManager) : base(serviceManager, "AmiVoice", "AmiVoice", 300) {
            settingsPage = new AmiVoiceSpeechRecognitionPage(ServiceManager, this);
        }

        public override void Load(JToken token) {
            Key = Decrypt(token.Value<string>("Key") ?? "");
            Log = token.Value<bool?>("Log") ?? true;
        }

        public override JObject Save() {
            return new JObject {
                new JProperty("Key", Encrypt(Key)),
                new JProperty("Log", Log)
            };
        }

        public override bool Start() {
            try {
                var manager = this.ServiceManager.GetManager<SpeechRecognitionServiceManager>();
                if (manager?.Device == null) {
                    return false;
                }
                MMDevice device = manager.Device;

                if (this.ListenerStart() == false)
                    return false;

                if (this.CaptureStart(device) == false)
                    return false;
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        public override void Stop() {
            try {
                this.StopRecording();
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message);
            }
        }

        private void RecognizingHandler(object? sender, WrpListener.RecognitionResult args) {
            if (args.text != null) {
                this.InvokeRecognizing(args.text);
            }
        }

        private void RecognizedHandler(object? sender, WrpListener.RecognitionResult args) {
            if (args.text != null) {
                this.InvokeRecognized(args.text);
            }
        }

        private void StopRecording() {
            try {
                if (Wrp != null) {
                    if (!Wrp.feedDataPause()) {
                        Console.WriteLine(Wrp.getLastMessage());
                        Console.WriteLine("WebSocket 音声認識サーバへの音声データの送信の完了に失敗しました。");
                    }
                    Wrp.disconnect();
                    Wrp = null;
                }
                if (Listener != null) {
                    Listener.Recognizing -= RecognizingHandler;
                    Listener.Recognized -= RecognizedHandler;
                    Listener = null;
                }
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message);
            }

            try {
                if (MicCapture != null) {
                    if (MicCapture.CaptureState == CaptureState.Starting || MicCapture.CaptureState == CaptureState.Capturing) {
                        MicCapture.StopRecording();
                    }
                    MicCapture.DataAvailable -= MicDataAvailable;
                    MicCapture = null;
                }
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message);
            }
        }

        private bool ListenerStart() {
            var manager = this.ServiceManager.GetManager<SpeechRecognitionServiceManager>();
            if (this.Key == "") {
                MessageBox.Show("APIキーが設定されていません。", null, MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            } else if (manager == null || manager.Language == "") {
                MessageBox.Show("エンジンが設定されていません。", null, MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            Listener = new WrpListener();
            Listener.Recognizing += RecognizingHandler;
            Listener.Recognized += RecognizedHandler;

            // WebSocket 音声認識サーバの初期化
            Debug.WriteLine(String.Format("AmiVoice 使用エンジン={0}", manager.Language));
            Debug.WriteLine(String.Format("AmiVoice ログ保存={0}）", this.Log));
            var serverURL = "wss://acp-api.amivoice.com/v1/";
            if (this.Log == false)
                serverURL += "nolog/";
            this.Wrp = com.amivoice.wrp.Wrp.construct();
            this.Wrp.setListener(Listener);
            this.Wrp.setServerURL(serverURL);
            this.Wrp.setCodec("lsb22k");
            this.Wrp.setGrammarFileNames(manager.Language);
            this.Wrp.setAuthorization(this.Key);

            // WebSocket 音声認識サーバへの接続
            if (!this.Wrp.connect()) {
                Debug.WriteLine("WebSocket 音声認識サーバ " + serverURL + " への接続に失敗しました。");
                return false;
            }

            if (!Wrp.feedDataResume()) {
                Console.WriteLine(Wrp.getLastMessage());
                Console.WriteLine("WebSocket 音声認識サーバへの音声データの送信の開始に失敗しました。");
                return false;
            }

            return true;
        }

        private bool CaptureStart(MMDevice device) {
            MicCapture = new WasapiCapture(device);
            MicCapture.DataAvailable += MicDataAvailable;
            MicCapture.StartRecording();
            return true;
        }

        private void MicDataAvailable(Object? sender, WaveInEventArgs args) {
            if (args.BytesRecorded == 0) {
                return;
            }

            if (Wrp == null || Wrp.isConnected() == false) {
                return;
            }

            try {
                // Create a WaveStream from the input buffer.
                using var memStream = new MemoryStream(args.Buffer, 0, args.BytesRecorded);
                using var waveStream = new RawSourceWaveStream(memStream, MicCapture?.WaveFormat);

                // Convert the input stream to a WaveProvider in 16bit PCM format
                var waveToSampleProvider = new WaveToSampleProvider(waveStream);
                var stereoToMonoSampleProvider = new StereoToMonoSampleProvider(waveToSampleProvider);
                var resamplingSampleProvider = new WdlResamplingSampleProvider(stereoToMonoSampleProvider, 22500);
                var convertedPCM = new SampleToWaveProvider16(resamplingSampleProvider);

                byte[] convertedBuffer = new byte[args.BytesRecorded];

                // Read the converted WaveProvider into a buffer and turn it into a Stream.
                using var stream = new MemoryStream();
                int read;
                while ((read = convertedPCM.Read(convertedBuffer, 0, args.BytesRecorded)) > 0) {
                    stream.Write(convertedBuffer, 0, read);
                }

                // 微小時間のスリープ
                Wrp.sleep(1);

                // 認識結果情報待機数が 1 以下になるまでスリープ
                int maxSleepTime = 50000;
                while (Wrp.getWaitingResults() > 1 && maxSleepTime > 0) {
                    Wrp.sleep(100);
                    maxSleepTime -= 100;
                }

                bool? res = this.Wrp?.feedData(stream.ToArray(), 0, (int)stream.Length);
                if (res == null || res == false) {
                    this.StopRecording();
                }
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message);
            }
        }

        private List<Language> engines = new List<Language>() {
            new Language("-a-general-input", "音声入力_汎用"),
            // new Language("-a-medgeneral-input", "音声入力_医療"),
            // new Language("-a-bizmrreport-input", "音声入力_製薬"),
            // new Language("-a-medkarte-input", "音声入力_電子カルテ"),
            // new Language("-a-bizinsurance-input", "音声入力_保険"),
            // new Language("-a-bizfinance-input", "音声入力_金融"),
            new Language("-a-general", "会話_汎用"),
            // new Language("-a-medgeneral", "会話_医療"),
            // new Language("-a-bizmrreport", "会話_製薬"),
            // new Language("-a-bizfinance", "会話_金融"),
            // new Language("-a-bizinsurance", "会話_保険"),
            new Language("-a-general-en", "英語_汎用"),
            new Language("-a-general-zh", "中国語_汎用"),
        };
    }

    public class WrpListener : com.amivoice.wrp.WrpListener {
        public class RecognitionResult {
            public List<Result>? results { get; set; }
            public string? utteranceid { get; set; }
            public string? text { get; set; }
            public string? code { get; set; }
            public string? message { get; set; }

            public class Result {
                public List<Token>? tokens { get; set; }
                public decimal? confidence { get; set; }
                public int? starttime { get; set; }
                public int? endtime { get; set; }
                public string? rulename { get; set; }
                public string? text { get; set; }
            }

            public class Token {
                public string? written { get; set; }
                public decimal? confidence { get; set; }
                public int? starttime { get; set; }
                public int? endtime { get; set; }
                public string? spoken { get; set; }
            }
        }

        public event EventHandler<RecognitionResult>? Recognizing;
        public event EventHandler<RecognitionResult>? Recognized;

        public void eventNotified(int eventId, string eventMessage) {
            Debug.WriteLine("eventNotified: " + eventId + ", " + eventMessage);
        }
        public void resultCreated() {
            Debug.WriteLine("resultCreated");
        }

        public void resultFinalized(string result) {
            RecognitionResult? r = this.DeserializeObject(result);
            if (r != null)
                this.Recognized?.Invoke(this, r);
            else
                Debug.WriteLine("deserialize failed");
        }

        public void resultUpdated(string result) {
            RecognitionResult? r = this.DeserializeObject(result);
            if (r != null)
                this.Recognizing?.Invoke(this, r);
            else
                Debug.WriteLine("deserialize failed");
        }

        public void TRACE(string message) {
            Debug.WriteLine("TRACE: " + message);
        }

        public void utteranceEnded(int endTime) {
            Debug.WriteLine("utteranceEnded: " + endTime);
        }

        public void utteranceStarted(int startTime) {
            Debug.WriteLine("utteranceStarted: " + startTime);
        }

        private RecognitionResult? DeserializeObject(string text) {
            RecognitionResult? result = JsonConvert.DeserializeObject<RecognitionResult>(text);
            if (result != null && result.text != null) {
                result.utteranceid = this.text_(result.utteranceid);
                result.text = this.text_(result.text);
                if (result.results != null) {
                    foreach (var r in result.results) {
                        r.text = this.text_(r.text);
                        if (r.tokens != null) {
                            foreach (var t in r.tokens) {
                                t.written = this.text_(t.written);
                                t.spoken = this.text_(t.spoken);
                            }
                        }
                    }
                }
                Debug.WriteLine(result.text);
            }
            return result;
        }

        private string? text_(string? result) {
            if (result == null) {
                return null;
            }
            int index = 0;
            int resultLength = result.Length;
            StringBuilder buffer = new StringBuilder();
            int c = (index >= resultLength) ? 0 : result[index++];
            while (c != 0) {
                if (c == '"') {
                    break;
                }
                if (c == '\\') {
                    c = (index >= resultLength) ? 0 : result[index++];
                    if (c == 0) {
                        return null;
                    }
                    if (c == '"' || c == '\\' || c == '/') {
                        buffer.Append((char)c);
                    } else
                    if (c == 'b' || c == 'f' || c == 'n' || c == 'r' || c == 't') {
                    } else
                    if (c == 'u') {
                        int c0 = (index >= resultLength) ? 0 : result[index++];
                        int c1 = (index >= resultLength) ? 0 : result[index++];
                        int c2 = (index >= resultLength) ? 0 : result[index++];
                        int c3 = (index >= resultLength) ? 0 : result[index++];
                        if (c0 >= '0' && c0 <= '9') { c0 -= '0'; } else if (c0 >= 'A' && c0 <= 'F') { c0 -= 'A' - 10; } else if (c0 >= 'a' && c0 <= 'f') { c0 -= 'a' - 10; } else { c0 = -1; }
                        if (c1 >= '0' && c1 <= '9') { c1 -= '0'; } else if (c1 >= 'A' && c1 <= 'F') { c1 -= 'A' - 10; } else if (c1 >= 'a' && c1 <= 'f') { c1 -= 'a' - 10; } else { c1 = -1; }
                        if (c2 >= '0' && c2 <= '9') { c2 -= '0'; } else if (c2 >= 'A' && c2 <= 'F') { c2 -= 'A' - 10; } else if (c2 >= 'a' && c2 <= 'f') { c2 -= 'a' - 10; } else { c2 = -1; }
                        if (c3 >= '0' && c3 <= '9') { c3 -= '0'; } else if (c3 >= 'A' && c3 <= 'F') { c3 -= 'A' - 10; } else if (c3 >= 'a' && c3 <= 'f') { c3 -= 'a' - 10; } else { c3 = -1; }
                        if (c0 == -1 || c1 == -1 || c2 == -1 || c3 == -1) {
                            return null;
                        }
                        buffer.Append((char)((c0 << 12) | (c1 << 8) | (c2 << 4) | c3));
                    } else {
                        return null;
                    }
                } else {
                    buffer.Append((char)c);
                }
                c = (index >= resultLength) ? 0 : result[index++];
            }
            return buffer.ToString();
        }
    }
}