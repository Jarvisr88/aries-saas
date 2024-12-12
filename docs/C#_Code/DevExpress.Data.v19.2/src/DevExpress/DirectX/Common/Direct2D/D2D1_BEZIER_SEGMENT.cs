namespace DevExpress.DirectX.Common.Direct2D
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct D2D1_BEZIER_SEGMENT
    {
        private readonly D2D_POINT_2F point1;
        private readonly D2D_POINT_2F point2;
        private readonly D2D_POINT_2F point3;
        public D2D_POINT_2F Point1 =>
            this.point1;
        public D2D_POINT_2F Point2 =>
            this.point2;
        public D2D_POINT_2F Point3 =>
            this.point3;
        public D2D1_BEZIER_SEGMENT(D2D_POINT_2F point1, D2D_POINT_2F point2, D2D_POINT_2F point3)
        {
            this.point1 = point1;
            this.point2 = point2;
            this.point3 = point3;
        }
    }
}

