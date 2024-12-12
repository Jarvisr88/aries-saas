namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public interface IDrawingFill : IOfficeNotifyPropertyChanged
    {
        IDrawingFill CloneTo(IDocumentModel documentModel);
        void Visit(IDrawingFillVisitor visitor);

        DrawingFillType FillType { get; }

        ISupportsInvalidate Parent { get; set; }
    }
}

