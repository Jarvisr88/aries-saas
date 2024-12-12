namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingShadowOpacity : OfficeDrawingFixedPointPropertyBase
    {
        public const double DefaultValue = 1.0;

        public DrawingShadowOpacity()
        {
            base.Value = 1.0;
        }

        public DrawingShadowOpacity(double opacity)
        {
            base.Value = opacity;
        }

        public override bool Complex =>
            false;
    }
}

