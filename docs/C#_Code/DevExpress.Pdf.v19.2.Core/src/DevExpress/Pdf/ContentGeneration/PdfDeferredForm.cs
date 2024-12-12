namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;

    public class PdfDeferredForm : PdfForm
    {
        public PdfDeferredForm(PdfDocumentCatalog catalog, PdfRectangle bbox) : base(catalog, bbox)
        {
        }

        protected internal override PdfObject GetDeferredSavedObject(PdfObjectCollection objects, bool isCloning) => 
            this;

        protected internal override bool IsDeferredObject(bool isCloning) => 
            true;
    }
}

