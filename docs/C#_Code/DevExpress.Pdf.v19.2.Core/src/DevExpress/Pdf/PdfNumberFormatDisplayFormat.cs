namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    [PdfDefaultField(0)]
    public enum PdfNumberFormatDisplayFormat
    {
        [PdfFieldName("D")]
        ShowAsDecimal = 0,
        [PdfFieldName("F")]
        ShowAsFraction = 1,
        [PdfFieldName("R")]
        Round = 2,
        [PdfFieldName("T")]
        Truncate = 3
    }
}

