namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfPopupAnnotation : PdfAnnotation
    {
        internal const string Type = "Popup";
        private readonly int parentNumber;
        private readonly bool open;
        private PdfAnnotation parent;

        internal PdfPopupAnnotation(PdfPage page, PdfReaderDictionary dictionary) : base(page, dictionary)
        {
            PdfObjectReference objectReference = dictionary.GetObjectReference("Parent");
            this.parentNumber = (objectReference == null) ? 0 : objectReference.Number;
            bool? boolean = dictionary.GetBoolean("Open");
            this.open = (boolean != null) ? boolean.GetValueOrDefault() : false;
        }

        internal PdfPopupAnnotation(PdfPage page, PdfAnnotation parent) : base(page, builder1)
        {
            PdfAnnotationBuilder builder1 = new PdfAnnotationBuilder(parent.Rect);
            builder1.Flags = PdfAnnotationFlags.NoRotate | PdfAnnotationFlags.NoZoom | PdfAnnotationFlags.Print;
            this.parent = parent;
        }

        protected internal override void Accept(IPdfAnnotationVisitor visitor)
        {
            visitor.Visit(this);
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection collection)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(collection);
            dictionary.Add("Parent", this.Parent);
            dictionary.Add("Open", this.open, false);
            return dictionary;
        }

        private void ResolveParent()
        {
            if (this.parentNumber != 0)
            {
                foreach (PdfAnnotation annotation in base.Page.Annotations)
                {
                    if (annotation.ObjectNumber == this.parentNumber)
                    {
                        this.parent = annotation;
                        break;
                    }
                }
                this.parent ??= base.DocumentCatalog.Objects.GetObject<PdfAnnotation>(new PdfObjectReference(this.parentNumber), dictionary => Parse(base.Page, dictionary));
            }
        }

        public bool Open =>
            this.open;

        public PdfAnnotation Parent
        {
            get
            {
                if (this.parent == null)
                {
                    this.ResolveParent();
                }
                return this.parent;
            }
        }

        protected override string AnnotationType =>
            "Popup";
    }
}

