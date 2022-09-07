using ObsWebSocket5;
using sikusiSubtitles.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.OBS {
    public class ObsService : Service.Service {
        public static string SERVICE_NAME = "OBS";
        public ObsWebSocket ObsSocket { get; }
        public string IP { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }

        public bool IsConnected {
            get { return ObsSocket.IsConnected; }
        }

        public ObsService(ServiceManager serviceManager) : base(serviceManager, SERVICE_NAME, "OBS", "OBS", 100) {
            this.ObsSocket = new ObsWebSocket();
            this.IP = "";
            this.Port = 0;
            this.Password = "";
        }

        async public Task<bool> ConnectAsync() {
            if (this.IP == "") {
                MessageBox.Show("接続先を設定してください。");
                return false;
            }

            var url = "ws://" + this.IP + ":" + this.Port + "/";
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
            var service = this.ServiceManager.GetService<SubtitlesService>();
            if (service != null) {
                service.Start();
            }
        }

        private void SubtitlesStop() {
            var service = this.ServiceManager.GetService<SubtitlesService>();
            if (service != null) {
                service.Stop();
            }
        }
    }
}
