namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    public class PdfContents : PdfDocumentStreamObject
    {
        private PdfDrawContext context;

        public PdfContents(IPdfContentsOwner owner, bool compressed, PdfHashtable pdfHashtable) : base(compressed)
        {
            this.context = PdfDrawContext.Create(base.Stream, owner, pdfHashtable);
        }

        public PdfDrawContext DrawContext =>
            this.context;
    }
}

