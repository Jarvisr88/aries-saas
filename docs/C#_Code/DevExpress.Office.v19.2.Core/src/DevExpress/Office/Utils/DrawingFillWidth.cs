namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingFillWidth : OfficeDrawingIntPropertyBase
    {
        public const int DefaultValue = 0;

        public DrawingFillWidth()
        {
            base.Value = 0;
        }

        public DrawingFillWidth(int width)
        {
            base.Value = width;
        }

        public override bool Complex =>
            false;
    }
}

