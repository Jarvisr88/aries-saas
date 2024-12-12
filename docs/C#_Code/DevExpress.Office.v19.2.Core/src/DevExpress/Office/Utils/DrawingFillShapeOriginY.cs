namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingFillShapeOriginY : OfficeDrawingFixedPointPropertyBase
    {
        public const double DefaultValue = 0.0;

        public DrawingFillShapeOriginY()
        {
            base.Value = 0.0;
        }

        public DrawingFillShapeOriginY(double origin)
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

