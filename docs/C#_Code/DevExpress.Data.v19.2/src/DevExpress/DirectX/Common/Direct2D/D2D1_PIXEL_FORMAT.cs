namespace DevExpress.DirectX.Common.Direct2D
{
    using DevExpress.DirectX.Common.DXGI;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct D2D1_PIXEL_FORMAT
    {
        public static readonly D2D1_PIXEL_FORMAT Default;
        private readonly DXGI_FORMAT format;
        private readonly D2D1_ALPHA_MODE alphaMode;
        public DXGI_FORMAT Format =>
            this.format;
        public D2D1_ALPHA_MODE AlphaMode =>
            this.alphaMode;
        public D2D1_PIXEL_FORMAT(DXGI_FORMAT format, D2D1_ALPHA_MODE alphaMode)
        {
            this.format = format;
            this.alphaMode = alphaMode;
        }

        static D2D1_PIXEL_FORMAT()
        {
            Default = new D2D1_PIXEL_FORMAT(DXGI_FORMAT.B8G8R8A8_UNorm, D2D1_ALPHA_MODE.Premultiplied);
        }
    }
}

