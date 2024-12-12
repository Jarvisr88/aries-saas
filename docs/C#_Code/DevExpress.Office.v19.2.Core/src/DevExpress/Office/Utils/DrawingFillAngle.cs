namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingFillAngle : OfficeDrawingFixedPointPropertyBase
    {
        public const double DefaultValue = 0.0;

        public DrawingFillAngle()
        {
            base.Value = 0.0;
        }

        public DrawingFillAngle(double angle)
        {
            base.Value = angle;
        }

        public override bool Complex =>
            false;
    }
}

