namespace DevExpress.DirectX.NativeInterop
{
    using DevExpress.DirectX.Common.Direct2D;
    using DevExpress.DirectX.NativeInterop.Direct2D;
    using DevExpress.DirectX.NativeInterop.DXGI;
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    [SecuritySafeCritical]
    public static class D2D1NativeInterop
    {
        private const string IID_ID2D1Factory1 = "bb12d362-daee-4b9a-aa1d-14ba401cfa1f";

        public static D2D1DeviceContext CreateDeviceContext(DXGISurface dxgiSurface, ref D2D1_CREATION_PROPERTIES creationProperties)
        {
            IntPtr ptr;
            D2D1CreateDeviceContext(dxgiSurface.NativeObject, ref creationProperties, out ptr);
            return new D2D1DeviceContext(ptr);
        }

        public static D2D1Factory1 CreateFactory()
        {
            IntPtr ptr;
            D2D1_FACTORY_OPTIONS pFactoryOptions = new D2D1_FACTORY_OPTIONS(D2D1_DEBUG_LEVEL.None);
            D2D1CreateFactory(0, new Guid("bb12d362-daee-4b9a-aa1d-14ba401cfa1f"), ref pFactoryOptions, out ptr);
            return new D2D1Factory1(ptr);
        }

        [DllImport("d2d1.dll")]
        private static extern uint D2D1CreateDevice(IntPtr dxgiDevice, IntPtr creationProperties, out IntPtr d2dDevice);
        [DllImport("d2d1.dll", CallingConvention=CallingConvention.StdCall)]
        private static extern int D2D1CreateDeviceContext(IntPtr dxgiSurface, ref D2D1_CREATION_PROPERTIES creationProperties, out IntPtr d2dDeviceContext);
        [DllImport("d2d1.dll")]
        private static extern uint D2D1CreateFactory(uint factoryType, [In, MarshalAs(UnmanagedType.LPStruct)] Guid riid, ref D2D1_FACTORY_OPTIONS pFactoryOptions, out IntPtr ppIFactory);
    }
}

