using NAudio.CoreAudioApi;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sikusiSubtitles.SpeechRecognition {
    public partial class AmiVoiceSpeechRecognitionPage : SettingPage {
        WasapiCapture? MicCapture;
        com.amivoice.wrp.Wrp? Wrp;
        WrpListener? Listener;

        public event EventHandler<SpeechRecognitionEventArgs>? Recognizing;
        public event EventHandler<SpeechRecognitionEventArgs>? Recognized;

        public AmiVoiceSpeechRecognitionPage() {
            InitializeComponent();
        }

        public override void Unload() {
            try {
                this.StopRecording();
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message);
            }
        }

        public override void LoadSettings() {
            this.keyTextBox.Text = this.Decrypt(Properties.Settings.Default.AmiVoiceKey);

            this.engineComboBox.SelectedIndex = 0;
            for (int i = 0; i < this.engines.Length; i++) {
                if (this.engines[i].Item1 == Properties.Settings.Default.AmiVoiceEngine) {
                    this.engineComboBox.SelectedIndex = i;
                }
            }

            this.logComboBox.SelectedIndex = 0;
            for (int i = 0; i < this.logs.Length; i++) {
                if (this.logs[i].Item1 == Properties.Settings.Default.AmiVoiceLog) {
                    this.logComboBox.SelectedIndex = i;
                }
            }
        }

        public override void SaveSettings() {
            Properties.Settings.Default.AmiVoiceKey = this.Encrypt(this.keyTextBox.Text);

            int engineIndex = this.engineComboBox.SelectedIndex > 0 ? this.engineComboBox.SelectedIndex : 0;
            Properties.Settings.Default.AmiVoiceEngine = this.engines[engineIndex].Item1;

            int logIndex = this.logComboBox.SelectedIndex > 0 ? this.logComboBox.SelectedIndex : 0;
            Properties.Settings.Default.AmiVoiceLog= this.logs[logIndex].Item1;
        }

        public bool SpeechRecognitionStart(MMDevice device) {
            try {
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

        public void SpeechRecognitionStop() {
            try {
                this.StopRecording();
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message);
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
                }
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message);
            }
        }

        private bool ListenerStart() {
            if (this.keyTextBox.Text == "") {
                MessageBox.Show("APIキーが設定されていません。", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            } else if (this.engineComboBox.SelectedIndex == -1) {
                MessageBox.Show("エンジンが設定されていません。", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            } else if (this.logComboBox.SelectedIndex == -1) {
                MessageBox.Show("ログの有無が設定されていません。", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            Listener = new WrpListener();
            Listener.Recognizing += RecognizingHandler;
            Listener.Recognized += RecognizedHandler;

            // WebSocket 音声認識サーバの初期化
            var serverURL = "wss://acp-api.amivoice.com/v1/";
            if (this.logs[this.logComboBox.SelectedIndex].Item1 == "nolog")
                serverURL += "nolog/";
            var codec = this.engines[this.engineComboBox.SelectedIndex].Item1;
            this.Wrp = com.amivoice.wrp.Wrp.construct();
            this.Wrp.setListener(Listener);
            this.Wrp.setServerURL(serverURL);
            this.Wrp.setCodec("lsb22k");
            this.Wrp.setGrammarFileNames(codec);
            this.Wrp.setAuthorization(this.keyTextBox.Text);

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
                using var inputStream = new RawSourceWaveStream(memStream, MicCapture?.WaveFormat);

                // Convert the input stream to a WaveProvider in 16bit PCM format
                var waveToSampleProvider = new WaveToSampleProvider(inputStream);
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

        private void RecognizingHandler(object? sender, WrpListener.RecognitionResult args) {
            this.Recognizing?.Invoke(this, new SpeechRecognitionEventArgs() { Text = args.text != null ? args.text : "" });
        }

        private void RecognizedHandler(object? sender, WrpListener.RecognitionResult args) {
            this.Recognized?.Invoke(this, new SpeechRecognitionEventArgs() { Text = args.text != null ? args.text : "" });
        }

        private void AmiVoiceSpeechRecognitionPage_Load(object sender, EventArgs e) {
            foreach (var engine in engines)
                this.engineComboBox.Items.Add(engine.Item2);
            foreach (var log in logs)
                this.logComboBox.Items.Add(log.Item2);
        }

        Tuple<string, string>[] logs =  {
            new Tuple<string, string>("log", "あり"),
            new Tuple<string, string>("nolog", "なし"),
        };
        Tuple<string, string>[] engines = {
            new Tuple<string, string>("-a-general-input", "音声入力_汎用"),
            // new Tuple<string, string>("-a-medgeneral-input, "音声入力_医療"),
            // new Tuple<string, string>("-a-bizmrreport-input, "音声入力_製薬"),
            // new Tuple<string, string>("-a-medkarte-input, "音声入力_電子カルテ"),
            // new Tuple<string, string>("-a-bizinsurance-input, "音声入力_保険"),
            // new Tuple<string, string>("-a-bizfinance-input, "音声入力_金融"),
            // new Tuple<string, string>("-a-general, "会話_汎用"),
            // new Tuple<string, string>("-a-medgeneral, "会話_医療"),
            // new Tuple<string, string>("-a-bizmrreport, "会話_製薬"),
            // new Tuple<string, string>("-a-bizfinance, "会話_金融"),
            // new Tuple<string, string>("-a-bizinsurance, "会話_保険"),
            new Tuple<string, string>("-a-general-en", "英語_汎用"),
            new Tuple<string, string>("-a-general-zh", "中国語_汎用"),
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
                public string? rulename{ get; set; }
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
