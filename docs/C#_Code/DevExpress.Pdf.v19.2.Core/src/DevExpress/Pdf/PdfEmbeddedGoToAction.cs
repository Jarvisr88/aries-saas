namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfEmbeddedGoToAction : PdfAction
    {
        internal const string Name = "GoToE";

        internal PdfEmbeddedGoToAction(PdfReaderDictionary dictionary) : base(dictionary)
        {
        }

        protected override string ActionType =>
            "GoToE";
    }
}

