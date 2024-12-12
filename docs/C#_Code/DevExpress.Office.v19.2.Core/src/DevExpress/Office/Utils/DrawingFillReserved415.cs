namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingFillReserved415 : OfficeDrawingIntPropertyBase
    {
        public const int DefaultValue = -1;

        public DrawingFillReserved415()
        {
            base.Value = -1;
        }

        public override bool Complex =>
            false;
    }
}

