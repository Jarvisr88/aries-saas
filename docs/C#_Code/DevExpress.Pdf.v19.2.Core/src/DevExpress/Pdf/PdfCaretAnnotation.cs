namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfCaretAnnotation : PdfMarkupAnnotation
    {
        internal const string Type = "Caret";

        internal PdfCaretAnnotation(PdfPage page, PdfReaderDictionary dictionary) : base(page, dictionary)
        {
        }

        protected override string AnnotationType =>
            "Caret";
    }
}

