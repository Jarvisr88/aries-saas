namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Localization;
    using DevExpress.Pdf.Native;
    using System;
    using System.Runtime.CompilerServices;

    public abstract class PdfAcroFormVisualField : PdfAcroFormField
    {
        private int pageNumber;
        private bool readOnly;
        private bool required;
        private bool visible;
        private bool print;
        private PdfAcroFormFieldAppearance appearance;

        protected PdfAcroFormVisualField(string name, int pageNumber) : base(name)
        {
            this.visible = true;
            this.print = true;
            this.appearance = new PdfAcroFormFieldAppearance();
            ValidatePageNumber(pageNumber);
            this.pageNumber = pageNumber;
        }

        protected internal virtual PdfWidgetAnnotationBuilder CreateWidgetBuilder(PdfRectangle rect)
        {
            PdfWidgetAnnotationBuilder builder1 = new PdfWidgetAnnotationBuilder(rect);
            builder1.Flags = this.AnnotationFlags;
            return builder1.SetAppearance(this.Appearance).SetRotation(this.Rotation);
        }

        private static void ValidatePageNumber(int pageNumber)
        {
            if (pageNumber < 1)
            {
                throw new ArgumentOutOfRangeException("pageNumber", PdfCoreLocalizer.GetString(PdfCoreStringId.MsgPageNumberShouldBePositive));
            }
        }

        public int PageNumber
        {
            get => 
                this.pageNumber;
            set
            {
                ValidatePageNumber(value);
                this.pageNumber = value;
            }
        }

        public bool ReadOnly
        {
            get => 
                this.readOnly;
            set => 
                this.readOnly = value;
        }

        public bool Required
        {
            get => 
                this.required;
            set => 
                this.required = value;
        }

        public bool Visible
        {
            get => 
                this.visible;
            set => 
                this.visible = value;
        }

        public bool Print
        {
            get => 
                this.print;
            set => 
                this.print = value;
        }

        public PdfAcroFormFieldAppearance Appearance
        {
            get => 
                this.appearance;
            set
            {
                PdfAcroFormFieldAppearance appearance1 = value;
                if (value == null)
                {
                    PdfAcroFormFieldAppearance local1 = value;
                    appearance1 = new PdfAcroFormFieldAppearance();
                }
                this.appearance = appearance1;
            }
        }

        public PdfAcroFormFieldRotation Rotation { get; set; }

        protected internal override PdfInteractiveFormFieldFlags Flags
        {
            get
            {
                PdfInteractiveFormFieldFlags flags = base.Flags;
                if (this.readOnly)
                {
                    flags |= PdfInteractiveFormFieldFlags.ReadOnly;
                }
                if (this.required)
                {
                    flags |= PdfInteractiveFormFieldFlags.Required;
                }
                return flags;
            }
        }

        protected virtual PdfAnnotationFlags AnnotationFlags
        {
            get
            {
                PdfAnnotationFlags none = PdfAnnotationFlags.None;
                if (!this.visible)
                {
                    none |= PdfAnnotationFlags.NoView;
                }
                if (this.print)
                {
                    none |= PdfAnnotationFlags.Print;
                }
                return none;
            }
        }
    }
}

