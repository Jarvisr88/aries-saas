namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfImageSet : PdfSpiderSet
    {
        internal PdfImageSet(PdfReaderDictionary dictionary) : base(dictionary)
        {
        }

        protected override string SubType =>
            "SIS";
    }
}

