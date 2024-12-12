namespace DevExpress.XtraExport.Implementation
{
    using System;
    using System.Runtime.CompilerServices;

    public class XlOutline : XlDrawingFillBase
    {
        private const double minWidth = 0.0;
        private const double maxWidth = 1584.0;
        private const double minMiterLimit = 0.0;
        private const double maxMiterLimit = 21474.83647;
        private double miterLimit = 8.0;
        private double width = 0.75;

        public XlOutline()
        {
            this.CapType = XlLineCapType.Flat;
            this.CompoundType = XlOutlineCompoundType.Single;
            this.Dashing = XlOutlineDashing.Solid;
            this.JoinType = XlLineJoinType.Round;
            this.StrokeAlignment = XlOutlineStrokeAlignment.Center;
        }

        public XlOutlineDashing Dashing { get; set; }

        public XlLineCapType CapType { get; set; }

        public XlOutlineCompoundType CompoundType { get; set; }

        public XlLineJoinType JoinType { get; set; }

        public double MiterLimit
        {
            get => 
                this.miterLimit;
            set
            {
                if ((value < 0.0) || (value > 21474.83647))
                {
                    throw new ArgumentOutOfRangeException($"Miter limit out of range {0.0}...{21474.83647}");
                }
                this.miterLimit = value;
            }
        }

        public XlOutlineStrokeAlignment StrokeAlignment { get; set; }

        public double Width
        {
            get => 
                this.width;
            set
            {
                if (value < 0.0)
                {
                    value = 0.0;
                }
                if (value > 1584.0)
                {
                    value = 1584.0;
                }
                this.width = value;
            }
        }
    }
}

