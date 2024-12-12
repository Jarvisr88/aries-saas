namespace DevExpress.Office.Utils
{
    using System;

    public abstract class DrawingShadowSkewAngle : OfficeDrawingIntPropertyBase
    {
        public const int DefaultValue = 0;

        protected DrawingShadowSkewAngle()
        {
            base.Value = 0;
        }

        protected DrawingShadowSkewAngle(int angle)
        {
            base.Value = angle;
        }
    }
}

