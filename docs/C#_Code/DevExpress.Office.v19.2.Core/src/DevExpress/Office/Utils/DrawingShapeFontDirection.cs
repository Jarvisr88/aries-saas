namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingShapeFontDirection : OfficeDrawingIntPropertyBase
    {
        public override bool Complex =>
            false;

        public FontDirection Direction
        {
            get => 
                (FontDirection) base.Value;
            set => 
                base.Value = (int) value;
        }
    }
}

