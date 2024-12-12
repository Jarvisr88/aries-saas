namespace DevExpress.DirectX.Common.Direct2D
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct D2D1_RADIAL_GRADIENT_BRUSH_PROPERTIES
    {
        private readonly D2D_POINT_2F center;
        private readonly D2D_POINT_2F gradientOriginOffset;
        private readonly float radiusX;
        private readonly float radiusY;
        public D2D1_RADIAL_GRADIENT_BRUSH_PROPERTIES(D2D_POINT_2F center, D2D_POINT_2F gradientOriginOffset, float radiusX, float radiusY)
        {
            this.center = center;
            this.gradientOriginOffset = gradientOriginOffset;
            this.radiusX = radiusX;
            this.radiusY = radiusY;
        }
    }
}

