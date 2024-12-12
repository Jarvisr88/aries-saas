namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    [PdfDefaultField(0)]
    public enum PdfOptionalContentState
    {
        [PdfFieldName("ON")]
        On = 0,
        [PdfFieldName("OFF")]
        Off = 1,
        Unchanged = 2
    }
}

