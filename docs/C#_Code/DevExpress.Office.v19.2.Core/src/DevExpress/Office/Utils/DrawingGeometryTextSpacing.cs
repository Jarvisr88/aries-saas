namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingGeometryTextSpacing : OfficeDrawingFixedPointPropertyBase
    {
        public const double DefaultValue = 1.0;

        public DrawingGeometryTextSpacing()
        {
            base.Value = 1.0;
        }

        public DrawingGeometryTextSpacing(double value)
        {
            if ((value < 0.0) || (value > 5.0))
            {
                throw new ArgumentOutOfRangeException("Text spacing out of range 0..5!");
            }
            base.Value = value;
        }

        public override bool Complex =>
            false;
    }
}

