namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Runtime.CompilerServices;

    public class PdfAcroFormComboBoxField : PdfAcroFormChoiceField
    {
        public PdfAcroFormComboBoxField(string name, int pageNumber, PdfRectangle rect) : base(name, pageNumber, rect)
        {
        }

        internal override PdfAdditionalActions CreateAdditionalActions(PdfDocument document) => 
            (this.ValueFormat == null) ? base.CreateAdditionalActions(document) : new PdfAdditionalActions(new PdfInteractiveFormFieldActions(this.ValueFormat, document));

        protected internal override PdfInteractiveFormField CreateFormField(IPdfExportFontProvider fontSearch, PdfDocument document, PdfInteractiveFormField parent) => 
            new PdfChoiceFormField(this, fontSearch, document, parent);

        public bool Editable
        {
            get => 
                base.Controller.Editable;
            set => 
                base.Controller.Editable = value;
        }

        protected internal override PdfInteractiveFormFieldFlags Flags
        {
            get
            {
                PdfInteractiveFormFieldFlags flags = base.Flags | PdfInteractiveFormFieldFlags.Combo;
                if (this.Editable)
                {
                    flags |= PdfInteractiveFormFieldFlags.Edit;
                }
                return flags;
            }
        }

        public PdfAcroFormValueFormat ValueFormat { get; set; }
    }
}

