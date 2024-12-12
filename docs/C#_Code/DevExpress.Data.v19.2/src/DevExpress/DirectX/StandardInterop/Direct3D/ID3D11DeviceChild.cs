namespace DevExpress.DirectX.StandardInterop.Direct3D
{
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("1841e5c8-16b0-489b-bcc8-44cfb0d5deae"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ID3D11DeviceChild
    {
        void GetDevice();
        void GetPrivateData();
        void SetPrivateData();
        void SetPrivateDataInterface();
    }
}

