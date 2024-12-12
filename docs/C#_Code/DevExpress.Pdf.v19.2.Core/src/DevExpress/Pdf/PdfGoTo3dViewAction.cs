namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfGoTo3dViewAction : PdfAction
    {
        internal const string Name = "GoTo3DView";

        internal PdfGoTo3dViewAction(PdfReaderDictionary dictionary) : base(dictionary)
        {
        }

        protected override string ActionType =>
            "GoTo3DView";
    }
}

