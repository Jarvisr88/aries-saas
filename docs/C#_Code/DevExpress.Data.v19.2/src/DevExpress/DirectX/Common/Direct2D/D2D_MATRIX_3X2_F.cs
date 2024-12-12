namespace DevExpress.DirectX.Common.Direct2D
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct D2D_MATRIX_3X2_F
    {
        private static readonly D2D_MATRIX_3X2_F identity;
        private readonly float m11;
        private readonly float m12;
        private readonly float m21;
        private readonly float m22;
        private readonly float dx;
        private readonly float dy;
        public static D2D_MATRIX_3X2_F Identity =>
            identity;
        public float M11 =>
            this.m11;
        public float M12 =>
            this.m12;
        public float M21 =>
            this.m21;
        public float M22 =>
            this.m22;
        public float Dx =>
            this.dx;
        public float Dy =>
            this.dy;
        public D2D_MATRIX_3X2_F(float m11, float m12, float m21, float m22, float dx, float dy)
        {
            this.m11 = m11;
            this.m12 = m12;
            this.m21 = m21;
            this.m22 = m22;
            this.dx = dx;
            this.dy = dy;
        }

        static D2D_MATRIX_3X2_F()
        {
            identity = new D2D_MATRIX_3X2_F(1f, 0f, 0f, 1f, 0f, 0f);
        }
    }
}

