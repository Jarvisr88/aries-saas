namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfReaderStream : PdfStream
    {
        private readonly PdfReaderDictionary dictionary;
        private readonly PdfEncryptionInfo encryptionInfo;

        public PdfReaderStream(PdfReaderDictionary dictionary, byte[] data) : this(dictionary, data, dictionary.Objects.EncryptionInfo)
        {
        }

        public PdfReaderStream(PdfReaderDictionary dictionary, byte[] data, PdfEncryptionInfo encryptionInfo) : base(data)
        {
            this.dictionary = dictionary;
            this.encryptionInfo = encryptionInfo;
        }

        public byte[] GetUncompressedData(bool shouldDecrypt)
        {
            byte[] buffer = shouldDecrypt ? this.DecryptedData : base.Data;
            return new PdfArrayCompressedData(this.dictionary, ((buffer.Length != 2) || ((buffer[0] != 13) || (buffer[1] != 10))) ? buffer : new byte[0]).UncompressedData;
        }

        public PdfReaderDictionary Dictionary =>
            this.dictionary;

        public byte[] DecryptedData
        {
            get
            {
                byte[] data = base.Data;
                if ((data.Length != 0) && (this.encryptionInfo != null))
                {
                    data = this.encryptionInfo.DecryptData(data, this.dictionary.Number, this.dictionary.Generation);
                }
                return data;
            }
        }

        public byte[] UncompressedData =>
            this.GetUncompressedData(true);
    }
}

