namespace DevExpress.DirectX.StandardInterop.Direct3D
{
    using DevExpress.DirectX.Common.Direct3D;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("6f15aaf2-d208-4e89-9ab4-489535d34f9c"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ID3D11Texture2D
    {
        void GetDevice();
        void GetPrivateData();
        void SetPrivateData();
        void SetPrivateDataInterface();
        void GetType();
        void SetEvictionPriority();
        void GetEvictionPriority();
        D3D11_TEXTURE2D_DESC GetDesc();
    }
}

