namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingGeometryTextSize : OfficeDrawingFixedPointPropertyBase
    {
        public const double DefaultValue = 36.0;

        public DrawingGeometryTextSize()
        {
            base.Value = 36.0;
        }

        public DrawingGeometryTextSize(double value)
        {
            if (value < 0.0)
            {
                throw new ArgumentOutOfRangeException("Text size must be greater than or equal to 0");
            }
            base.Value = value;
        }

        public override bool Complex =>
            false;
    }
}

