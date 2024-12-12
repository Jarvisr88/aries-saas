namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingGeometryTextAlign : OfficeDrawingIntPropertyBase
    {
        public override bool Complex =>
            false;

        public DrawingGeometryTextAlignType AlignType
        {
            get => 
                (DrawingGeometryTextAlignType) base.Value;
            set => 
                base.Value = (int) value;
        }
    }
}

