namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Runtime.CompilerServices;

    public class PdfAcroFormTextBoxField : PdfAcroFormCommonVisualField
    {
        private int maxLength;

        public PdfAcroFormTextBoxField(string name, int pageNumber, PdfRectangle rectangle) : base(name, pageNumber, rectangle)
        {
        }

        internal override PdfAdditionalActions CreateAdditionalActions(PdfDocument document) => 
            (this.ValueFormat == null) ? base.CreateAdditionalActions(document) : new PdfAdditionalActions(new PdfInteractiveFormFieldActions(this.ValueFormat, document));

        protected internal override PdfInteractiveFormField CreateFormField(IPdfExportFontProvider fontSearch, PdfDocument document, PdfInteractiveFormField parent) => 
            new PdfTextFormField(parent, fontSearch, document, this);

        public string Text { get; set; }

        public bool SpellCheck { get; set; }

        public bool Scrollable { get; set; }

        public bool Multiline { get; set; }

        public PdfAcroFormTextFieldType Type { get; set; }

        public int MaxLength
        {
            get => 
                this.maxLength;
            set => 
                this.maxLength = Math.Max(0, value);
        }

        protected internal override PdfInteractiveFormFieldFlags Flags
        {
            get
            {
                PdfInteractiveFormFieldFlags flags = base.Flags;
                switch (this.Type)
                {
                    case PdfAcroFormTextFieldType.Password:
                        flags |= PdfInteractiveFormFieldFlags.Password;
                        break;

                    case PdfAcroFormTextFieldType.FileSelect:
                        flags |= PdfInteractiveFormFieldFlags.FileSelect;
                        break;

                    default:
                        break;
                }
                if (this.Multiline)
                {
                    flags |= PdfInteractiveFormFieldFlags.Multiline;
                }
                if (!this.SpellCheck)
                {
                    flags |= PdfInteractiveFormFieldFlags.DoNotSpellCheck;
                }
                if (!this.Scrollable)
                {
                    flags |= PdfInteractiveFormFieldFlags.DoNotScroll;
                }
                return flags;
            }
        }

        public PdfAcroFormValueFormat ValueFormat { get; set; }
    }
}

