namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfScreenAnnotation : PdfAnnotation
    {
        internal const string Type = "Screen";

        internal PdfScreenAnnotation(PdfPage page, PdfReaderDictionary dictionary) : base(page, dictionary)
        {
        }

        protected override string AnnotationType =>
            "Screen";
    }
}

