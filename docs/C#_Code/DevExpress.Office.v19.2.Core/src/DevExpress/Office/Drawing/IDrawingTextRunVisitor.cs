namespace DevExpress.Office.Drawing
{
    using System;

    public interface IDrawingTextRunVisitor
    {
        void Visit(DrawingTextField item);
        void Visit(DrawingTextLineBreak item);
        void Visit(DrawingTextRun item);
    }
}

