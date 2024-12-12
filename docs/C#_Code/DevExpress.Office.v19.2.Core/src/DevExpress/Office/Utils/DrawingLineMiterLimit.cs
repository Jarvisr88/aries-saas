namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingLineMiterLimit : OfficeDrawingFixedPointPropertyBase
    {
        public const double DefaultValue = 8.0;

        public DrawingLineMiterLimit()
        {
            base.Value = 8.0;
        }

        public DrawingLineMiterLimit(int miterLimit)
        {
            base.Value = miterLimit;
        }

        public override bool Complex =>
            false;
    }
}

