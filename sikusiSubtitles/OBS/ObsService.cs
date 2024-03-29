﻿using Newtonsoft.Json.Linq;
using ObsWebSocket5;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace sikusiSubtitles.OBS {
    public class ObsService : sikusiSubtitles.Service {
        // Events
        public event EventHandler<bool>? ConnectionChanged;

        public ObsWebSocket ObsSocket { get; }
        public string IP { get; set; } = "127.0.0.1";
        public int Port { get; set; } = 4455;
        public string Password { get; set; } = "";

        private ToggleButton obsButton;

        public bool IsConnected {
            get { return ObsSocket.IsConnected; }
        }

        public ObsService(ServiceManager serviceManager) : base(serviceManager, ObsServiceManager.ServiceName, "OBS", "OBS", 100) {
            this.ObsSocket = new ObsWebSocket();

            obsButton = new ToggleButton();
            obsButton.Content = "OBS接続";
            obsButton.Width = 70;
            obsButton.Checked += obsButton_Checked;
            obsButton.Unchecked += obsButton_Unchecked;
            serviceManager.AddTopFlowControl(obsButton, 200);

            settingsPage = new ObsPage(ServiceManager, this);
        }

        public override void Load(JToken token) {
            IP = token.Value<string>("IP") ?? IP;
            Port = token.Value<int?>("Port") ?? Port;
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
            try {
                if (IsConnected) {
                    return true;
                }

                if (this.IP == "") {
                    MessageBox.Show("接続先を設定してください。");
                    return false;
                }

                var url = String.Format("ws://{0}:{1}/", IP, Port);
                try {
                    await ObsSocket.ConnectAsync(url , this.Password);
                    ConnectionChanged?.Invoke(this , true);
                    ObsSocket.Closed += ClosedHandler;
                } catch (WebSocketClosedException) {
                    MessageBox.Show("認証に失敗しました。" , null , MessageBoxButton.OK , MessageBoxImage.Error);
                    return false;
                } catch (Exception) {
                    MessageBox.Show("接続できませんでした。接続先を確認してください。" , null , MessageBoxButton.OK , MessageBoxImage.Error);
                    return false;
                }
            } finally {
                obsButton.Dispatcher.Invoke(() => {
                    obsButton.IsChecked = ObsSocket.IsConnected;
                });
            }

            return ObsSocket.IsConnected;
        }

        async public Task DisconnectAsync() {
            obsButton.Dispatcher.Invoke(() => {
                obsButton.IsChecked = false;
            });
            ObsSocket.Closed -= ClosedHandler;
            await ObsSocket.CloseAsync();
            ConnectionChanged?.Invoke(this, false);
        }

        /** OBSへの接続ボタン */
        private async void obsButton_Checked(object? sender, RoutedEventArgs e) {
            await ConnectAsync();
        }

        /** OBSへの接続ボタン */
        private async void obsButton_Unchecked(object? sender, RoutedEventArgs e) {
            await DisconnectAsync();
        }

        private async void ClosedHandler(object? sender, WebSocketCloseCode? code) {
            await DisconnectAsync();
        }
    }
}
