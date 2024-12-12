namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfStreamCompressedData : PdfCompressedData
    {
        private readonly PdfDocumentCatalog documentCatalog;
        private readonly int objectNumber;
        private readonly int decryptedDataLength;

        public PdfStreamCompressedData(PdfReaderDictionary dictionary) : base(dictionary)
        {
            this.documentCatalog = dictionary.Objects.DocumentCatalog;
            this.objectNumber = dictionary.Number;
            int? integer = dictionary.GetInteger("Length");
            this.decryptedDataLength = (integer != null) ? integer.GetValueOrDefault() : this.Data.Length;
        }

        public override IPdfWritableObject CreateWritableObject(PdfWriterDictionary dictionary)
        {
            base.AddFilters(dictionary);
            return (((dictionary.Objects.EncryptionInfo != null) || (this.documentCatalog.Objects.EncryptionInfo != null)) ? ((IPdfWritableObject) new PdfWriterStream(dictionary, this.Data)) : ((IPdfWritableObject) new PdfStreamCopier(dictionary, this)));
        }

        public void WriteData(PdfDocumentStream stream)
        {
            PdfEncryptionInfo encryptionInfo = stream.EncryptionInfo;
            if (encryptionInfo != null)
            {
                stream.WriteBytes(encryptionInfo.EncryptData(this.Data, this.objectNumber));
            }
            else if (this.decryptedDataLength > 0)
            {
                PdfDataStream stream2 = this.documentCatalog.Objects.GetIndirectObject(this.objectNumber).Stream;
                PdfDocumentParser.SetPositionToStreamDataFirstByte(stream2);
                stream.CopyFrom(stream2, this.decryptedDataLength);
            }
        }

        public override byte[] Data
        {
            get
            {
                PdfReaderStream stream = this.documentCatalog.Objects.TryResolve(new PdfObjectReference(this.objectNumber), null) as PdfReaderStream;
                return ((stream != null) ? stream.DecryptedData : new byte[0]);
            }
        }

        public int DecryptedDataLength =>
            this.decryptedDataLength;
    }
}

