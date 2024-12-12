namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingFillOpacity : OfficeDrawingFixedPointPropertyBase
    {
        public const double DefaultValue = 1.0;

        public DrawingFillOpacity()
        {
            base.Value = 1.0;
        }

        public DrawingFillOpacity(double opacity)
        {
            base.Value = opacity;
        }

        public override bool Complex =>
            false;
    }
}

