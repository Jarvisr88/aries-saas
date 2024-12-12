namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingFillHeight : OfficeDrawingIntPropertyBase
    {
        public const int DefaultValue = 0;

        public DrawingFillHeight()
        {
            base.Value = 0;
        }

        public DrawingFillHeight(int width)
        {
            base.Value = width;
        }

        public override bool Complex =>
            false;
    }
}

