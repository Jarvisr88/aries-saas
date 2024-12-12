namespace DevExpress.DirectX.StandardInterop.DirectWrite
{
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("6d4865fe-0ab8-4d91-8f62-5dd6be34a3e0"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDWriteFontFileStream
    {
        [PreserveSig]
        int ReadFileFragment(out IntPtr fragmentStart, long fileOffset, long fragmentSize, out IntPtr fragmentContext);
        [PreserveSig]
        int ReleaseFileFragment(IntPtr fragmentContext);
        [PreserveSig]
        int GetFileSize(out long fileSize);
        [PreserveSig]
        int GetLastWriteTime(out long lastWriteTime);
    }
}

