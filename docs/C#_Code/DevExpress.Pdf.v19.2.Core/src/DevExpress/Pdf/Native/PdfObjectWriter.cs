namespace DevExpress.Pdf.Native
{
    using System;
    using System.IO;

    internal class PdfObjectWriter
    {
        public const string EndOfLine = "\r\n";
        private readonly PdfDocumentStream stream;

        public PdfObjectWriter(PdfDocumentStream stream)
        {
            this.stream = stream;
        }

        public PdfObjectWriter(System.IO.Stream stream) : this(PdfDocumentStream.CreateStreamForWriting(stream))
        {
        }

        protected void WriteEndOfDocument()
        {
            this.stream.WriteString("\r\n%%EOF\r\n");
        }

        public virtual PdfObjectPointer WriteIndirectObject(PdfObjectContainer container) => 
            this.stream.WriteObjectContainer(container);

        public PdfDocumentStream Stream =>
            this.stream;
    }
}

