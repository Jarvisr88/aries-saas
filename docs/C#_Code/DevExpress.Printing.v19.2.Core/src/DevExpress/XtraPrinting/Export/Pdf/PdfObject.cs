namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.IO;

    public abstract class PdfObject
    {
        private PdfObject owner;
        private PdfObjectType type;
        private PdfIndirectReference indirectReference;

        protected PdfObject() : this(PdfObjectType.Direct)
        {
        }

        protected PdfObject(PdfObjectType type)
        {
            this.type = type;
            if (type == PdfObjectType.Indirect)
            {
                this.indirectReference = new PdfIndirectReference();
            }
        }

        protected abstract void WriteContent(StreamWriter writer);
        public void WriteIndirect(StreamWriter writer)
        {
            if (this.indirectReference == null)
            {
                throw new PdfException("Can't write direct object as indirect");
            }
            this.indirectReference.CalculateByteOffset(writer);
            writer.WriteLine("{0} {1} obj", this.indirectReference.Number, 0);
            this.WriteContent(writer);
            writer.WriteLine();
            writer.WriteLine("endobj");
        }

        public void WriteToStream(StreamWriter writer)
        {
            if (this.indirectReference != null)
            {
                this.indirectReference.WriteToStream(writer);
            }
            else
            {
                this.WriteContent(writer);
            }
        }

        public PdfObjectType Type =>
            this.type;

        public PdfIndirectReference IndirectReference =>
            this.indirectReference;

        public PdfIndirectReference ChainingIndirectReference =>
            (this.IndirectReference == null) ? this.Owner?.ChainingIndirectReference : this.IndirectReference;

        public PdfObject Owner
        {
            get => 
                this.owner;
            set => 
                this.owner = value;
        }
    }
}

