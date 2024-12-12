namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public interface IPdfDocumentContext
    {
        void NotifyFontChanged(PdfFont font);

        PdfDocumentCatalog DocumentCatalog { get; }
    }
}

