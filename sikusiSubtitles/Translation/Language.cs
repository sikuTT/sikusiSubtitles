using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable C8618

namespace sikusiSubtitles.Translation {
    public class Language {
        public Language() {
            Key = "";
            Name = "";
        }

        public Language(string key, string name) {
            Key = key;
            Name = name;
        }

        public string Key { get; set; }
        public string Name { get; set; }
    }
}

#pragma warning restore C8618
