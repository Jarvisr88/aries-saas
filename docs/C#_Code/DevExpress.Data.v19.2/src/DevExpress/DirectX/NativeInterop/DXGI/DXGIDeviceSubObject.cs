namespace DevExpress.DirectX.NativeInterop.DXGI
{
    using System;
    using System.Runtime.InteropServices;

    public class DXGIDeviceSubObject : DXGIObject
    {
        public DXGIDeviceSubObject(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public void GetDevice(Guid riid, out IntPtr ppDevice)
        {
            throw new NotImplementedException();
        }
    }
}

