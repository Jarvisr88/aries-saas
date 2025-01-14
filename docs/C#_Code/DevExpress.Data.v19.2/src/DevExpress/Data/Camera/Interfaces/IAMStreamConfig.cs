﻿namespace DevExpress.Data.Camera.Interfaces
{
    using DevExpress.Data.Camera;
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    [ComImport, Guid("C6E13340-30AC-11d0-A18C-00A0C9118956"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown), SuppressUnmanagedCodeSecurity]
    internal interface IAMStreamConfig
    {
        [PreserveSig]
        int SetFormat([In, MarshalAs(UnmanagedType.LPStruct)] AMMediaType pmt);
        [PreserveSig]
        int GetFormat(out AMMediaType pmt);
        [PreserveSig]
        int GetNumberOfCapabilities(out int piCount, out int piSize);
        [PreserveSig]
        int GetStreamCaps([In] int iIndex, out AMMediaType ppmt, [In] VideoStreamConfigCaps streamConfigCaps);
    }
}

