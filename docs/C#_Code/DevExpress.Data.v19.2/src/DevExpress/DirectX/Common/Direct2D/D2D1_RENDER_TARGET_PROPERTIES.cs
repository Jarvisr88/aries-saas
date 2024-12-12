namespace DevExpress.DirectX.Common.Direct2D
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct D2D1_RENDER_TARGET_PROPERTIES
    {
        private readonly D2D1_RENDER_TARGET_TYPE type;
        private readonly D2D1_PIXEL_FORMAT pixelFormat;
        private readonly float dpiX;
        private readonly float dpiY;
        private readonly D2D1_RENDER_TARGET_USAGE usage;
        private readonly D2D1_FEATURE_LEVEL minLevel;
        public D2D1_RENDER_TARGET_TYPE Type =>
            this.type;
        public D2D1_PIXEL_FORMAT PixelFormat =>
            this.pixelFormat;
        public float DpiX =>
            this.dpiX;
        public float DpiY =>
            this.dpiY;
        public D2D1_RENDER_TARGET_USAGE Usage =>
            this.usage;
        public D2D1_FEATURE_LEVEL MinLevel =>
            this.minLevel;
        public D2D1_RENDER_TARGET_PROPERTIES(D2D1_RENDER_TARGET_TYPE type, D2D1_PIXEL_FORMAT pixelFormat, float dpiX, float dpiY, D2D1_RENDER_TARGET_USAGE usage, D2D1_FEATURE_LEVEL minLevel)
        {
            this.type = type;
            this.pixelFormat = pixelFormat;
            this.dpiX = dpiX;
            this.dpiY = dpiY;
            this.usage = usage;
            this.minLevel = minLevel;
        }
    }
}

