using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sikusiSubtitles.SpeechRecognition {
    public partial class ChromeSpeechRecognitionPage : SettingPage {
        HttpListener? Listener;
        ChromeDriver? ChromeDriver;
        System.Timers.Timer? ChromeTimer;
        String LastText = "";
        bool isLastFinal = false;

        public event EventHandler<SpeechRecognitionEventArgs>? Recognizing;
        public event EventHandler<SpeechRecognitionEventArgs>? Recognized;

        public ChromeSpeechRecognitionPage() {
            InitializeComponent();
        }

        public override void LoadSettings() {
            this.portNumericUpDown.Value = Properties.Settings.Default.ChromePort;
            for (var i = 0; i < this.Languages.Length; i++) {
                if (Properties.Settings.Default.ChromeLanguage == this.Languages[i].Item1) {
                    this.languageComboBox.SelectedIndex = i;
                    break;
                }
            }
        }

        public override void SaveSettings() {
            Properties.Settings.Default.ChromePort = (int)this.portNumericUpDown.Value;
            if (this.languageComboBox.SelectedIndex != -1)
                Properties.Settings.Default.ChromeLanguage = this.Languages[this.languageComboBox.SelectedIndex].Item1;
        }

        public override void Unload() {
            try {
                SpeechRecognitionStop();
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message);
            }
        }
        private void ChromeSpeechRecognitionPage_Load(object sender, EventArgs e) {
            Array.Sort(this.Languages, (a, b) => a.Item2.CompareTo(b.Item2));
            foreach (var language in this.Languages) {
                this.languageComboBox.Items.Add(language.Item2);
            }
        }

        public bool SpeechRecognitionStart() {
            if (this.languageComboBox.SelectedIndex == -1) {
                MessageBox.Show("言語を選択してください。", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            this.LastText = "";
            var prefix = "http://127.0.0.1:" + this.portNumericUpDown.Value + "/";

            if (this.HttpListenerStart(prefix) == false)
                return false;

            if (this.LunchChrome(prefix) == false) {
                this.HttpListenerStop();
                return false;
            }

            return true;
        }

        public void SpeechRecognitionStop() {
            // 手動でChromeを閉じられていると、Chromeが存在しないことが分かるまでかなり時間がかかるので、
            // UIがフリーズしないように別スレッドでクローズ処理をする。
            Task.Run(() => {
                try {
                    this.HttpListenerStop();

                    if (ChromeTimer != null) {
                        ChromeTimer.Close();
                        ChromeTimer.Elapsed -= ChromeObserver;
                        ChromeTimer = null;
                    }

                    // Chromeを終了する。
                    // 終了に時間がかかってる間にもう1回Chromeを起動されるとQuit中にChromeDriverｎインスタンスが変わる可能性があるので、
                    // 変わっていればnullにはしない。
                    var temp = ChromeDriver;
                    if (this.ChromeDriver != null) {
                        ChromeDriver.Quit();
                        if (temp == ChromeDriver) {
                            ChromeDriver = null;
                        }
                    }
                } catch (Exception ex) {
                    Debug.WriteLine(ex.Message);
                }
            });
        }

        private bool HttpListenerStart(string prefix) {
            try {
                Listener = new HttpListener();
                Listener.Prefixes.Add(prefix);
                Listener.Start();
                IAsyncResult result = Listener.BeginGetContext(new AsyncCallback(ListenerCallback), Listener);
            } catch (HttpListenerException ex) {
                Debug.WriteLine(ex.Message);
                MessageBox.Show("WEBサーバーを開始できませんでした。", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        private void HttpListenerStop() {
            if (Listener != null) {
                Listener.Stop();
                Listener.Close();
                Listener = null;
            }
        }

        private void ListenerCallback(IAsyncResult result) {
            Assembly? myAssembly = Assembly.GetEntryAssembly();
            string? path = myAssembly?.Location;
            if (path == null)
                return;

            int index = path.LastIndexOf('\\');
            if (index == -1)
                return;

            path = path.Substring(0, index);

            HttpListener? listener = (HttpListener?)result.AsyncState;
            if (listener == null)
                return;

            try {
                // Call EndGetContext to complete the asynchronous operation.
                HttpListenerContext context = listener.EndGetContext(result);
                HttpListenerRequest request = context.Request;


                path += request.RawUrl?.Replace("/", "\\");
                index = path.IndexOf('?');
                if (index != -1)
                    path = path.Remove(index);

                if (System.IO.Directory.Exists(path)) {
                    path += @"\index.html";
                }
                Debug.WriteLine("request file = " + path);

                // Obtain a response object.
                HttpListenerResponse response = context.Response;

                // Construct a response.
                byte[] buffer;

                if (!System.IO.File.Exists(path)) {
                    Debug.WriteLine("request file not found");
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    string responseString = "<HTML><BODY>File not found.</BODY></HTML>";
                    buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                } else {
                    Debug.WriteLine("request file found");
                    using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read)) {
                        buffer = new byte[stream.Length];
                        stream.Read(buffer, 0, (int)stream.Length);
                    }
                }

                // Get a response stream and write the response to it.
                response.ContentLength64 = buffer.Length;
                System.IO.Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                // You must close the output stream.
                output.Close();
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message);
            } finally {
                if (listener.IsListening) {
                    listener.BeginGetContext(new AsyncCallback(ListenerCallback), Listener);
                }
            }
        }

        private bool LunchChrome(string prefix) {
            try {
                var service = ChromeDriverService.CreateDefaultService();
                service.HideCommandPromptWindow = true;
                var url = prefix + "browser-recognition/index.html?lang=" + this.Languages[this.languageComboBox.SelectedIndex].Item1;

                var options = new ChromeOptions();
                // options.AddArgument($"--app={url}");
                options.AddArgument("--window-size=500,300");
                // options.AddArgument("use-fake-device-for-media-stream");
                // options.AddArgument("use-fake-ui-for-media-stream");
                options.AddArgument("--disable-notifications");
                options.AddArgument("--no-sandbox");
                options.AddExcludedArgument("enable-automation");
                // options.AddAdditionalCapability("useAutomationExtension", false);

                this.ChromeDriver = new ChromeDriver(service, options);
                this.ChromeDriver.Navigate().GoToUrl(url);

                this.ChromeTimer = new System.Timers.Timer(1000);
                this.ChromeTimer.AutoReset = false;
                this.ChromeTimer.Elapsed += ChromeObserver;
                this.ChromeTimer.Start();
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        private void ChromeObserver(Object? sender, System.Timers.ElapsedEventArgs args) {
            if (ChromeDriver == null)
                return;

            try {
                var elem = ChromeDriver.FindElement(By.Id("result"));
                var isFinal = elem.GetAttribute("data-is-final") == "true";
                var text = elem.Text;
                if (this.isLastFinal != isFinal || this.LastText != text) {
                    // 読み取った音声に変化がない場合は送信しないようにしたいので、値を記憶しておく。
                    this.isLastFinal = isFinal;
                    this.LastText = text;
                    Debug.WriteLine("is final = " + isFinal + ", text = " + text);

                    // イベントを送信
                    if (isFinal) {
                        this.Recognized?.Invoke(this, new SpeechRecognitionEventArgs(text));
                    } else {
                        this.Recognizing?.Invoke(this, new SpeechRecognitionEventArgs(text));
                    }
                }
                this.ChromeTimer?.Start();
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message);
            }
        }

        Tuple<string, string>[] Languages = {
            new Tuple<string, string>("ja-JP", "日本語"),
            new Tuple<string, string>("en-US", "英語"),
            new Tuple<string, string>("es-ES", "スペイン語（スペイン）"),
            new Tuple<string, string>("pt-P", "ポルトガル語（ポルトガル）"),
            new Tuple<string, string>("pt-BR", "ポルトガル語（ブラジル）"),
            new Tuple<string, string>("ko-KR", "韓国語"),
            new Tuple<string, string>("zh", "中国語（簡体字、中国本土）"),
            new Tuple<string, string>("cmn-Hant-TW", "中国語（繁体字、台湾）"),
        };
    }
}
