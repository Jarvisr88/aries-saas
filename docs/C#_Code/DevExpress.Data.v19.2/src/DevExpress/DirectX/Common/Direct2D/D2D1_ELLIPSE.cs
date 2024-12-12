namespace DevExpress.DirectX.Common.Direct2D
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct D2D1_ELLIPSE
    {
        private readonly D2D_POINT_2F point;
        private readonly float radiusX;
        private readonly float radiusY;
        public D2D1_ELLIPSE(D2D_POINT_2F point, float radiusX, float radiusY)
        {
            this.point = point;
            this.radiusX = radiusX;
            this.radiusY = radiusY;
        }
    }
}

