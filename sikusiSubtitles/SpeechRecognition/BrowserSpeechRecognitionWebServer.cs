using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace sikusiSubtitles.SpeechRecognition {
    public class BrowserSpeechRecognitionWebServer {
        int Port;
        HttpListener? Listener;

        public BrowserSpeechRecognitionWebServer(int port) {
            Port = port;
        }

        public bool Start() {
            return HttpListenerStart($"http://127.0.0.1:{Port}/");
        }

        private bool HttpListenerStart(string uri) {
            try {
                Listener = new HttpListener();
                Listener.Prefixes.Add(uri);
                Listener.Start();
                var result = Listener.BeginGetContext(new AsyncCallback(HttpListenerCallback), Listener);
            } catch (HttpListenerException ex) {
                Debug.WriteLine(ex.Message);
                MessageBox.Show("WEBサーバーを開始できませんでした。", null, MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        public void Stop() {
            if (Listener != null) {
                Listener.Stop();
                Listener.Close();
                Listener = null;
            }
        }

        private void HttpListenerCallback(IAsyncResult result) {
            var path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            HttpListener? listener = (HttpListener?)result.AsyncState;
            if (listener == null)
                return;

            try {
                // Call EndGetContext to complete the asynchronous operation.
                HttpListenerContext context = listener.EndGetContext(result);
                HttpListenerRequest request = context.Request;


                path += request.RawUrl?.Replace("/", "\\");
                var index = path.IndexOf('?');
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
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    string responseString = "<HTML><BODY>File not found.</BODY></HTML>";
                    buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                } else {
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

                if (listener.IsListening) {
                    listener.BeginGetContext(new AsyncCallback(HttpListenerCallback), Listener);
                }
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
