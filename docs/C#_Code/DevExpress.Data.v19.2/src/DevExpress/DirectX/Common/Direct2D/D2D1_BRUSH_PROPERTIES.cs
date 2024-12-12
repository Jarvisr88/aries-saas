namespace DevExpress.DirectX.Common.Direct2D
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct D2D1_BRUSH_PROPERTIES
    {
        private readonly float opacity;
        private readonly D2D_MATRIX_3X2_F transform;
        public static D2D1_BRUSH_PROPERTIES Default =>
            new D2D1_BRUSH_PROPERTIES(1f, D2D_MATRIX_3X2_F.Identity);
        public float Opacity =>
            this.opacity;
        public D2D_MATRIX_3X2_F Transform =>
            this.transform;
        public D2D1_BRUSH_PROPERTIES(float opacity, D2D_MATRIX_3X2_F transform)
        {
            this.opacity = opacity;
            this.transform = transform;
        }
    }
}

