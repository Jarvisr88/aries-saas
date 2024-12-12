namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingFillRectRight : OfficeDrawingIntPropertyBase
    {
        public const int DefaultValue = 0;

        public DrawingFillRectRight()
        {
            base.Value = 0;
        }

        public DrawingFillRectRight(int value)
        {
            base.Value = value;
        }

        public override bool Complex =>
            false;
    }
}

