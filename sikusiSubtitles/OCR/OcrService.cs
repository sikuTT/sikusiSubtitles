﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.OCR {
    public abstract class OcrService : sikusiSubtitles.Service {
        public OcrService(ServiceManager serviceManager, string name, string displayName, int index) : base(serviceManager, OcrServiceManager.ServiceName, name, displayName, index) {
        }

        public abstract List<Language> GetLanguages();

        public abstract Task<OcrResult> ExecuteAsync(Bitmap bitmap, string language);

        protected string ConcatString(string text1, string text2) {
            if (text1.Length > 0) {
                char c = text1.Last();
                if (Char.IsAscii(c) && !Char.IsWhiteSpace(c) && c != '-') {
                    text1 += ' ';
                }
            }
            text1 += text2;
            return text1;
        }

        protected string RemoveLineBreak(string text) {
            text = text.Replace("\r\n", " ").Replace("\r", " ").Replace("\n", " ").Trim();
            return text;
        }
    }
}