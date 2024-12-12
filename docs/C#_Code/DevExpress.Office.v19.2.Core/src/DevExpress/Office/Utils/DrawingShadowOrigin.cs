namespace DevExpress.Office.Utils
{
    using System;

    public abstract class DrawingShadowOrigin : OfficeDrawingFixedPointPropertyBase
    {
        public const double DefaultValue = 0.0;

        protected DrawingShadowOrigin()
        {
            base.Value = 0.0;
        }

        protected DrawingShadowOrigin(double origin)
        {
            base.Value = origin;
        }

        public override bool Complex =>
            false;
    }
}

