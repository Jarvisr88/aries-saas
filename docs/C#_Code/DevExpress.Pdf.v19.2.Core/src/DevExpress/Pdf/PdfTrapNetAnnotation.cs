namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfTrapNetAnnotation : PdfAnnotation
    {
        internal const string Type = "TrapNet";

        internal PdfTrapNetAnnotation(PdfPage page, PdfReaderDictionary dictionary) : base(page, dictionary)
        {
        }

        protected override string AnnotationType =>
            "TrapNet";
    }
}

