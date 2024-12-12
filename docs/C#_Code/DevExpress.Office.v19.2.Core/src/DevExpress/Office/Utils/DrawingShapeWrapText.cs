namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingShapeWrapText : OfficeDrawingIntPropertyBase
    {
        public override bool Complex =>
            false;

        public MsoWrapMode WrapMode
        {
            get => 
                (MsoWrapMode) base.Value;
            set => 
                base.Value = (int) value;
        }
    }
}

