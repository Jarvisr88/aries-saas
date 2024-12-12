namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    internal class LayoutDependentGlyphRunProperties
    {
        public double fontRenderingSize;
        public ushort[] glyphIndices;
        public double[] advanceWidths;
        public Point[] glyphOffsets;
        public bool sideways;
        public int bidiLevel;
        public GlyphTypeface glyphTypeface;
        public string unicodeString;

        public GlyphRun CreateGlyphRun(Point origin);
    }
}

