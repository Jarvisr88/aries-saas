namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using System;

    public class PdfAcroFormCombTextBoxField : PdfAcroFormTextBoxField
    {
        public PdfAcroFormCombTextBoxField(string name, int pageNumber, PdfRectangle rectangle, int maxLength) : base(name, pageNumber, rectangle)
        {
            base.MaxLength = maxLength;
        }

        protected internal override PdfInteractiveFormFieldFlags Flags =>
            base.Flags | PdfInteractiveFormFieldFlags.Comb;
    }
}

