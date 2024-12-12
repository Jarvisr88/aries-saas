namespace DevExpress.DirectX.Common.Direct2D
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct D2D1_COLOR_F
    {
        private static readonly D2D1_COLOR_F transparentWhite;
        private readonly float r;
        private readonly float g;
        private readonly float b;
        private readonly float a;
        public static D2D1_COLOR_F TransparentWhite =>
            transparentWhite;
        public bool IsTransparent =>
            this.a == 0f;
        public float R =>
            this.r;
        public float G =>
            this.g;
        public float B =>
            this.b;
        public float A =>
            this.a;
        public D2D1_COLOR_F(float r, float g, float b, float a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        static D2D1_COLOR_F()
        {
            transparentWhite = new D2D1_COLOR_F(1f, 1f, 1f, 0f);
        }
    }
}

