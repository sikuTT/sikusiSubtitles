using OBSWebsocketDotNet;
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
        public OBSWebsocket ObsSocket { get; }
        public string IP { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }

        public bool IsConnected {
            get { return ObsSocket.IsConnected; }
        }

        public ObsService(ServiceManager serviceManager) : base(serviceManager, SERVICE_NAME, "OBS", "OBS", 100) {
            this.ObsSocket = new OBSWebsocket();
            this.IP = "";
            this.Port = 0;
            this.Password = "";
        }

        public bool Connect() {
            if (this.IP == "") {
                MessageBox.Show("接続先を設定してください。");
                return false;
            }

            var url = "ws://" + this.IP + ":" + this.Port + "/";
            try {
                ObsSocket.Connect(url, this.Password);
            } catch (AuthFailureException) {
                // MessageBox.Show("Authentication failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                MessageBox.Show("認証に失敗しました。");
                return false;
            } catch (ErrorResponseException) {
                // MessageBox.Show("Connect failed : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                MessageBox.Show("接続できませんでした。接続先を確認してください。");
                return false;
            } catch (Exception ex) {
                MessageBox.Show("接続できませんでした。");
                Debug.WriteLine(ex.Message);
                return false;
            }

            // 字幕開始
            SubtitlesStart();

            return ObsSocket.IsConnected;
        }

        public void Disconnect() {
            // 字幕終了
            SubtitlesStop();

            ObsSocket.Disconnect();
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
