namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingFillBackOpacity : OfficeDrawingFixedPointPropertyBase
    {
        public const double DefaultValue = 1.0;

        public DrawingFillBackOpacity()
        {
            base.Value = 1.0;
        }

        public DrawingFillBackOpacity(double opacity)
        {
            base.Value = opacity;
        }

        public override bool Complex =>
            false;
    }
}

