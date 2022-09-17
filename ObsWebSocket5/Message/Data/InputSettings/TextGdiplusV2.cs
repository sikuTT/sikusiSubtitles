using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsWebSocket5.Message.Data.InputSettings {
    public class TextGdiplusV2 : InputSettings {
        public string? align;
        public bool? antialiasing;
        public uint? bk_color;
        public int? bk_opacity;
        public int? chatlog_lines;
        public uint? color;
        public int? extents_cx;
        public int? extents_cy;
        public bool? extents_wrap;
        public Font? font;
        public uint? gradient_color;
        public double? gradient_dir;
        public int? gradient_opacity;
        public int? opacity;
        public uint? outline_color;
        public int? outline_opacity;
        public int? outline_size;
        public int? transform;
        public string? valign;
        public string? text;

        public class Font {
            public string? face;
            public int? size;
        }
    }
}
