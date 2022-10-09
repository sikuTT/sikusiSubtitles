using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.Speech {
    public class Voice {
        public string Type => type;
        public string Id => id;
        public string DisplayName => displayName;
        public string? Language => language;
        public string? Gender => gender;
        public string? Age => age;

        public Voice(string type, string id, string displayName, string? language = null, string? gender = null, string? age = null) {
            this.type = type;
            this.id = id;
            this.displayName = displayName;
            this.language = language;
            this.gender = gender;
            this.age = age;
        }

        string type;
        string id;
        string displayName;
        string? language;
        string? gender;
        string? age;
    }
}
