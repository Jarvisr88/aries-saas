namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfSoundAnnotation : PdfMarkupAnnotation
    {
        internal const string Type = "Sound";

        internal PdfSoundAnnotation(PdfPage page, PdfReaderDictionary dictionary) : base(page, dictionary)
        {
        }

        protected override string AnnotationType =>
            "Sound";
    }
}

