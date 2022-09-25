using Newtonsoft.Json.Linq;
using ObsWebSocket5;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.OBS {
    public class ObsService : sikusiSubtitles.Service {
        public class Properties {
            public string IP = "127.0.0.1";
            public int Port = 4455;
            public string Password = "";
        }

        public ObsWebSocket ObsSocket { get; }
        public string IP { get; set; } = "";
        public int Port { get; set; }
        public string Password { get; set; } = "";

        private CheckBox obsCheckBox;
        private SubtitlesService? subtitlesService;

        public bool IsConnected {
            get { return ObsSocket.IsConnected; }
        }

        public ObsService(ServiceManager serviceManager) : base(serviceManager, ObsServiceManager.ServiceName, "OBS", "OBS", 100) {
            ObsSocket = new ObsWebSocket();

            // OBSに接続ボタン
            obsCheckBox = new CheckBox();
            obsCheckBox.Text = "OBSに接続";
            obsCheckBox.Appearance = Appearance.Button;
            obsCheckBox.CheckedChanged += obsCheckBox_CheckedChanged;
        }

        public override UserControl? GetSettingPage() {
            return new ObsPage(ServiceManager, this);
        }

        public override Control GetTopPanelControl() {
            return obsCheckBox;
        }

        public override void Load(JToken token) {
            var props = token.ToObject<Properties>();
            if (props != null) {
                IP = props.IP;
                Port = props.Port;
                Password = Decrypt(props.Password);
            }
        }

        public override JObject Save() {
            return new JObject(Name, new Properties() {
                IP = IP,
                Port = Port,
                Password = Encrypt(Password),
            });
        }

        async public Task<bool> ConnectAsync() {
            if (this.IP == "") {
                MessageBox.Show("接続先を設定してください。");
                return false;
            }

            var url = String.Format("ws://{0}:{1}/", IP, Port);
            try {
                await ObsSocket.ConnectAsync(url, this.Password);
            } catch (WebSocketClosedException) {
                MessageBox.Show("認証に失敗しました。", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            } catch (Exception) {
                MessageBox.Show("接続できませんでした。接続先を確認してください。", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // 字幕開始
            SubtitlesStart();

            return ObsSocket.IsConnected;
        }

        async public Task DisconnectAsync() {
            // 字幕終了
            SubtitlesStop();

            await ObsSocket.CloseAsync();
        }

        private void SubtitlesStart() {
            subtitlesService = this.ServiceManager.GetService<SubtitlesService>();
            if (subtitlesService  != null) {
                subtitlesService.Start(this);
            }
        }

        private void SubtitlesStop() {
            if (subtitlesService != null) {
                subtitlesService.Stop();
                subtitlesService = null;
            }
        }

        async private void obsCheckBox_CheckedChanged(object? sender, EventArgs e) {
            SetCheckBoxButtonColor(obsCheckBox);

            if (obsCheckBox.Checked) {
                if (await ConnectAsync() == false) {
                    obsCheckBox.Checked = false;
                }
            } else if (IsConnected) {
                await DisconnectAsync();
            }
        }
    }
}
