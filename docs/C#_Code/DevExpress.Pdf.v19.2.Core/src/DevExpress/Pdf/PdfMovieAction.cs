namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfMovieAction : PdfAction
    {
        internal const string Name = "Movie";

        internal PdfMovieAction(PdfReaderDictionary dictionary) : base(dictionary)
        {
        }

        protected override string ActionType =>
            "Movie";
    }
}

