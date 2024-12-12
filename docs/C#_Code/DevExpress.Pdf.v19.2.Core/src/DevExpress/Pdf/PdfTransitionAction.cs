namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfTransitionAction : PdfAction
    {
        internal const string Name = "Trans";

        internal PdfTransitionAction(PdfReaderDictionary dictionary) : base(dictionary)
        {
        }

        protected override string ActionType =>
            "Trans";
    }
}

