namespace DevExpress.Office.DrawingML
{
    using System;

    public interface IDrawingTextAutoFitVisitor
    {
        void Visit(DrawingTextNormalAutoFit autoFit);
        void VisitAutoFitNone();
        void VisitAutoFitShape();
    }
}

