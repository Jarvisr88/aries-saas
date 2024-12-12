namespace DevExpress.DirectX.StandardInterop.DirectWrite
{
    using DevExpress.DirectX.Common.DirectWrite;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("acd16696-8c14-4f5d-877e-fe3fc1d32738"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDWriteFont1
    {
        void GetFontFamily();
        void GetWeight();
        void GetStretch();
        void GetStyle();
        void IsSymbolFont();
        void GetFaceNames();
        void GetInformationalStrings();
        [PreserveSig]
        DWRITE_FONT_SIMULATIONS GetSimulations();
        void GetMetrics();
        void HasCharacter();
        IDWriteFontFace CreateFontFace();
        [PreserveSig]
        void GetMetrics(out DWRITE_FONT_METRICS1 fontMetrics);
        [PreserveSig]
        void GetPanose([MarshalAs(UnmanagedType.LPArray)] byte[] panose);
        [PreserveSig]
        int GetUnicodeRanges(int maxRangeCount, [Out, MarshalAs(UnmanagedType.LPArray)] DWRITE_UNICODE_RANGE[] unicodeRanges, out int actualRangeCount);
        [return: MarshalAs(UnmanagedType.Bool)]
        [PreserveSig]
        bool IsMonospacedFont();
    }
}

