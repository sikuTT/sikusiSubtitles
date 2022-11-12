using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.OCR.Archive {
    public abstract class Archive {
        public abstract string GetName();

        public static Archive? Create(string name) {
            if (name == "Notion") {
                return new Notion();
            } else {
                return null;
            }
        }
    }
}
