namespace DevExpress.DirectX.NativeInterop.DXGI
{
    using System;
    using System.Runtime.InteropServices;

    [Guid("54ec77fa-1377-44e6-8c32-88fd5f44c84c")]
    public class DXGIDevice : DXGIObject
    {
        public DXGIDevice(IntPtr nativeObject) : base(nativeObject)
        {
        }
    }
}

