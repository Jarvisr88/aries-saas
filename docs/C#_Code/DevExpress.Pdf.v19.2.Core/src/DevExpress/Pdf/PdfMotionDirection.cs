namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    [PdfDefaultField(0)]
    public enum PdfMotionDirection
    {
        [PdfFieldName("I")]
        Inward = 0,
        [PdfFieldName("O")]
        Outward = 1
    }
}

