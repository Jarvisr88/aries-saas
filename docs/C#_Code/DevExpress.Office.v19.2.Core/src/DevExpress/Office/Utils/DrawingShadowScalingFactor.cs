namespace DevExpress.Office.Utils
{
    using System;

    public abstract class DrawingShadowScalingFactor : OfficeDrawingIntPropertyBase
    {
        public const int DefaultValue = 0x10000;

        protected DrawingShadowScalingFactor()
        {
            base.Value = 0x10000;
        }

        protected DrawingShadowScalingFactor(int factor)
        {
            base.Value = factor;
        }
    }
}

