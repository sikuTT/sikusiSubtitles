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
        public ObsWebSocket ObsSocket { get; }
        public string IP { get; set; } = "127.0.0.1";
        public int Port { get; set; } = 4455;
        public string Password { get; set; } = "";

        private ObsSubtitlesService? obsSubtitlesService;

        public bool IsConnected {
            get { return ObsSocket.IsConnected; }
        }

        public ObsService(ServiceManager serviceManager) : base(serviceManager, ObsServiceManager.ServiceName, "OBS", "OBS", 100) {
            SettingPage = new ObsPage(serviceManager, this);

            this.ObsSocket = new ObsWebSocket();
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
            obsSubtitlesService = this.ServiceManager.GetService<ObsSubtitlesService>();
            if (obsSubtitlesService != null) {
                obsSubtitlesService.Start(this);
            }
        }

        private void SubtitlesStop() {
            if (obsSubtitlesService != null) {
                obsSubtitlesService.Stop();
                obsSubtitlesService = null;
            }
        }
    }
}
