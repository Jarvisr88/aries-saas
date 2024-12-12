namespace DevExpress.DirectX.Common.Direct2D
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct D2D1_LINEAR_GRADIENT_BRUSH_PROPERTIES
    {
        private readonly D2D_POINT_2F startPoint;
        private readonly D2D_POINT_2F endPoint;
        public D2D1_LINEAR_GRADIENT_BRUSH_PROPERTIES(D2D_POINT_2F startPoint, D2D_POINT_2F endPoint)
        {
            this.startPoint = startPoint;
            this.endPoint = endPoint;
        }
    }
}

