namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingFillRectTop : OfficeDrawingIntPropertyBase
    {
        public const int DefaultValue = 0;

        public DrawingFillRectTop()
        {
            base.Value = 0;
        }

        public DrawingFillRectTop(int value)
        {
            base.Value = value;
        }

        public override bool Complex =>
            false;
    }
}

