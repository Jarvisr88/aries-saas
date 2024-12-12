namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingLineCapStyle : OfficeDrawingIntPropertyBase
    {
        public DrawingLineCapStyle()
        {
            base.Value = 2;
        }

        public DrawingLineCapStyle(OutlineEndCapStyle capStyle)
        {
            base.Value = (int) capStyle;
        }

        public override bool Complex =>
            false;

        public OutlineEndCapStyle Style
        {
            get => 
                (OutlineEndCapStyle) base.Value;
            set => 
                base.Value = (int) value;
        }
    }
}

