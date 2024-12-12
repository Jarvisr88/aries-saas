namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;

    public interface IFillOwner
    {
        void SetDrawingFillCore(IDrawingFill value);

        IDrawingFill Fill { get; set; }

        IDocumentModel DocumentModel { get; }
    }
}

