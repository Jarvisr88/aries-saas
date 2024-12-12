namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfMovieAnnotation : PdfAnnotation
    {
        internal const string Type = "Movie";

        internal PdfMovieAnnotation(PdfPage page, PdfReaderDictionary dictionary) : base(page, dictionary)
        {
        }

        protected override string AnnotationType =>
            "Movie";
    }
}

