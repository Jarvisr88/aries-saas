namespace DevExpress.Pdf.Native
{
    using System;

    public interface IPdfBookmarkParent
    {
        void Invalidate();

        PdfDocumentCatalog DocumentCatalog { get; }
    }
}

