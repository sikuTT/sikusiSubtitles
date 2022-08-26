using OBSWebsocketDotNet;
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

namespace sikusiSubtitles {
    public partial class ObsPage : SettingPage {
        class SubtitlesText {
            public SubtitlesText(string text, bool recognized) {
                Text = text;
                Recognized = recognized;
            }
            public string Text { get; set; }
            public bool Recognized { get; set; }
        }
        OBSWebsocket ObsSocket = new OBSWebsocket();
        Dictionary<string, List<System.Timers.Timer>> TimerDic = new Dictionary<string, List<System.Timers.Timer>>();
        Dictionary<string, List<SubtitlesText>> SubtitlesList = new Dictionary<string, List<SubtitlesText>>();

        public bool IsConnected {
            get { return ObsSocket.IsConnected; }
        }

        public ObsPage() {
            InitializeComponent();
        }
        public override void LoadSettings() {
            this.ipTextBox.Text = Properties.Settings.Default.ObsIP;
            this.portNumericUpDown.Value = Properties.Settings.Default.ObsPort;
            this.passwordTextBox.Text = Properties.Settings.Default.ObsPassword;
        }
        public override void SaveSettings() {
            Properties.Settings.Default.ObsIP = this.ipTextBox.Text;
            Properties.Settings.Default.ObsPort = (int)this.portNumericUpDown.Value;
            Properties.Settings.Default.ObsPassword = this.passwordTextBox.Text;
        }

        public bool Connect() {
            if (this.ipTextBox.Text == "") {
                MessageBox.Show("接続先を設定してください。");
                return false;
            }

            var url = "ws://" + this.ipTextBox.Text + ":" + this.portNumericUpDown.Value.ToString() + "/";
            try {
                ObsSocket.Connect(url, this.passwordTextBox.Text);
            } catch (AuthFailureException) {
                // MessageBox.Show("Authentication failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                MessageBox.Show("認証に失敗しました。");
                return false;
            } catch (ErrorResponseException) {
                // MessageBox.Show("Connect failed : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                MessageBox.Show("接続できませんでした。接続先を確認してください。");
                return false;
            }

            this.SubtitlesList.Clear();
            this.TimerDic.Clear();

            return ObsSocket.IsConnected;
        }

        public void Disconnect() {
            ObsSocket.Disconnect();
        }

        public void SetSubtitles(string text, string target, bool recognized, int? timeout = null, int? additionalTimeout = null) {
            if (this.IsConnected == false) {
                return;
            }

            try {
                // 字幕を追加もしくは既存字幕を更新
                if (this.SubtitlesList.ContainsKey(target) == false) {
                    this.SubtitlesList.Add(target, new List<SubtitlesText>());
                }
                var subtitlesText = this.SubtitlesList[target];
                if (subtitlesText.Count == 0 || (timeout != null && subtitlesText.Last().Recognized == true)) {
                    subtitlesText.Add(new SubtitlesText(text, recognized));
                    if (subtitlesText.Count > 1) {
                        Debug.WriteLine("" + subtitlesText.Count);
                    }
                } else {
                    subtitlesText.Last().Text = text;
                    subtitlesText.Last().Recognized = recognized;
                }

                // 字幕を表示
                this.UpdateGDIPlusText(target);

                // 字幕削除のタイマーを作成する。
                if (timeout != null && recognized) {
                    double timeoutms = (double)timeout * 1000;
                    if (additionalTimeout != null) {
                        timeoutms += ((double)additionalTimeout / 100 * 1000) * text.Length;
                    }
                    var timer = new System.Timers.Timer(timeoutms);
                    timer.AutoReset = false;
                    timer.Elapsed += UpdateSubtitlesTimer;
                    timer.Start();
                    if (this.TimerDic.ContainsKey(target) == false) {
                        this.TimerDic.Add(target, new List<System.Timers.Timer>());
                    }
                    TimerDic[target].Add(timer);
                }
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message);
            }

        }

        private void UpdateSubtitlesTimer(Object? sender, System.Timers.ElapsedEventArgs args) {
            try {
                var keys = this.TimerDic.Keys;
                foreach (var key in keys) {
                    var timer = this.TimerDic[key].Find(timer => timer == sender);
                    if (timer != null) {
                        this.SubtitlesList[key].Remove(this.SubtitlesList[key][0]);
                        this.TimerDic[key].Remove(timer);
                        this.UpdateGDIPlusText(key);
                        return;
                    }
                }
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message);
            }
        }

        private void UpdateGDIPlusText(string target) {
            try {
                string text = this.CreateSubtitlesText(target);
                if (ObsSocket.IsConnected) {
                    var prop = ObsSocket.GetTextGDIPlusProperties(target);
                    prop.Text = text;
                    ObsSocket.SetTextGDIPlusProperties(prop);
                }
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message);
            }
        }

        private string CreateSubtitlesText(string key) {
            var subtitlesList = this.SubtitlesList[key];
            Debug.WriteLine("subtitlesList.Count = " + subtitlesList.Count);
            string text = "";
            foreach (var subtitle in subtitlesList) {
                text += subtitle.Text;
            }
            return text;
        }
    }
}
