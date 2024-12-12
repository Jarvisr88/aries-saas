namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfBinaryToken : IPdfWritableObject
    {
        private readonly byte[] data;

        public PdfBinaryToken(byte[] data)
        {
            this.data = data;
        }

        void IPdfWritableObject.Write(PdfDocumentStream stream, int number)
        {
            stream.WriteBytes(this.data);
        }

        public byte[] Data =>
            this.data;
    }
}

