namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    [PdfDefaultField(0)]
    public enum PdfRichMediaActivationCondition
    {
        [PdfFieldName("XA")]
        Explicitly = 0,
        [PdfFieldName("PO")]
        PageBecomeCurrent = 1,
        [PdfFieldName("PV")]
        PageBecomeVisible = 2
    }
}

