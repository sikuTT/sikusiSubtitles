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
        // Events
        public event EventHandler<bool>? ConnectionChanged;

        public ObsWebSocket ObsSocket { get; }
        public string IP { get; set; } = "127.0.0.1";
        public int Port { get; set; } = 4455;
        public string Password { get; set; } = "";

        private CheckBox obsCheckBox;

        public bool IsConnected {
            get { return ObsSocket.IsConnected; }
        }

        public ObsService(ServiceManager serviceManager) : base(serviceManager, ObsServiceManager.ServiceName, "OBS", "OBS", 100) {
            this.ObsSocket = new ObsWebSocket();

            obsCheckBox = new CheckBox();
            obsCheckBox.Appearance = Appearance.Button;
            obsCheckBox.Text = "OBS接続";
            obsCheckBox.TextAlign = ContentAlignment.MiddleCenter;
            obsCheckBox.Width = 70;
            obsCheckBox.CheckedChanged += obsCheckBox_CheckedChanged;
            serviceManager.AddTopFlowControl(obsCheckBox, 200);
        }

        public override UserControl? GetSettingPage() {
            return new ObsPage(ServiceManager, this);
        }

        public override void Load(JToken token) {
            IP = token.Value<string>("IP") ?? "127.0.0.1";
            Port = token.Value<int?>("Port") ?? 4455;
            Password = Decrypt(token.Value<string>("Password") ?? "");
        }

        public override JObject Save() {
            return new JObject{
                new JProperty("IP", IP),
                new JProperty("Port", Port),
                new JProperty("Password", Encrypt(Password))
            };
        }

        async public Task<bool> ConnectAsync() {
            if (this.IP == "") {
                MessageBox.Show("接続先を設定してください。");
                return false;
            }

            var url = String.Format("ws://{0}:{1}/", IP, Port);
            try {
                await ObsSocket.ConnectAsync(url, this.Password);
                ConnectionChanged?.Invoke(this, true);
                ObsSocket.Closed += ClosedHandler;
            } catch (WebSocketClosedException) {
                MessageBox.Show("認証に失敗しました。", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            } catch (Exception) {
                MessageBox.Show("接続できませんでした。接続先を確認してください。", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return ObsSocket.IsConnected;
        }

        async public Task DisconnectAsync() {
            if (obsCheckBox.InvokeRequired) {
                var act = delegate () {
                    obsCheckBox.Checked = false;
                };
                obsCheckBox.Invoke(act);
            } else {
                obsCheckBox.Checked = false;
            }
            ObsSocket.Closed -= ClosedHandler;
            await ObsSocket.CloseAsync();
            ConnectionChanged?.Invoke(this, false);
        }

        /** OBSへの接続ボタン */
        private async void obsCheckBox_CheckedChanged(object? sender, EventArgs e) {
            this.SetCheckBoxButtonColor(this.obsCheckBox);

            if (this.obsCheckBox.Checked) {
                if (await ConnectAsync() == false) {
                    this.obsCheckBox.Checked = false;
                }
            } else if (IsConnected) {
                await DisconnectAsync();
            }
        }

        private async void ClosedHandler(object? sender, WebSocketCloseCode? code) {
            await DisconnectAsync();
        }
    }
}
