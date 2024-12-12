namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingFillToRight : OfficeDrawingFixedPointPropertyBase
    {
        public const double DefaultValue = 0.0;

        public DrawingFillToRight()
        {
            base.Value = 0.0;
        }

        public DrawingFillToRight(double value)
        {
            base.Value = value;
        }

        public override bool Complex =>
            false;
    }
}

