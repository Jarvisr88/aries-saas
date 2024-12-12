namespace DevExpress.Office.Utils
{
    using System;

    public abstract class DrawingShadowOffset : OfficeDrawingIntPropertyBase
    {
        public const int DefaultValue = 0x6338;

        protected DrawingShadowOffset()
        {
            base.Value = 0x6338;
        }

        protected DrawingShadowOffset(int offset)
        {
            base.Value = offset;
        }

        public override bool Complex =>
            false;
    }
}

