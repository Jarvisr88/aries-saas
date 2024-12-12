namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    public abstract class PdfPageTreeItem : PdfDocumentDictionaryObject
    {
        private PdfRectangle mediaBox;
        private PdfPageTreeItem parent;

        protected PdfPageTreeItem(PdfPageTreeItem parent, bool compressed) : base(compressed)
        {
            this.parent = parent;
        }

        public override void FillUp()
        {
            base.FillUp();
            if (this.mediaBox != null)
            {
                base.Dictionary.Add("MediaBox", this.mediaBox);
            }
            if (this.parent != null)
            {
                base.Dictionary.Add("Parent", this.parent.Dictionary);
            }
        }

        public PdfRectangle MediaBox
        {
            get => 
                this.mediaBox;
            set => 
                this.mediaBox = value;
        }

        public PdfPageTreeItem Parent =>
            this.parent;
    }
}

