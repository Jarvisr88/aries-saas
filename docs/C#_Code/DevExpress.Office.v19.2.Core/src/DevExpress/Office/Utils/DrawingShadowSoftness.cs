namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingShadowSoftness : OfficeDrawingIntPropertyBase
    {
        public const int DefaultValue = 0;

        public DrawingShadowSoftness()
        {
            base.Value = 0;
        }

        public DrawingShadowSoftness(int softness)
        {
            base.Value = softness;
        }

        public override bool Complex =>
            false;
    }
}

