namespace DevExpress.DirectX.NativeInterop.DXGI
{
    using System;
    using System.Runtime.InteropServices;

    [Guid("cafcb56c-6ac3-4889-bf47-9e23bbd260ec")]
    public class DXGISurface : DXGIDeviceSubObject
    {
        public DXGISurface(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public void GetDesc()
        {
            throw new NotImplementedException();
        }

        public void Map()
        {
            throw new NotImplementedException();
        }

        public void Unmap()
        {
            throw new NotImplementedException();
        }
    }
}

