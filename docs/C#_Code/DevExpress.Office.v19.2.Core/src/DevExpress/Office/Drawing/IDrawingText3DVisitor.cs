namespace DevExpress.Office.Drawing
{
    using System;

    public interface IDrawingText3DVisitor
    {
        void Visit(DrawingText3DFlatText text3d);
        void Visit(Shape3DProperties text3d);
    }
}

