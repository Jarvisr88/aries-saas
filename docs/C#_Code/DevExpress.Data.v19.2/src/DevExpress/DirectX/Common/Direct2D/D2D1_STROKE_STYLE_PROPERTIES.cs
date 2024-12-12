namespace DevExpress.DirectX.Common.Direct2D
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct D2D1_STROKE_STYLE_PROPERTIES
    {
        private readonly D2D1_CAP_STYLE startCap;
        private readonly D2D1_CAP_STYLE endCap;
        private readonly D2D1_CAP_STYLE dashCap;
        private readonly D2D1_LINE_JOIN lineJoin;
        private readonly float miterLimit;
        private readonly D2D1_DASH_STYLE dashStyle;
        private readonly float dashOffset;
        public D2D1_STROKE_STYLE_PROPERTIES(D2D1_CAP_STYLE lineCap, D2D1_LINE_JOIN lineJoin, float miterLimit)
        {
            this.startCap = lineCap;
            this.endCap = lineCap;
            this.dashCap = lineCap;
            this.lineJoin = lineJoin;
            this.miterLimit = miterLimit;
            this.dashStyle = D2D1_DASH_STYLE.Solid;
            this.dashOffset = 0f;
        }
    }
}

