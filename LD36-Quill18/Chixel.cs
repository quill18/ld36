using System;

namespace LD36Quill18
{
    public class Chixel
    {
        public Chixel( char glyph, ConsoleColor fg_color = ConsoleColor.White, ConsoleColor bg_color = ConsoleColor.Black )
        {
            this.Glyph = glyph;
            this.ForegroundColor = fg_color;
            this.BackgroundColor = bg_color;
            this.Dirty = true;
        }

        public char Glyph { get; set; }
        public ConsoleColor ForegroundColor { get; set; }
        public ConsoleColor BackgroundColor { get; set; }
        public bool Dirty { get; set; }
    }
}

