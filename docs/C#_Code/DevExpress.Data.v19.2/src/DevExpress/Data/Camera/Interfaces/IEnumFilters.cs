namespace DevExpress.Data.Camera.Interfaces
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    [ComImport, Guid("56a86893-0ad4-11ce-b03a-0020af0ba770"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown), SuppressUnmanagedCodeSecurity]
    internal interface IEnumFilters
    {
        [PreserveSig]
        int Next([In] int cFilters, [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex=0)] IBaseFilter[] ppFilter, [In] IntPtr pcFetched);
        [PreserveSig]
        int Skip([In] int cFilters);
        [PreserveSig]
        int Reset();
        [PreserveSig]
        int Clone(out IEnumFilters ppEnum);
    }
}

