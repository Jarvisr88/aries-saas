namespace DevExpress.DirectX.Common.Direct2D
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct D2D_SIZE_U
    {
        private readonly int width;
        private readonly int height;
        public int Width =>
            this.width;
        public int Height =>
            this.height;
        public D2D_SIZE_U(int width, int height)
        {
            this.width = width;
            this.height = height;
        }
    }
}

