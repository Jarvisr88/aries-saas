namespace DevExpress.DirectX.StandardInterop.DXGI
{
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("aec22fb8-76f3-4639-9be0-28eb43a67a2e"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDXGIObject
    {
        void SetPrivateData(Guid Name, int DataSize, [MarshalAs(UnmanagedType.LPArray)] byte[] pData);
        void SetPrivateDataInterface(Guid Name, [MarshalAs(UnmanagedType.IUnknown)] object pUnknown);
        void GetPrivateData(Guid Name, [In, Out] ref int pDataSize, out byte[] pData);
        void GetParent(Guid riid, [MarshalAs(UnmanagedType.LPArray)] out IntPtr ppParent);
    }
}

