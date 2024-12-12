namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.IO;

    public abstract class PdfDocumentObject
    {
        private PdfObject innerObject;
        private bool compressed;

        protected PdfDocumentObject(PdfObject innerObject, bool compressed)
        {
            this.innerObject = innerObject;
            this.compressed = compressed;
        }

        public virtual void AddToDictionary(PdfDictionary dictionary)
        {
            throw new NotImplementedException();
        }

        public virtual void FillUp()
        {
        }

        public void Register(PdfXRef xRef)
        {
            xRef.RegisterObject(this.innerObject);
            this.RegisterContent(xRef);
        }

        protected virtual void RegisterContent(PdfXRef xRef)
        {
        }

        protected void SetInnerObjectIfNull(PdfObject innerObject)
        {
            this.innerObject ??= innerObject;
        }

        public void Write(StreamWriter writer)
        {
            this.innerObject.WriteIndirect(writer);
            this.WriteContent(writer);
        }

        protected virtual void WriteContent(StreamWriter writer)
        {
        }

        protected bool Compressed =>
            this.compressed;

        public PdfObject InnerObject =>
            this.innerObject;
    }
}

