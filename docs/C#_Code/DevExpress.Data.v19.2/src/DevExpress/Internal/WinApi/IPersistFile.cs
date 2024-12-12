namespace DevExpress.Internal.WinApi
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    [ComImport, Guid("0000010b-0000-0000-C000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IPersistFile
    {
        uint GetCurFile([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile);
        uint IsDirty();
        uint Load([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, [MarshalAs(UnmanagedType.U4)] STGM dwMode);
        uint Save([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, bool fRemember);
        uint SaveCompleted([MarshalAs(UnmanagedType.LPWStr)] string pszFileName);
    }
}

