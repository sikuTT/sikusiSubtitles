using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace sikusiSubtitles.SpeechRecognition {
    public class EdgeSpeechRecognitionService : BrowserSpeechRecognitionBaseService {
        public EdgeSpeechRecognitionService(ServiceManager serviceManager) : base(serviceManager, "EdgeSpeechRecognition", "Microsoft Edge", 0) {
        }

        protected override string? GetBrowserPath() {
            return Microsoft.Win32.Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\msedge.exe", null, null) as string;
        }
        protected override string GetBrowserArgs(string uri) {
            return $"--app={uri}";
        }
    }
}
