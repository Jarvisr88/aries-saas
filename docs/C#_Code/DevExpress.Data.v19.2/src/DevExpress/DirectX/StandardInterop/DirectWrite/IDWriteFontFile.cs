namespace DevExpress.DirectX.StandardInterop.DirectWrite
{
    using DevExpress.DirectX.Common.DirectWrite;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("739d886a-cef5-47dc-8769-1a8b41bebbb0"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDWriteFontFile
    {
        void GetReferenceKey(out IntPtr fontFileReferenceKey, out int fontFileReferenceKeySize);
        IDWriteFontFileLoader GetLoader();
        void Analyze([MarshalAs(UnmanagedType.Bool)] out bool isSupportedFontType, out DWRITE_FONT_FILE_TYPE fontFileType, out DWRITE_FONT_FACE_TYPE fontFaceType, out int numberOfFaces);
    }
}

