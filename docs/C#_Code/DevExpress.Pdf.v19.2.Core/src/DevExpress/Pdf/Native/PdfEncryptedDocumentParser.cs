namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfEncryptedDocumentParser : PdfDocumentParser
    {
        private readonly string nonEncryptedDictionaryKey;

        public PdfEncryptedDocumentParser(PdfObjectCollection objects, int number, int generation, PdfDataStream stream, string nonEncryptedDictionaryKey) : base(objects, number, generation, stream)
        {
            this.nonEncryptedDictionaryKey = nonEncryptedDictionaryKey;
        }

        protected override PdfReaderStream CreateStream(PdfReaderDictionary dictionary, byte[] data) => 
            new PdfReaderStream(dictionary, data);

        protected override byte[] DecryptString(List<byte> list)
        {
            byte[] buffer = base.DecryptString(list);
            string currentlyReadingDictionaryKey = base.CurrentlyReadingDictionaryKey;
            return (((currentlyReadingDictionaryKey == null) || (currentlyReadingDictionaryKey != this.nonEncryptedDictionaryKey)) ? base.Objects.EncryptionInfo.DecryptData(base.DecryptString(list), base.Number, base.Generation) : buffer);
        }
    }
}

