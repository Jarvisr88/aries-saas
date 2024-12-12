namespace DevExpress.DirectX.StandardInterop.DirectWrite
{
    using DevExpress.DirectX.Common.DirectWrite;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("da20d8ef-812a-4c43-9802-62ec4abd7add"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDWriteFontFamily
    {
        void GetFontCollection();
        void GetFontCount();
        void GetFont();
        IDWriteLocalizedStrings GetFamilyNames();
        IDWriteFont1 GetFirstMatchingFont(DWRITE_FONT_WEIGHT weight, DWRITE_FONT_STRETCH stretch, DWRITE_FONT_STYLE style);
        void GetMatchingFonts();
    }
}

