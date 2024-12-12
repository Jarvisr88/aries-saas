namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingGeometryShapePath : OfficeDrawingIntPropertyBase
    {
        public override bool Complex =>
            false;

        public ShapePathType ShapePath
        {
            get => 
                (ShapePathType) base.Value;
            set => 
                base.Value = (int) value;
        }
    }
}

