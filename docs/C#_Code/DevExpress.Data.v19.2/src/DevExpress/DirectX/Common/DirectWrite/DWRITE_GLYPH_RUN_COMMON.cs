namespace DevExpress.DirectX.Common.DirectWrite
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct DWRITE_GLYPH_RUN_COMMON
    {
        private readonly IntPtr fontFace;
        private readonly float fontEmSize;
        private int glyphCount;
        private IntPtr glyphIndices;
        private IntPtr glyphAdvances;
        private IntPtr glyphOffsets;
        private readonly int isSideways;
        private readonly int bidiLevel;
        public IntPtr FontFace =>
            this.fontFace;
        public float FontEmSize =>
            this.fontEmSize;
        public int GlyphCount
        {
            get => 
                this.glyphCount;
            set => 
                this.glyphCount = value;
        }
        public IntPtr GlyphIndices
        {
            get => 
                this.glyphIndices;
            set => 
                this.glyphIndices = value;
        }
        public IntPtr GlyphAdvances
        {
            get => 
                this.glyphAdvances;
            set => 
                this.glyphAdvances = value;
        }
        public IntPtr GlyphOffsets
        {
            get => 
                this.glyphOffsets;
            set => 
                this.glyphOffsets = value;
        }
        public int IsSideways =>
            this.isSideways;
        public int BidiLevel =>
            this.bidiLevel;
        internal DWRITE_GLYPH_RUN_COMMON(IntPtr fontFace, float fontEmSize, int glyphCount, IntPtr glyphIndices, IntPtr glyphAdvances, IntPtr glyphOffsets, int isSideways, int bidiLevel)
        {
            this.fontFace = fontFace;
            this.fontEmSize = fontEmSize;
            this.glyphCount = glyphCount;
            this.glyphIndices = glyphIndices;
            this.glyphAdvances = glyphAdvances;
            this.glyphOffsets = glyphOffsets;
            this.isSideways = isSideways;
            this.bidiLevel = bidiLevel;
        }
    }
}

