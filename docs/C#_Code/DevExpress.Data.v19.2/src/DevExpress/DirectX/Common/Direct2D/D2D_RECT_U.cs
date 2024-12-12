namespace DevExpress.DirectX.Common.Direct2D
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct D2D_RECT_U
    {
        private readonly int left;
        private readonly int top;
        private readonly int right;
        private readonly int bottom;
        public static implicit operator D2D_RECT_F(D2D_RECT_U rect) => 
            new D2D_RECT_F((float) rect.left, (float) rect.top, (float) rect.right, (float) rect.bottom);

        public int Left =>
            this.left;
        public int Top =>
            this.top;
        public int Right =>
            this.right;
        public int Bottom =>
            this.bottom;
        public int Height =>
            this.bottom - this.top;
        public int Width =>
            this.right - this.left;
        public D2D_RECT_U(int left, int top, int right, int bottom)
        {
            this.left = left;
            this.top = top;
            this.right = right;
            this.bottom = bottom;
        }
    }
}

