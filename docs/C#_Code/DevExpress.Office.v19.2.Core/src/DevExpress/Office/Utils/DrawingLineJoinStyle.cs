namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingLineJoinStyle : OfficeDrawingIntPropertyBase
    {
        public DrawingLineJoinStyle()
        {
            base.Value = 1;
        }

        public DrawingLineJoinStyle(LineJoinStyle joinStyle)
        {
            base.Value = (int) joinStyle;
        }

        public override bool Complex =>
            false;

        public LineJoinStyle Style
        {
            get => 
                (LineJoinStyle) base.Value;
            set => 
                base.Value = (int) value;
        }
    }
}

