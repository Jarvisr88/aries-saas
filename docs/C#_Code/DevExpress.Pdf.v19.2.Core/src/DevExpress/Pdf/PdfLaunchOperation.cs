namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    [PdfDefaultField(0, false)]
    public enum PdfLaunchOperation
    {
        None = 0,
        [PdfFieldName("open")]
        Open = 1,
        [PdfFieldName("print")]
        Print = 2
    }
}

