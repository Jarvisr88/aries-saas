namespace DevExpress.Data.Camera.Interfaces
{
    using DevExpress.Data.Camera;
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    [ComImport, Guid("C6E13360-30AC-11d0-A18C-00A0C9118956"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown), SuppressUnmanagedCodeSecurity]
    internal interface IAMVideoProcAmp
    {
        [PreserveSig]
        int GetRange([In] VideoProcAmpProperty Property, out int pMin, out int pMax, out int pSteppingDelta, out int pDefault, out VideoProcAmpFlags pCapsFlags);
        [PreserveSig]
        int Set([In] VideoProcAmpProperty Property, [In] int lValue, [In] VideoProcAmpFlags Flags);
        [PreserveSig]
        int Get([In] VideoProcAmpProperty Property, out int lValue, out VideoProcAmpFlags Flags);
    }
}

