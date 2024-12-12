namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public interface IDrawingTextRun
    {
        IDrawingTextRun CloneTo(IDocumentModel documentModel);
        void Visit(IDrawingTextRunVisitor visitor);

        string Text { get; }

        ISupportsInvalidate Parent { get; set; }

        DrawingTextCharacterProperties RunProperties { get; }
    }
}

