namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;
    using System.Drawing;

    public interface IDrawingColor : IDrawingOriginalColor
    {
        DrawingColor CloneTo(IDocumentModel documentModel);
        Color ToRgb(Color styleColor);

        Color FinalColor { get; }

        DrawingColorType ColorType { get; }

        bool IsEmpty { get; }
    }
}

