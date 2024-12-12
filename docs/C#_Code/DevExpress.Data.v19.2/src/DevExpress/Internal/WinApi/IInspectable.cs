namespace DevExpress.Internal.WinApi
{
    using System;
    using System.Runtime.InteropServices;

    [Guid("AF86E2E0-B12D-4c6a-9C5A-D7AA65101E90"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IInspectable
    {
        void GetIids();
        int GetRuntimeClassName([MarshalAs(UnmanagedType.HString)] out string name);
        void GetTrustLevel();
    }
}

