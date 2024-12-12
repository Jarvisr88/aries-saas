namespace DevExpress.DirectX.Common.Direct2D
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct D2D_POINT_2U
    {
        private static readonly D2D_POINT_2U empty;
        private readonly int x;
        private readonly int y;
        public static D2D_POINT_2U Empty =>
            empty;
        public D2D_POINT_2U(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        static D2D_POINT_2U()
        {
        }
    }
}

