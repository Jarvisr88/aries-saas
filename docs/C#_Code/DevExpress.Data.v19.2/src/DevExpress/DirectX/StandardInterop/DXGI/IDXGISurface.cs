namespace DevExpress.DirectX.StandardInterop.DXGI
{
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("cafcb56c-6ac3-4889-bf47-9e23bbd260ec"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDXGISurface
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
    }
}

