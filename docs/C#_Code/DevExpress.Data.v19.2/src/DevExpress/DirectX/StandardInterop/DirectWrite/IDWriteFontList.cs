namespace DevExpress.DirectX.StandardInterop.DirectWrite
{
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("1a0d8438-1d97-4ec1-aef9-a2fb86ed6acb"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDWriteFontList
    {
        void GetFontCollection();
        void GetFontCount();
        void GetFont();
    }
}

