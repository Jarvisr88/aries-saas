namespace DevExpress.DirectX.Common.Direct2D
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct D2D_RECT_F
    {
        private static readonly D2D_RECT_F infinite;
        private readonly float left;
        private readonly float top;
        private readonly float right;
        private readonly float bottom;
        public static D2D_RECT_F Infinite =>
            infinite;
        public float Left =>
            this.left;
        public float Top =>
            this.top;
        public float Right =>
            this.right;
        public float Bottom =>
            this.bottom;
        public D2D_RECT_F(float left, float top, float right, float bottom)
        {
            this.left = left;
            this.top = top;
            this.right = right;
            this.bottom = bottom;
        }

        static D2D_RECT_F()
        {
            infinite = new D2D_RECT_F(float.MinValue, float.MinValue, float.MaxValue, float.MaxValue);
        }
    }
}

