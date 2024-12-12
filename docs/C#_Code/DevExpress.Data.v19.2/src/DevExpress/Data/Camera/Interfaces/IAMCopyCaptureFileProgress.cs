namespace DevExpress.Data.Camera.Interfaces
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    [ComImport, Guid("670d1d20-a068-11d0-b3f0-00aa003761c5"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown), SuppressUnmanagedCodeSecurity]
    internal interface IAMCopyCaptureFileProgress
    {
        [PreserveSig]
        int Progress(int iProgress);
    }
}

