namespace DevExpress.DirectX.NativeInterop.Direct3D
{
    using DevExpress.DirectX.Common.Direct3D;
    using DevExpress.DirectX.NativeInterop;
    using DevExpress.DirectX.NativeInterop.DXGI;
    using System;

    public class D3D11Texture2D : D3D11Resource
    {
        public D3D11Texture2D(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public DXGIResource AsResource() => 
            new DXGIResource(base.QueryInterface<DXGIResource>());

        public DXGISurface AsSurface() => 
            new DXGISurface(base.QueryInterface<DXGISurface>());

        public D3D11_TEXTURE2D_DESC GetDesc()
        {
            D3D11_TEXTURE2D_DESC dd_textured_desc = new D3D11_TEXTURE2D_DESC();
            ComObject.InvokeHelper.Calli(base.NativeObject, ref dd_textured_desc, 10);
            return dd_textured_desc;
        }
    }
}

