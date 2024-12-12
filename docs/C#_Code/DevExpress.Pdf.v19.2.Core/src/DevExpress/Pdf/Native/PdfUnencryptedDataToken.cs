namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfUnencryptedDataToken : IPdfWritableObject
    {
        private readonly byte[] data;

        public PdfUnencryptedDataToken(byte[] data)
        {
            this.data = data;
        }

        public void Write(PdfDocumentStream stream, int number)
        {
            stream.WriteHexadecimalString(this.data, -1);
        }
    }
}

