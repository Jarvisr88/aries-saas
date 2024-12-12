namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfAcroFormListBoxField : PdfAcroFormChoiceField
    {
        private int topIndex;

        public PdfAcroFormListBoxField(string name, int pageNumber, PdfRectangle rectangle) : base(name, pageNumber, rectangle)
        {
        }

        protected internal override PdfInteractiveFormField CreateFormField(IPdfExportFontProvider fontSearch, PdfDocument document, PdfInteractiveFormField parent) => 
            new PdfChoiceFormField(parent, fontSearch, document, this);

        public int TopIndex
        {
            get => 
                this.topIndex;
            set => 
                this.topIndex = value;
        }

        public bool MultiSelect
        {
            get => 
                base.Controller.MultiSelect;
            set => 
                base.Controller.MultiSelect = value;
        }

        protected internal override PdfInteractiveFormFieldFlags Flags =>
            this.MultiSelect ? (base.Flags | PdfInteractiveFormFieldFlags.MultiSelect) : base.Flags;
    }
}

