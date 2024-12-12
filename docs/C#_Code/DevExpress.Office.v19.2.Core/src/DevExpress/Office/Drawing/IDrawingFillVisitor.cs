namespace DevExpress.Office.Drawing
{
    using System;

    public interface IDrawingFillVisitor
    {
        void Visit(DrawingBlipFill fill);
        void Visit(DrawingFill fill);
        void Visit(DrawingGradientFill fill);
        void Visit(DrawingPatternFill fill);
        void Visit(DrawingSolidFill fill);
    }
}

