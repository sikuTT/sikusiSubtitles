using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.SpeechRecognition {
    public class SpeechRecognitionEventArgs {
        public SpeechRecognitionEventArgs() {}
        public SpeechRecognitionEventArgs(string text, bool recognized) {
            Text = text;
            Recognized = recognized;
        }

        public string Text { get; set; } = "";
        public bool Recognized { get; set; } = true;
    }
}
