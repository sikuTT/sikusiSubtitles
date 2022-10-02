﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.SpeechRecognition {
    public class SpeechRecognitionEventArgs {
        public SpeechRecognitionEventArgs() {
            Text = "";
        }
        public SpeechRecognitionEventArgs(string text) {
            Text = text;
        }

        public string Text { get; set;}
    }
}
