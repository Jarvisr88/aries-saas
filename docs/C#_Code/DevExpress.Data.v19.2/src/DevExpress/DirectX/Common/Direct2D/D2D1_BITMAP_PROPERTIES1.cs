namespace DevExpress.DirectX.Common.Direct2D
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct D2D1_BITMAP_PROPERTIES1
    {
        private static readonly D2D1_BITMAP_PROPERTIES1 defaultProperties;
        private readonly D2D1_PIXEL_FORMAT pixelFormat;
        private readonly float dpiX;
        private readonly float dpiY;
        private readonly D2D1_BITMAP_OPTIONS bitmapOptions;
        private readonly IntPtr colorContextPointer;
        public static D2D1_BITMAP_PROPERTIES1 Default =>
            defaultProperties;
        public D2D1_BITMAP_PROPERTIES1(D2D1_PIXEL_FORMAT pixelFormat, float dpiX, float dpiY, D2D1_BITMAP_OPTIONS bitmapOptions, IntPtr colorContextPointer)
        {
            this.pixelFormat = pixelFormat;
            this.dpiX = dpiX;
            this.dpiY = dpiY;
            this.bitmapOptions = bitmapOptions;
            this.colorContextPointer = colorContextPointer;
        }

        static D2D1_BITMAP_PROPERTIES1()
        {
            defaultProperties = new D2D1_BITMAP_PROPERTIES1(D2D1_PIXEL_FORMAT.Default, 0f, 0f, D2D1_BITMAP_OPTIONS.Target, IntPtr.Zero);
        }
    }
}

