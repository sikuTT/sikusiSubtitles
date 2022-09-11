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
        public string IP { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }

        private SubtitlesService? subtitlesService;

        public bool IsConnected {
            get { return ObsSocket.IsConnected; }
        }

        public ObsService(ServiceManager serviceManager) : base(serviceManager, ObsServiceManager.ServiceName, "OBS", "OBS", 100) {
            SettingPage = new ObsPage(serviceManager, this);

            this.ObsSocket = new ObsWebSocket();
            this.IP = "127.0.0.1";
            this.Port = 4455;
            this.Password = "";
        }

        public override void Load() {
            IP = Properties.Settings.Default.ObsIP;
            Port = Properties.Settings.Default.ObsPort;
            Password = Properties.Settings.Default.ObsPassword;
        }

        public override void Save() {
            Properties.Settings.Default.ObsIP = IP;
            Properties.Settings.Default.ObsPort = Port;
            Properties.Settings.Default.ObsPassword = Password;
        }

        async public Task<bool> ConnectAsync() {
            if (this.IP == "") {
                MessageBox.Show("接続先を設定してください。");
                return false;
            }

            var url = String.Format("ws://{0}:{1}/", IP, Port);
            try {
                await ObsSocket.ConnectAsync(url, this.Password);
            } catch (AuthenticationFailedException) {
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
    }
}
