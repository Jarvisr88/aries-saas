namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfAlphaSoftMask : PdfCustomSoftMask
    {
        internal const string Name = "Alpha";

        internal PdfAlphaSoftMask(PdfReaderDictionary dictionary) : base(dictionary)
        {
        }

        protected override string ActualName =>
            "Alpha";
    }
}

