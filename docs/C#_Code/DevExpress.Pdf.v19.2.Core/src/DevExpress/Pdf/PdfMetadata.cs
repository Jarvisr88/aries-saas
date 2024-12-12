namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Text;

    public class PdfMetadata : PdfObject
    {
        internal const string Name = "Metadata";
        private const string subtypeValue = "XML";
        private readonly string data;

        internal PdfMetadata(PdfReaderStream stream) : base(stream.Dictionary.Number)
        {
            byte[] uncompressedData;
            PdfEncryptionInfo encryptionInfo = stream.Dictionary.Objects.EncryptionInfo;
            if (!((encryptionInfo != null) && encryptionInfo.EncryptMetadata))
            {
                uncompressedData = stream.GetUncompressedData(false);
            }
            else
            {
                try
                {
                    uncompressedData = stream.GetUncompressedData(true);
                }
                catch
                {
                    uncompressedData = stream.GetUncompressedData(false);
                }
            }
            if (uncompressedData == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.data = Encoding.UTF8.GetString(uncompressedData, 0, uncompressedData.Length);
        }

        internal PdfMetadata(string data)
        {
            this.data = data;
        }

        protected internal override object ToWritableObject(PdfObjectCollection collection)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(collection);
            dictionary.Add("Type", new PdfName("Metadata"));
            dictionary.Add("Subtype", new PdfName("XML"));
            return new PdfWriterStream(dictionary, Encoding.UTF8.GetBytes(this.data));
        }

        public string Data =>
            this.data;
    }
}

