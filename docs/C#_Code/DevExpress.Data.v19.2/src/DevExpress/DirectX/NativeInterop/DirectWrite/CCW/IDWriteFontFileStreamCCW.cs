namespace DevExpress.DirectX.NativeInterop.DirectWrite.CCW
{
    using DevExpress.DirectX.NativeInterop.CCW;
    using System;
    using System.Runtime.InteropServices;

    [Guid("6d4865fe-0ab8-4d91-8f62-5dd6be34a3e0")]
    public interface IDWriteFontFileStreamCCW : IUnknownCCW
    {
        [MethodOffset(0)]
        int ReadFileFragment(out IntPtr fragmentStart, long fileOffset, long fragmentSize, out IntPtr fragmentContext);
        [MethodOffset(1)]
        int ReleaseFileFragment(IntPtr fragmentContext);
        [MethodOffset(2)]
        int GetFileSize(out long fileSize);
        [MethodOffset(3)]
        int GetLastWriteTime(out long lastWriteTime);
    }
}

