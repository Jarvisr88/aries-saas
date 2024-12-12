namespace DevExpress.DirectX.StandardInterop.DirectWrite
{
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("727cad4e-d6af-4c9e-8a08-d695b11caa49"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDWriteFontFileLoader
    {
        [PreserveSig]
        int CreateStreamFromKey(IntPtr fontFileReferenceKey, int fontFileReferenceKeySize, out IDWriteFontFileStream fontFileStream);
    }
}

