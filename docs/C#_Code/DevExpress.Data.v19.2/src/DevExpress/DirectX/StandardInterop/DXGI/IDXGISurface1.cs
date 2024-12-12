namespace DevExpress.DirectX.StandardInterop.DXGI
{
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("4AE63092-6327-4c1b-80AE-BFE12EA32B86"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDXGISurface1
    {
        void SetPrivateData(Guid Name, int DataSize, [MarshalAs(UnmanagedType.LPArray)] byte[] pData);
        void SetPrivateDataInterface(Guid Name, [MarshalAs(UnmanagedType.IUnknown)] object pUnknown);
        void GetPrivateData(Guid Name, [In, Out] ref int pDataSize, out byte[] pData);
        void GetParent(Guid riid, [MarshalAs(UnmanagedType.LPArray)] out IntPtr ppParent);
        void GetDevice(Guid riid, out IntPtr ppDevice);
        void GetDesc();
        [PreserveSig]
        int Map(out IntPtr pLockedRect, int MapFlags);
        void Unmap();
        void GedDC();
        void ReleaseDC();
    }
}

