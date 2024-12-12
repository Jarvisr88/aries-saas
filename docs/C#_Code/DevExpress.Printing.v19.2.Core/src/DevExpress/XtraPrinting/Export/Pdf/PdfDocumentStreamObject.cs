namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    public abstract class PdfDocumentStreamObject : PdfDocumentObject
    {
        protected PdfDocumentStreamObject(bool compressed) : base(null, compressed)
        {
            base.SetInnerObjectIfNull(CreateInnerStream(this.UseFlateEncoding & compressed));
        }

        public virtual void Close()
        {
            this.Stream.Close();
        }

        private static PdfStream CreateInnerStream(bool useFlateEncoding) => 
            useFlateEncoding ? new PdfDirectFlateStream() : new PdfStream();

        protected virtual bool UseFlateEncoding =>
            true;

        public PdfStream Stream =>
            (PdfStream) base.InnerObject;

        public PdfDictionary Attributes =>
            this.Stream.Attributes;
    }
}

