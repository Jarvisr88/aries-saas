namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfRenditionAction : PdfAction
    {
        internal const string Name = "Rendition";

        internal PdfRenditionAction(PdfReaderDictionary dictionary) : base(dictionary)
        {
        }

        protected override string ActionType =>
            "Rendition";
    }
}

