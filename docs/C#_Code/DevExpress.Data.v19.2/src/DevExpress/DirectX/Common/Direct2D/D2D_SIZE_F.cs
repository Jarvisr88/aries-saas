namespace DevExpress.DirectX.Common.Direct2D
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct D2D_SIZE_F
    {
        private readonly float width;
        private readonly float height;
        public D2D_SIZE_F(float width, float height)
        {
            this.width = width;
            this.height = height;
        }

        public float Width =>
            this.width;
        public float Height =>
            this.height;
    }
}

