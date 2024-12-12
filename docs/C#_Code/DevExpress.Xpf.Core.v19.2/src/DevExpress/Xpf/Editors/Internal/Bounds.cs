namespace DevExpress.Xpf.Editors.Internal
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct Bounds
    {
        private int x;
        private int y;
        private int width;
        private int height;
        public int X =>
            this.x;
        public int Y =>
            this.y;
        public int Width =>
            this.width;
        public int Height =>
            this.height;
        public Bounds(int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }
    }
}

