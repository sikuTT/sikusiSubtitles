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
        public string Name => name;
        public string? Language => language;
        public string? Gender => gender;
        public string? Age => age;

        public Voice(string type, string id, string name, string? language = null, string? gender = null, string? age = null) {
            this.type = type;
            this.id = id;
            this.name = name;
            this.language = language;
            this.gender = gender;
            this.age = age;
            this.displayName = name;
            if (language != null) {
                displayName += $" ({language})";
            }
        }

        string type;
        string id;
        string displayName;
        string name;
        string? language;
        string? gender;
        string? age;
    }
}
