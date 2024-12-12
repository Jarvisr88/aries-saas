namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using System;

    public abstract class XlDrawingFillBase
    {
        private const double minOpacity = 0.0;
        private const double maxOpacity = 1.0;
        private XlColor color = XlColor.Empty;
        private double opacity = 1.0;

        protected XlDrawingFillBase()
        {
        }

        public XlColor Color
        {
            get => 
                this.color;
            set
            {
                XlColor empty = value;
                if (value == null)
                {
                    XlColor local1 = value;
                    empty = XlColor.Empty;
                }
                this.color = empty;
            }
        }

        public double Opacity
        {
            get => 
                this.opacity;
            set
            {
                if ((value < 0.0) || (value > 1.0))
                {
                    throw new ArgumentOutOfRangeException($"Opacity out of range {0.0}...{1.0}");
                }
                this.opacity = value;
            }
        }
    }
}

