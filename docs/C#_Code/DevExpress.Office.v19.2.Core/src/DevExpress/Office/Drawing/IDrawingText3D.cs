namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public interface IDrawingText3D
    {
        IDrawingText3D CloneTo(IDocumentModel documentModel);
        void Visit(IDrawingText3DVisitor visitor);

        DrawingText3DType Type { get; }
    }
}

