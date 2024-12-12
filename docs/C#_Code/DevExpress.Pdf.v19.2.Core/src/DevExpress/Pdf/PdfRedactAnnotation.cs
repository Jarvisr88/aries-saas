namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfRedactAnnotation : PdfMarkupAnnotation
    {
        internal const string Type = "Redact";

        internal PdfRedactAnnotation(PdfPage page, PdfReaderDictionary dictionary) : base(page, dictionary)
        {
        }

        protected override string AnnotationType =>
            "Redact";
    }
}

