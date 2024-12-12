namespace DevExpress.DirectX.Common.Direct2D
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct D2D1_ROUNDED_RECT
    {
        private readonly D2D_RECT_F rect;
        private readonly float radiusX;
        private readonly float radiusY;
        public D2D_RECT_F Rect =>
            this.rect;
        public float RadiusX =>
            this.radiusX;
        public float RadiusY =>
            this.radiusY;
        public D2D1_ROUNDED_RECT(D2D_RECT_F rect, float radiusX, float radiusY)
        {
            this.rect = rect;
            this.radiusX = radiusX;
            this.radiusY = radiusY;
        }
    }
}

