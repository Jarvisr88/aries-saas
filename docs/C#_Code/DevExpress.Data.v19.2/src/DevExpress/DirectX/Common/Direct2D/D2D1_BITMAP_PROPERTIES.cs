namespace DevExpress.DirectX.Common.Direct2D
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct D2D1_BITMAP_PROPERTIES
    {
        private readonly D2D1_PIXEL_FORMAT pixelFormat;
        private readonly float dpiX;
        private readonly float dpiY;
        public D2D1_BITMAP_PROPERTIES(D2D1_PIXEL_FORMAT pixelFormat, float dpiX, float dpiY)
        {
            this.pixelFormat = pixelFormat;
            this.dpiX = dpiX;
            this.dpiY = dpiY;
        }
    }
}

