namespace DevExpress.DirectX.StandardInterop.DirectWrite
{
    using DevExpress.DirectX.Common.DirectWrite;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("acd16696-8c14-4f5d-877e-fe3fc1d32737"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDWriteFont
    {
        IDWriteFontFamily GetFontFamily();
        [PreserveSig]
        DWRITE_FONT_WEIGHT GetWeight();
        [PreserveSig]
        DWRITE_FONT_STRETCH GetStretch();
        [PreserveSig]
        DWRITE_FONT_STYLE GetStyle();
        [return: MarshalAs(UnmanagedType.Bool)]
        [PreserveSig]
        bool IsSymbolFont();
        void GetFaceNames();
        void GetInformationalStrings();
        [PreserveSig]
        DWRITE_FONT_SIMULATIONS GetSimulations();
        void GetMetrics();
        void HasCharacter();
        IDWriteFontFace CreateFontFace();
    }
}

