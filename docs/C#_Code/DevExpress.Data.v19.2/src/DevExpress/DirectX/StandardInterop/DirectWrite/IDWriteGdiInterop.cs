namespace DevExpress.DirectX.StandardInterop.DirectWrite
{
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("1edd9491-9853-4299-898f-6432983b6f3a"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDWriteGdiInterop
    {
        void CreateFontFromLOGFONT();
        void ConvertFontToLOGFONT();
        void ConvertFontFaceToLOGFONT();
        void CreateFontFaceFromHdc(IntPtr hdc, out IDWriteFontFace fontFace);
        void CreateBitmapRenderTarget();
    }
}

