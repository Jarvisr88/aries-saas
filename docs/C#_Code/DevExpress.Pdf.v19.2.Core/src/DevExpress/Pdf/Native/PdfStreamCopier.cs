namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfStreamCopier : IPdfWritableObject
    {
        private readonly PdfWriterDictionary dictionary;
        private readonly PdfStreamCompressedData data;

        public PdfStreamCopier(PdfWriterDictionary dictionary, PdfStreamCompressedData data)
        {
            this.dictionary = dictionary;
            this.data = data;
            dictionary["Length"] = data.DecryptedDataLength;
        }

        public void Write(PdfDocumentStream stream, int number)
        {
            ((IPdfWritableObject) this.dictionary).Write(stream, number);
            stream.WriteString("stream\r\n");
            this.data.WriteData(stream);
            stream.WriteString("\r\nendstream");
        }
    }
}

