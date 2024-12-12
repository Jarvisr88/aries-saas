namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Runtime.CompilerServices;

    public class PdfAcroFormSignatureField : PdfAcroFormCommonVisualField
    {
        private readonly IPdfAnnotationAppearanceBuilder builder;

        public PdfAcroFormSignatureField(string name, int pageNumber, PdfRectangle rect) : base(name, pageNumber, rect)
        {
        }

        internal PdfAcroFormSignatureField(string name, int pageNumber, PdfRectangle rect, IPdfAnnotationAppearanceBuilder builder) : base(name, pageNumber, rect)
        {
            this.builder = builder;
        }

        protected internal override PdfInteractiveFormField CreateFormField(IPdfExportFontProvider fontSearch, PdfDocument document, PdfInteractiveFormField parent) => 
            new PdfSignatureFormField(parent, fontSearch, document, this, this.builder);

        public string Text { get; set; }

        public PdfAcroFormStringAlignment LineAlignment { get; set; }
    }
}

