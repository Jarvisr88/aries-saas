namespace DevExpress.DirectX.Common.WIC
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct WICRect
    {
        private readonly int x;
        private readonly int y;
        private readonly int width;
        private readonly int height;
        public int X =>
            this.x;
        public int Y =>
            this.y;
        public int Width =>
            this.width;
        public int Height =>
            this.height;
        public WICRect(int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }
    }
}

