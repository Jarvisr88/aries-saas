namespace DevExpress.DirectX.StandardInterop.DirectWrite
{
    using DevExpress.DirectX.Common.DirectWrite;
    using DevExpress.DirectX.StandardInterop;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class DWriteFontFace : ComObject<IDWriteFontFace>
    {
        protected internal DWriteFontFace(IDWriteFontFace nativeObject) : base(nativeObject)
        {
        }

        public DWRITE_GLYPH_METRICS[] GetDesignGlyphMetrics(short[] glyphIndices, bool isSideways)
        {
            DWRITE_GLYPH_METRICS[] glyphMetrics = new DWRITE_GLYPH_METRICS[glyphIndices.Length];
            base.WrappedObject.GetDesignGlyphMetrics(glyphIndices, glyphIndices.Length, glyphMetrics, isSideways);
            return glyphMetrics;
        }

        public DWriteFontFile[] GetFiles()
        {
            base.WrappedObject.GetFiles(0, null);
            IDWriteFontFile[] fontFiles = new IDWriteFontFile[numberOfFiles];
            base.WrappedObject.GetFiles(ref numberOfFiles, fontFiles);
            Func<IDWriteFontFile, DWriteFontFile> selector = <>c.<>9__2_0;
            if (<>c.<>9__2_0 == null)
            {
                Func<IDWriteFontFile, DWriteFontFile> local1 = <>c.<>9__2_0;
                selector = <>c.<>9__2_0 = f => new DWriteFontFile(f);
            }
            return fontFiles.Select<IDWriteFontFile, DWriteFontFile>(selector).ToArray<DWriteFontFile>();
        }

        public void GetGdiCompatibleGlyphMetrics()
        {
            throw new NotImplementedException();
        }

        public void GetGdiCompatibleMetrics()
        {
            throw new NotImplementedException();
        }

        public void GetGlyphCount()
        {
            throw new NotImplementedException();
        }

        public void GetGlyphIndices(int[] codePoints, int codePointCount, IntPtr glyphIndices)
        {
            base.WrappedObject.GetGlyphIndices(codePoints, codePointCount, glyphIndices);
        }

        public int GetIndex() => 
            base.WrappedObject.GetIndex();

        public DWRITE_FONT_METRICS GetMetrics() => 
            base.WrappedObject.GetMetrics();

        public void GetNativeType()
        {
            throw new NotImplementedException();
        }

        public void GetRecommendedRenderingMode()
        {
            throw new NotImplementedException();
        }

        public DWRITE_FONT_SIMULATIONS GetSimulations() => 
            base.WrappedObject.GetSimulations();

        public void IsSymbolFont()
        {
            throw new NotImplementedException();
        }

        public void ReleaseFontTable()
        {
            throw new NotImplementedException();
        }

        public void TryGetFontTable()
        {
            throw new NotImplementedException();
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DWriteFontFace.<>c <>9 = new DWriteFontFace.<>c();
            public static Func<IDWriteFontFile, DWriteFontFile> <>9__2_0;

            internal DWriteFontFile <GetFiles>b__2_0(IDWriteFontFile f) => 
                new DWriteFontFile(f);
        }
    }
}

