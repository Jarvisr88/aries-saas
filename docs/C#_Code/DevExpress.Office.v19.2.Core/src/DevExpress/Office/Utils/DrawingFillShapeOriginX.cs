namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingFillShapeOriginX : OfficeDrawingFixedPointPropertyBase
    {
        public const double DefaultValue = 0.0;

        public DrawingFillShapeOriginX()
        {
            base.Value = 0.0;
        }

        public DrawingFillShapeOriginX(double origin)
        {
            if ((origin < -0.5) || (origin > 0.5))
            {
                throw new ArgumentOutOfRangeException("Origin out of range -0.5..0.5!");
            }
            base.Value = origin;
        }

        public override bool Complex =>
            false;
    }
}

