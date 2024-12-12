namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    public abstract class PdfAnnotation : PdfDocumentDictionaryObject
    {
        private PdfRectangle rect;

        protected PdfAnnotation(bool compressed) : this(new PdfRectangle(), compressed)
        {
        }

        protected PdfAnnotation(PdfRectangle rect, bool compressed) : base(compressed)
        {
            rect ??= new PdfRectangle();
            this.rect = rect;
        }

        public override void FillUp()
        {
            base.Dictionary.Add("Type", "Annot");
            base.Dictionary.Add("Subtype", this.Subtype);
            base.Dictionary.Add("Rect", this.Rect);
        }

        public abstract string Subtype { get; }

        public PdfRectangle Rect
        {
            get => 
                this.rect;
            set => 
                this.rect = value;
        }
    }
}

