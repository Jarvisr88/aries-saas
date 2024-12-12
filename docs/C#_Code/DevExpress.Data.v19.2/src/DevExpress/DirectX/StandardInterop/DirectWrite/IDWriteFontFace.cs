namespace DevExpress.DirectX.StandardInterop.DirectWrite
{
    using DevExpress.DirectX.Common.DirectWrite;
    using DevExpress.DirectX.StandardInterop.Direct2D;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("5f49804d-7024-4d43-bfa9-d25984f53849"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDWriteFontFace
    {
        void GetType();
        void GetFiles(ref int numberOfFiles, [Out, MarshalAs(UnmanagedType.LPArray)] IDWriteFontFile[] fontFiles);
        [PreserveSig]
        int GetIndex();
        DWRITE_FONT_SIMULATIONS GetSimulations();
        void IsSymbolFont();
        DWRITE_FONT_METRICS GetMetrics();
        void GetGlyphCount();
        void GetDesignGlyphMetrics([MarshalAs(UnmanagedType.LPArray)] short[] glyphIndices, int glyphCount, [Out, MarshalAs(UnmanagedType.LPArray)] DWRITE_GLYPH_METRICS[] glyphMetrics, [MarshalAs(UnmanagedType.Bool)] bool isSideways);
        void GetGlyphIndices([MarshalAs(UnmanagedType.LPArray)] int[] codePoints, int codePointCount, IntPtr glyphIndices);
        void TryGetFontTable();
        void ReleaseFontTable();
        void GetGlyphRunOutline(float emSize, IntPtr glyphIndices, IntPtr glyphAdvances, IntPtr glyphOffsets, int glyphCount, [MarshalAs(UnmanagedType.Bool)] bool isSideways, [MarshalAs(UnmanagedType.Bool)] bool isRightToLeft, [MarshalAs(UnmanagedType.Interface)] ID2D1SimplifiedGeometrySink geometrySink);
        void GetRecommendedRenderingMode();
        void GetGdiCompatibleMetrics();
        void GetGdiCompatibleGlyphMetrics();
    }
}

