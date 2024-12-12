namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public class PdfArrayCompressedData : PdfCompressedData
    {
        private readonly byte[] data;

        public PdfArrayCompressedData(byte[] data) : this(filterArray1, PdfFlateEncoder.Encode(data))
        {
            PdfFilter[] filterArray1 = new PdfFilter[] { new PdfFlateDecodeFilter(null) };
        }

        public PdfArrayCompressedData(PdfReaderStream stream) : this(stream.Dictionary, stream.DecryptedData)
        {
        }

        public PdfArrayCompressedData(PdfReaderDictionary dictionary, byte[] data) : base(dictionary)
        {
            this.data = data;
        }

        public PdfArrayCompressedData(IList<PdfFilter> filters, byte[] data) : base(filters)
        {
            this.data = data;
        }

        public override IPdfWritableObject CreateWritableObject(PdfWriterDictionary dictionary) => 
            this.CreateWriterStream(dictionary);

        public PdfWriterStream CreateWriterStream(PdfWriterDictionary dictionary)
        {
            base.AddFilters(dictionary);
            return new PdfWriterStream(dictionary, this.data);
        }

        public override byte[] Data =>
            this.data;
    }
}

