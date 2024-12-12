namespace DevExpress.Emf
{
    using System;
    using System.Collections.Generic;

    public class DXPen : IDisposable
    {
        private static IDictionary<DXDashStyle, float[]> dashPatterns = new Dictionary<DXDashStyle, float[]>();
        private DXPenAlignment alignment;
        private DXBrush brush;
        private double width;
        private DXLineCap startCap;
        private DXLineCap endCap;
        private DXCustomLineCap customStartCap;
        private DXCustomLineCap customEndCap;
        private double miterLimit;
        private DXDashCap dashCap;
        private DXDashStyle dashStyle;
        private double dashOffset;
        private float[] dashPattern;
        private DXLineJoin lineJoin;

        static DXPen()
        {
            float[] singleArray1 = new float[] { 1f, 1f };
            dashPatterns.Add(DXDashStyle.Dot, singleArray1);
            float[] singleArray2 = new float[] { 3f, 1f, 1f, 1f, 1f, 1f };
            dashPatterns.Add(DXDashStyle.DashDotDot, singleArray2);
            float[] singleArray3 = new float[] { 3f, 1f, 1f, 1f };
            dashPatterns.Add(DXDashStyle.DashDot, singleArray3);
            float[] singleArray4 = new float[] { 3f, 1f };
            dashPatterns.Add(DXDashStyle.Dash, singleArray4);
        }

        public DXPen(ARGBColor color) : this(color, 1.0)
        {
        }

        public DXPen(DXBrush brush) : this(brush, 1.0)
        {
        }

        public DXPen(ARGBColor color, double width) : this(new DXSolidBrush(color), width)
        {
        }

        public DXPen(DXBrush brush, double width)
        {
            this.miterLimit = 10.0;
            this.brush = brush;
            this.width = (width < 0.0) ? 0.0 : width;
        }

        public void Dispose()
        {
            if (this.brush != null)
            {
                this.brush.Dispose();
            }
        }

        public DXPenAlignment Alignment
        {
            get => 
                this.alignment;
            set => 
                this.alignment = value;
        }

        public double Width =>
            this.width;

        public DXBrush Brush
        {
            get => 
                this.brush;
            set => 
                this.brush = value;
        }

        public DXLineCap StartCap
        {
            get => 
                this.startCap;
            set => 
                this.startCap = value;
        }

        public DXLineCap EndCap
        {
            get => 
                this.endCap;
            set => 
                this.endCap = value;
        }

        public double MiterLimit
        {
            get => 
                this.miterLimit;
            set => 
                this.miterLimit = value;
        }

        public DXDashCap DashCap
        {
            get => 
                this.dashCap;
            set => 
                this.dashCap = value;
        }

        public DXDashStyle DashStyle
        {
            get => 
                this.dashStyle;
            set
            {
                if ((value != DXDashStyle.Custom) && (value != DXDashStyle.Solid))
                {
                    this.dashPattern = dashPatterns[value];
                }
                this.dashStyle = value;
            }
        }

        public double DashOffset
        {
            get => 
                this.dashOffset;
            set => 
                this.dashOffset = value;
        }

        public float[] DashPattern
        {
            get => 
                (this.dashStyle == DXDashStyle.Solid) ? null : this.dashPattern;
            set
            {
                this.dashStyle = DXDashStyle.Custom;
                this.dashPattern = value;
            }
        }

        public DXLineJoin LineJoin
        {
            get => 
                this.lineJoin;
            set => 
                this.lineJoin = value;
        }

        public DXCustomLineCap CustomStartCap
        {
            get => 
                this.customStartCap;
            set => 
                this.customStartCap = value;
        }

        public DXCustomLineCap CustomEndCap
        {
            get => 
                this.customEndCap;
            set => 
                this.customEndCap = value;
        }
    }
}

