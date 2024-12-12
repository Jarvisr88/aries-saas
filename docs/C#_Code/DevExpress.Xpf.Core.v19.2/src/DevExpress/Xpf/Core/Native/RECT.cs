namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct RECT
    {
        private int _left;
        private int _top;
        private int _right;
        private int _bottom;
        public void Offset(int dx, int dy);
        public int Left { get; set; }
        public int Right { get; set; }
        public int Top { get; set; }
        public int Bottom { get; set; }
        public int Width { get; }
        public int Height { get; }
        public POINT Position { get; }
        public SIZE Size { get; }
        public static RECT Union(RECT rect1, RECT rect2);
        public override bool Equals(object obj);
        public override int GetHashCode();
    }
}

