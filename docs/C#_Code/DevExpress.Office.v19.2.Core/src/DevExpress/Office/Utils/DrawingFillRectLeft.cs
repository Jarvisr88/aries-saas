namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingFillRectLeft : OfficeDrawingIntPropertyBase
    {
        public const int DefaultValue = 0;

        public DrawingFillRectLeft()
        {
            base.Value = 0;
        }

        public DrawingFillRectLeft(int value)
        {
            base.Value = value;
        }

        public override bool Complex =>
            false;
    }
}

