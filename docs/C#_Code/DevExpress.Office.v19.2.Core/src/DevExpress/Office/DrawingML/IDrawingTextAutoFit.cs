namespace DevExpress.Office.DrawingML
{
    using DevExpress.Office;
    using System;

    public interface IDrawingTextAutoFit
    {
        IDrawingTextAutoFit CloneTo(IDocumentModel documentModel);
        bool Equals(IDrawingTextAutoFit other);
        void Visit(IDrawingTextAutoFitVisitor visitor);

        DrawingTextAutoFitType Type { get; }
    }
}

