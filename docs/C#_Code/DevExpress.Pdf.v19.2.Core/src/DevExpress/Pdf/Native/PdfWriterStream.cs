namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfWriterStream : PdfStream, IPdfWritableObject
    {
        public const string BeginStreamMarker = "stream\r\n";
        public const string EndStreamMarker = "\r\nendstream";
        private readonly PdfWriterDictionary dictionary;

        public PdfWriterStream(PdfWriterDictionary dictionary, byte[] data) : base(data)
        {
            this.dictionary = dictionary;
        }

        public static PdfWriterStream CreateCompressedStream(PdfWriterDictionary dictionary, byte[] data) => 
            new PdfArrayCompressedData(data).CreateWriterStream(dictionary);

        void IPdfWritableObject.Write(PdfDocumentStream stream, int number)
        {
            PdfEncryptionInfo encryptionInfo = stream.EncryptionInfo;
            byte[] buffer = (encryptionInfo == null) ? base.Data : encryptionInfo.EncryptData(base.Data, number);
            this.dictionary["Length"] = buffer.Length;
            ((IPdfWritableObject) this.dictionary).Write(stream, number);
            stream.WriteString("stream\r\n");
            stream.WriteBytes(buffer);
            stream.WriteString("\r\nendstream");
        }
    }
}

