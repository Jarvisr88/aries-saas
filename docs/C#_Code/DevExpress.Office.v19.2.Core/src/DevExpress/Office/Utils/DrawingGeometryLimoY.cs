namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingGeometryLimoY : OfficeDrawingIntPropertyBase, IDrawingGeometryLimoY, IOfficeDrawingIntPropertyBase
    {
        public const int DefaultValue = 0x7fffffff;

        public override bool Complex =>
            false;
    }
}

