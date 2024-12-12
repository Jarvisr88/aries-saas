namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingFillToTop : OfficeDrawingFixedPointPropertyBase
    {
        public const double DefaultValue = 0.0;

        public DrawingFillToTop()
        {
            base.Value = 0.0;
        }

        public DrawingFillToTop(double value)
        {
            base.Value = value;
        }

        public override bool Complex =>
            false;
    }
}

