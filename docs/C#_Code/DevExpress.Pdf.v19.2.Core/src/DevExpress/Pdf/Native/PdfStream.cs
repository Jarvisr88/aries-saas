namespace DevExpress.Pdf.Native
{
    using System;

    public abstract class PdfStream
    {
        public const string DictionaryLengthKey = "Length";
        private readonly byte[] data;

        protected PdfStream(byte[] data)
        {
            this.data = data;
        }

        public byte[] Data =>
            this.data;
    }
}

