namespace DevExpress.DirectX.NativeInterop.DirectWrite
{
    using DevExpress.DirectX.Common;
    using DevExpress.DirectX.Common.DirectWrite;
    using DevExpress.DirectX.NativeInterop;
    using System;

    internal class DWriteGlyphRunMarshaler : IDisposable
    {
        private readonly ArrayMarshaler glyphAdvances;
        private readonly ArrayMarshaler glyphIndices;
        private readonly DWRITE_GLYPH_RUN_COMMON glyphRunInternal;

        internal DWriteGlyphRunMarshaler(DWRITE_GLYPH_RUN glyphRun)
        {
            this.glyphIndices = new ArrayMarshaler(glyphRun.GlyphIndices);
            this.glyphAdvances = new ArrayMarshaler(glyphRun.GlyphAdvances);
            this.glyphRunInternal = new DWRITE_GLYPH_RUN_COMMON(glyphRun.FontFace.ToNativeObject(), glyphRun.FontEmSize, glyphRun.GlyphIndices.Length, this.glyphIndices.Pointer, this.glyphAdvances.Pointer, IntPtr.Zero, 0, 0);
        }

        public void Dispose()
        {
            this.glyphIndices.Dispose();
            this.glyphAdvances.Dispose();
        }

        public DWRITE_GLYPH_RUN_COMMON GlyphRun =>
            this.glyphRunInternal;
    }
}

