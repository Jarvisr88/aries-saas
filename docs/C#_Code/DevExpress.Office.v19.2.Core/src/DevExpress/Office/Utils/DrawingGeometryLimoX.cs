namespace DevExpress.Office.Utils
{
    using System;

    public class DrawingGeometryLimoX : OfficeDrawingIntPropertyBase, IDrawingGeometryLimoX, IOfficeDrawingIntPropertyBase
    {
        public const int DefaultValue = 0x7fffffff;

        public override bool Complex =>
            false;
    }
}

