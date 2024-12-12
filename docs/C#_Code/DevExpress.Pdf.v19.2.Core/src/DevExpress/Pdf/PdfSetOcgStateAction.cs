namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfSetOcgStateAction : PdfAction
    {
        internal const string Name = "SetOCGState";

        internal PdfSetOcgStateAction(PdfReaderDictionary dictionary) : base(dictionary)
        {
        }

        protected override string ActionType =>
            "SetOCGState";
    }
}

