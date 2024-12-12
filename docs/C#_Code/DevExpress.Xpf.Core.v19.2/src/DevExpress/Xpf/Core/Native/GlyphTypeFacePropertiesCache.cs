namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Media;

    [CLSCompliant(false)]
    public class GlyphTypeFacePropertiesCache
    {
        private readonly Dictionary<ushort, double> advanceWidthCache;
        private readonly Dictionary<ushort, double> advanceHeightCache;
        private readonly Dictionary<char, ushort> glyphToCharacterCache;
        private readonly GlyphTypeface glyphTypeface;

        public GlyphTypeFacePropertiesCache(GlyphTypeface glyphTypeface);
        public double GetAdvanceWidth(ushort glyphIndex, bool sideways);
        public ushort GetGlyphFromCharacter(char character);
    }
}

