namespace DevExpress.Data.Camera.Interfaces
{
    using DevExpress.Data.Camera;
    using System;
    using System.Runtime.InteropServices;
    using System.Runtime.InteropServices.ComTypes;
    using System.Security;

    [ComImport, Guid("36b73882-c2c8-11cf-8b46-00805f6cef60"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown), SuppressUnmanagedCodeSecurity]
    internal interface IFilterGraph2 : IGraphBuilder, IFilterGraph
    {
        [PreserveSig]
        int EnumFilters(out IEnumFilters ppEnum);
        [PreserveSig]
        int AddSourceFilterForMoniker([In] IMoniker pMoniker, [In] IBindCtx pCtx, [In, MarshalAs(UnmanagedType.LPWStr)] string lpcwstrFilterName, out IBaseFilter ppFilter);
        [PreserveSig]
        int ReconnectEx([In] IPin ppin, [In] AMMediaType pmt);
        [PreserveSig]
        int RenderEx([In] IPin pPinOut, [In] AMRenderExFlags dwFlags, [In] IntPtr pvContext);
    }
}

