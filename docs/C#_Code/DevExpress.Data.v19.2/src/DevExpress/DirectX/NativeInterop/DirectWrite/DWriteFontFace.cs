namespace DevExpress.DirectX.NativeInterop.DirectWrite
{
    using DevExpress.DirectX.Common;
    using DevExpress.DirectX.Common.DirectWrite;
    using DevExpress.DirectX.NativeInterop;
    using DevExpress.DirectX.NativeInterop.Direct2D;
    using System;

    public class DWriteFontFace : ComObject
    {
        public DWriteFontFace(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public DWRITE_GLYPH_METRICS[] GetDesignGlyphMetrics(short[] glyphIndices)
        {
            DWRITE_GLYPH_METRICS[] dwrite_glyph_metricsArray = new DWRITE_GLYPH_METRICS[glyphIndices.Length];
            using (ArrayMarshaler marshaler = new ArrayMarshaler(dwrite_glyph_metricsArray))
            {
                using (ArrayMarshaler marshaler2 = new ArrayMarshaler(glyphIndices))
                {
                    InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, marshaler2.Pointer, glyphIndices.Length, marshaler.Pointer, MarshalBool(false), 10));
                }
            }
            return dwrite_glyph_metricsArray;
        }

        public DWRITE_FONT_FACE_TYPE GetFaceType() => 
            (DWRITE_FONT_FACE_TYPE) ComObject.InvokeHelper.CalliInt(base.NativeObject, 3);

        public DWriteFontFile GetFiles(int numberOfFiles)
        {
            IntPtr ptr;
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, numberOfFiles, out ptr, 4));
            return new DWriteFontFile(ptr);
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
            using (ArrayMarshaler marshaler = new ArrayMarshaler(codePoints))
            {
                InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, marshaler.Pointer, codePointCount, glyphIndices, 11));
            }
        }

        public void GetGlyphRunOutline(float emSize, short[] glyphIndices, float[] glyphAdvances, D2D1SimplifiedGeometrySink geometrySink)
        {
            this.GetGlyphRunOutline(emSize, glyphIndices, glyphAdvances, null, glyphIndices.Length, false, false, geometrySink);
        }

        public void GetGlyphRunOutline(float emSize, short[] glyphIndices, float[] glyphAdvances, DWRITE_GLYPH_OFFSET[] glyphOffsets, int glyphCount, bool isSideways, bool isRightToLeft, D2D1SimplifiedGeometrySink geometrySink)
        {
            using (ArrayMarshaler marshaler = new ArrayMarshaler(glyphIndices))
            {
                using (ArrayMarshaler marshaler2 = new ArrayMarshaler(glyphAdvances))
                {
                    using (ArrayMarshaler marshaler3 = new ArrayMarshaler(glyphOffsets))
                    {
                        InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, emSize, marshaler.Pointer, marshaler2.Pointer, marshaler3.Pointer, glyphCount, MarshalBool(isSideways), MarshalBool(isRightToLeft), geometrySink.NativeObject, 14));
                    }
                }
            }
        }

        public int GetIndex() => 
            ComObject.InvokeHelper.CalliInt(base.NativeObject, 5);

        public DWRITE_FONT_METRICS GetMetrics()
        {
            DWRITE_FONT_METRICS dwrite_font_metrics;
            ComObject.InvokeHelper.Calli(base.NativeObject, out dwrite_font_metrics, 8);
            return dwrite_font_metrics;
        }

        public void GetRecommendedRenderingMode()
        {
            throw new NotImplementedException();
        }

        public DWRITE_FONT_SIMULATIONS GetSimulations() => 
            (DWRITE_FONT_SIMULATIONS) ComObject.InvokeHelper.CalliInt(base.NativeObject, 6);

        public void IsSymbolFont()
        {
            throw new NotImplementedException();
        }

        public void ReleaseFontTable(IntPtr tableContext)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, tableContext, 13);
        }

        public void TryGetFontTable()
        {
            throw new NotImplementedException();
        }
    }
}

