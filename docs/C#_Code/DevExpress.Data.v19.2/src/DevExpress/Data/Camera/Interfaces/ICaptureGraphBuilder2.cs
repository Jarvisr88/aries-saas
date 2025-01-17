﻿namespace DevExpress.Data.Camera.Interfaces
{
    using DevExpress.Data.Camera;
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    [ComImport, Guid("93E5A4E0-2D50-11d2-ABFA-00A0C9C6E38D"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown), SuppressUnmanagedCodeSecurity]
    internal interface ICaptureGraphBuilder2
    {
        [PreserveSig]
        int SetFiltergraph([In] IGraphBuilder pfg);
        [PreserveSig]
        int GetFiltergraph(out IGraphBuilder ppfg);
        [PreserveSig]
        int SetOutputFileName([In, MarshalAs(UnmanagedType.LPStruct)] Guid pType, [In, MarshalAs(UnmanagedType.LPWStr)] string lpstrFile, out IBaseFilter ppbf, out IFileSinkFilter ppSink);
        [PreserveSig]
        int FindInterface([In, MarshalAs(UnmanagedType.LPStruct)] DsGuid pCategory, [In, MarshalAs(UnmanagedType.LPStruct)] DsGuid pType, [In] IBaseFilter pbf, [In, MarshalAs(UnmanagedType.LPStruct)] Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppint);
        [PreserveSig]
        int RenderStream([In, MarshalAs(UnmanagedType.LPStruct)] DsGuid PinCategory, [In, MarshalAs(UnmanagedType.LPStruct)] DsGuid MediaType, [In, MarshalAs(UnmanagedType.IUnknown)] object pSource, [In] IBaseFilter pfCompressor, [In] IBaseFilter pfRenderer);
        [PreserveSig]
        int ControlStream([In, MarshalAs(UnmanagedType.LPStruct)] Guid PinCategory, [In, MarshalAs(UnmanagedType.LPStruct)] DsGuid MediaType, [In, MarshalAs(UnmanagedType.Interface)] IBaseFilter pFilter, [In] DsLong pstart, [In] DsLong pstop, [In] short wStartCookie, [In] short wStopCookie);
        [PreserveSig]
        int AllocCapFile([In, MarshalAs(UnmanagedType.LPWStr)] string lpstrFile, [In] long dwlSize);
        [PreserveSig]
        int CopyCaptureFile([In, MarshalAs(UnmanagedType.LPWStr)] string lpwstrOld, [In, MarshalAs(UnmanagedType.LPWStr)] string lpwstrNew, [In, MarshalAs(UnmanagedType.Bool)] bool fAllowEscAbort, [In] IAMCopyCaptureFileProgress pFilter);
        [PreserveSig]
        int FindPin([In, MarshalAs(UnmanagedType.IUnknown)] object pSource, [In] PinDirection pindir, [In, MarshalAs(UnmanagedType.LPStruct)] DsGuid PinCategory, [In, MarshalAs(UnmanagedType.LPStruct)] DsGuid MediaType, [In, MarshalAs(UnmanagedType.Bool)] bool fUnconnected, [In] int ZeroBasedIndex, [MarshalAs(UnmanagedType.Interface)] out IPin ppPin);
    }
}

