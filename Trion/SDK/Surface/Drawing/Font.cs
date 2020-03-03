using System;

using Trion.SDK.Interfaces;

namespace Trion.SDK.Surface.Drawing
{
    internal class Font
    {
        [Flags]
        public enum FontFlags
        {
            FONTFLAG_NONE,
            FONTFLAG_ITALIC = 0x001,
            FONTFLAG_UNDERLINE = 0x002,
            FONTFLAG_STRIKEOUT = 0x004,
            FONTFLAG_SYMBOL = 0x008,
            FONTFLAG_ANTIALIAS = 0x010,
            FONTFLAG_GAUSSIANBLUR = 0x020,
            FONTFLAG_ROTARY = 0x040,
            FONTFLAG_DROPSHADOW = 0x080,
            FONTFLAG_ADDITIVE = 0x100,
            FONTFLAG_OUTLINE = 0x200,
            FONTFLAG_CUSTOM = 0x400,
            FONTFLAG_BITMAP = 0x800,
        };

        public uint Id { get; set; }
        public string Name { get; set; }
        public uint Size { get; set; }
        public FontFlags Style { get; set; }

        public Font():this("Tahoma",29, FontFlags.FONTFLAG_NONE) { }

        public Font(string Name, uint Size, FontFlags Style)
        {
            this.Name = Name;
            this.Size = Size;
            this.Style = Style;

            Id = Interface.Surface.CreateFont();
            Interface.Surface.SetFontGlyphSet(Id, Name, (int)Size, 0, 0, 0, Style);
        }
    }
}