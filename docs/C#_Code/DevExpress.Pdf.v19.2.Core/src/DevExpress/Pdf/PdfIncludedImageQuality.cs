namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    [PdfDefaultField(2)]
    public enum PdfIncludedImageQuality
    {
        [PdfFieldValue(1)]
        Low = 0,
        [PdfFieldValue(2)]
        Normal = 1,
        [PdfFieldValue(3)]
        High = 2
    }
}

