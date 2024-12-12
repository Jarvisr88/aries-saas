namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public enum PdfFormSubmissionType
    {
        [PdfFieldValue(0)]
        None = 0,
        [PdfFieldValue(1)]
        HttpGet = 1,
        [PdfFieldValue(2)]
        HttpPost = 2
    }
}

