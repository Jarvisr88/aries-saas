namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Media;

    [CLSCompliant(false)]
    public static class FontCache
    {
        [ThreadStatic]
        private static Dictionary<TypefaceKey, Typeface> typefaceCache;
        [ThreadStatic]
        private static Dictionary<GlyphTypeface, GlyphTypeFacePropertiesCache> advanceCache;

        public static GlyphTypeFacePropertiesCache GetAdvanceCache(GlyphTypeface glyphTypeface);
        public static Typeface GetTypeface(RenderTextBlockMode renderMode, FontFamily family, FontStyle style, FontWeight weight, FontStretch stretch);

        private static Dictionary<TypefaceKey, Typeface> TypefaceCache { get; }

        private static Dictionary<GlyphTypeface, GlyphTypeFacePropertiesCache> AdvanceCache { get; }
    }
}

