namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    [PdfDefaultField(0)]
    public enum PdfNumberFormatLabelPosition
    {
        [PdfFieldName("S")]
        Suffix = 0,
        [PdfFieldName("P")]
        Prefix = 1
    }
}

