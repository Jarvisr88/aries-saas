namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfSoundAction : PdfAction
    {
        internal const string Name = "Sound";

        internal PdfSoundAction(PdfReaderDictionary dictionary) : base(dictionary)
        {
        }

        protected override string ActionType =>
            "Sound";
    }
}

