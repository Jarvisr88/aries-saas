namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public interface IOfficeShape
    {
        ModelShapeCustomGeometry GetCalculatedGeometry();

        DevExpress.Office.Drawing.ShapeProperties ShapeProperties { get; }

        DevExpress.Office.Drawing.ShapeStyle ShapeStyle { get; }

        IDocumentModel DocumentModel { get; }

        float Height { get; }

        float Width { get; }

        bool HasTextProperties { get; }

        IDrawingLocks Locks { get; }
    }
}

