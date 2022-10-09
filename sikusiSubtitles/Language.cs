using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles {
    public class Language {
        public string Code { get; set; }
        public string Name { get; set; }
        public string AltCode { get; set; }

        public Language(string code, string name) {
            Code = code;
            Name = name;
            AltCode = code;

        }
        public Language(string code, string name, string altCode) {
            Code = code;
            Name = name;
            AltCode = altCode;
        }
    }
}
