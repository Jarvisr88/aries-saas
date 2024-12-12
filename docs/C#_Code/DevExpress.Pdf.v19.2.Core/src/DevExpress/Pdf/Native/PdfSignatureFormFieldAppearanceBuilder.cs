namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfSignatureFormFieldAppearanceBuilder : PdfTextFieldAppearanceBuilder<PdfSignatureFormField>
    {
        private readonly string content;
        private readonly PdfAcroFormStringAlignment lineAlignment;

        public PdfSignatureFormFieldAppearanceBuilder(PdfAcroFormSignatureField signature, PdfWidgetAnnotation widget, PdfSignatureFormField formField, IPdfExportFontProvider fontSearch) : base(widget, formField, fontSearch, null)
        {
            this.content = signature.Text;
            this.lineAlignment = signature.LineAlignment;
        }

        protected override PdfStringFormat CreateStringFormat()
        {
            PdfStringFormat format = base.CreateStringFormat();
            PdfAcroFormStringAlignment lineAlignment = this.lineAlignment;
            format.LineAlignment = (lineAlignment == PdfAcroFormStringAlignment.Center) ? PdfStringAlignment.Center : ((lineAlignment == PdfAcroFormStringAlignment.Far) ? PdfStringAlignment.Far : PdfStringAlignment.Near);
            return format;
        }

        protected override void DrawContent(PdfFormCommandConstructor constructor, PdfRectangle contentRectangle)
        {
            if (!string.IsNullOrEmpty(this.content))
            {
                base.DrawTextField(constructor, contentRectangle, this.content);
            }
        }

        protected override bool Multiline =>
            true;
    }
}

