namespace DevExpress.DirectX.StandardInterop.DXGI
{
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("3d3e0379-f9de-4d58-bb6c-18d62992f1a6"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDXGIDeviceSubObject
    {
        void SetPrivateData(Guid Name, int DataSize, [MarshalAs(UnmanagedType.LPArray)] byte[] pData);
        void SetPrivateDataInterface(Guid Name, [MarshalAs(UnmanagedType.IUnknown)] object pUnknown);
        void GetPrivateData(Guid Name, [In, Out] ref int pDataSize, out byte[] pData);
        void GetParent(Guid riid, [MarshalAs(UnmanagedType.LPArray)] out IntPtr ppParent);
        void GetDevice(Guid riid, out IntPtr ppDevice);
    }
}

