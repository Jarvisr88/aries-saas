namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingFillToLeft : OfficeDrawingFixedPointPropertyBase
    {
        public const double DefaultValue = 0.0;

        public DrawingFillToLeft()
        {
            base.Value = 0.0;
        }

        public DrawingFillToLeft(double value)
        {
            base.Value = value;
        }

        public override bool Complex =>
            false;
    }
}

