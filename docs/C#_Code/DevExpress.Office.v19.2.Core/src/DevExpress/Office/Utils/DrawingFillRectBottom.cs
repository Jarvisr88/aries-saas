namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingFillRectBottom : OfficeDrawingIntPropertyBase
    {
        public const int DefaultValue = 0;

        public DrawingFillRectBottom()
        {
            base.Value = 0;
        }

        public DrawingFillRectBottom(int value)
        {
            base.Value = value;
        }

        public override bool Complex =>
            false;
    }
}

