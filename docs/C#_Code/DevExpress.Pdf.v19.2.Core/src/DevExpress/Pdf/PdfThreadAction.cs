namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfThreadAction : PdfAction
    {
        internal const string Name = "Thread";

        internal PdfThreadAction(PdfReaderDictionary dictionary) : base(dictionary)
        {
        }

        protected override string ActionType =>
            "Thread";
    }
}

