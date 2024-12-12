namespace DevExpress.DirectX.StandardInterop.DirectWrite
{
    using DevExpress.DirectX.Common.DirectWrite;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("b859ee5a-d838-4b5b-a2e8-1adc7d93db48"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDWriteFactory
    {
        void GetSystemFontCollection(out IDWriteFontCollection collection, bool checkForUpdates);
        IDWriteFontCollection CreateCustomFontCollection([MarshalAs(UnmanagedType.Interface)] IDWriteFontCollectionLoader loader, IntPtr collectionKey, int collectionKeySize);
        void RegisterFontCollectionLoader([MarshalAs(UnmanagedType.Interface)] IDWriteFontCollectionLoader loader);
        void UnregisterFontCollectionLoader([MarshalAs(UnmanagedType.Interface)] IDWriteFontCollectionLoader loader);
        void CreateFontFileReference(string filePath, IntPtr lastWriteTime, out IDWriteFontFile fontFile);
        IDWriteFontFile CreateCustomFontFileReference(IntPtr fontFileReferenceKey, int fontFileReferenceKeySize, [MarshalAs(UnmanagedType.Interface)] IDWriteFontFileLoader fontFileLoader);
        IDWriteFontFace CreateFontFace(DWRITE_FONT_FACE_TYPE fontFaceType, int numberOfFiles, [MarshalAs(UnmanagedType.LPArray)] IDWriteFontFile[] fontFiles, int faceIndex, DWRITE_FONT_SIMULATIONS fontFaceSimulationFlags);
        void CreateRenderingParams();
        void CreateMonitorRenderingParams();
        void CreateCustomRenderingParams();
        void RegisterFontFileLoader([MarshalAs(UnmanagedType.Interface)] IDWriteFontFileLoader fontFileLoader);
        void UnregisterFontFileLoader([MarshalAs(UnmanagedType.Interface)] IDWriteFontFileLoader fontFileLoader);
        IDWriteTextFormat CreateTextFormat(string fontFamilyName, IDWriteFontCollection fontCollection, DWRITE_FONT_WEIGHT weight, DWRITE_FONT_STYLE style, DWRITE_FONT_STRETCH fontStretch, float fontSize, string localeName);
        void CreateTypography();
        IDWriteGdiInterop GetGdiInterop();
        IDWriteTextLayout CreateTextLayout(string str, int stringLength, IDWriteTextFormat textFormat, float maxWidth, float maxHeight);
        void CreateGdiCompatibleTextLayout();
        void CreateEllipsisTrimmingSign();
        IDWriteTextAnalyzer CreateTextAnalyzer();
        void CreateNumberSubstitution();
        void CreateGlyphRunAnalysis();
    }
}

