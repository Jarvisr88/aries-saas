namespace DevExpress.Data.Camera
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential), ComVisible(false)]
    internal struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }
}

