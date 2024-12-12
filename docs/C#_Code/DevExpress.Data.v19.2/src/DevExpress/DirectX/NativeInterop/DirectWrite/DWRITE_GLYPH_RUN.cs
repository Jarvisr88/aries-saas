namespace DevExpress.DirectX.NativeInterop.DirectWrite
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct DWRITE_GLYPH_RUN
    {
        private readonly DWriteFontFace fontFace;
        private readonly float fontEmSize;
        private readonly short[] glyphIndices;
        private readonly float[] glyphAdvances;
        public DWriteFontFace FontFace =>
            this.fontFace;
        public float FontEmSize =>
            this.fontEmSize;
        public short[] GlyphIndices =>
            this.glyphIndices;
        public float[] GlyphAdvances =>
            this.glyphAdvances;
        public DWRITE_GLYPH_RUN(DWriteFontFace fontFace, float fontEmSize, short[] glyphIndices, float[] glyphAdvances)
        {
            this.fontFace = fontFace;
            this.fontEmSize = fontEmSize;
            this.glyphIndices = glyphIndices;
            this.glyphAdvances = glyphAdvances;
        }
    }
}

