namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingLineOpacity : OfficeDrawingIntPropertyBase
    {
        public DrawingLineOpacity()
        {
            base.Value = 0x10000;
        }

        public DrawingLineOpacity(int opacity)
        {
            base.Value = opacity;
        }

        public override bool Complex =>
            false;
    }
}

