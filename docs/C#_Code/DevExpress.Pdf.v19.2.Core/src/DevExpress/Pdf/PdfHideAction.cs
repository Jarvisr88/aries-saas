namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfHideAction : PdfAction
    {
        internal const string Name = "Hide";

        internal PdfHideAction(PdfReaderDictionary dictionary) : base(dictionary)
        {
        }

        protected override string ActionType =>
            "Hide";
    }
}

