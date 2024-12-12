namespace DevExpress.DirectX.Common.DirectWrite
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct DWRITE_MATRIX
    {
        private float m11;
        private float m12;
        private float m21;
        private float m22;
        private float dx;
        private float dy;
        public DWRITE_MATRIX(float m11, float m12, float m21, float m22, float dx, float dy)
        {
            this.m11 = m11;
            this.m12 = m12;
            this.m21 = m21;
            this.m22 = m22;
            this.dx = dx;
            this.dy = dy;
        }
    }
}

