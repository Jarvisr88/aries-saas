namespace DevExpress.Pdf
{
    using System;

    public class PdfCharacterMapping
    {
        private readonly byte[] data;

        internal PdfCharacterMapping(byte[] data)
        {
            this.data = data;
        }

        public byte[] Data =>
            this.data;
    }
}

