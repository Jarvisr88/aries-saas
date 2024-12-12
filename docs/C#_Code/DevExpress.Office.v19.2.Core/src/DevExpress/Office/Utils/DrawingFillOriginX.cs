namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingFillOriginX : OfficeDrawingFixedPointPropertyBase
    {
        public const double DefaultValue = 0.0;

        public DrawingFillOriginX()
        {
            base.Value = 0.0;
        }

        public DrawingFillOriginX(double origin)
        {
            if ((origin < -1.5) || (origin > 0.5))
            {
                throw new ArgumentOutOfRangeException("Origin out of range -1.5..0.5!");
            }
            base.Value = origin;
        }

        public override bool Complex =>
            false;
    }
}

