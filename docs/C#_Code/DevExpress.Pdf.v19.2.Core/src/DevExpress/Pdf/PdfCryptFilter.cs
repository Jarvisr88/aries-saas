namespace DevExpress.Pdf
{
    using System;

    public class PdfCryptFilter : PdfFilter
    {
        internal const string Name = "Crypt";

        internal PdfCryptFilter()
        {
        }

        protected internal override byte[] Decode(byte[] data) => 
            data;

        protected internal override string FilterName =>
            "Crypt";
    }
}

