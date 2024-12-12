namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.IO;

    public class PdfStreamWriter : StreamWriter
    {
        private PdfDocument document;

        public PdfStreamWriter(Stream stream, PdfDocument document) : base(stream)
        {
            this.document = document;
        }

        public MemoryStream EncryptStream(MemoryStream stream, PdfObject pdfObject) => 
            ((this.document.Encryption == null) || (pdfObject.ChainingIndirectReference == null)) ? stream : this.document.Encryption.EncryptStream(stream, pdfObject.ChainingIndirectReference.Number, 0);

        public string EncryptString(string text, PdfObject pdfObject) => 
            ((this.document.Encryption == null) || (pdfObject.ChainingIndirectReference == null)) ? text : this.document.Encryption.EncryptString(text, pdfObject.ChainingIndirectReference.Number, 0);
    }
}

