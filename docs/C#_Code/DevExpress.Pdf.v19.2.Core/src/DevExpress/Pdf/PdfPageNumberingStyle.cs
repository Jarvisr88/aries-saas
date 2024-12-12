namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    [PdfDefaultField(0)]
    public enum PdfPageNumberingStyle
    {
        None = 0,
        [PdfFieldName("D")]
        DecimalArabic = 1,
        [PdfFieldName("R")]
        UppercaseRoman = 2,
        [PdfFieldName("r")]
        LowercaseRoman = 3,
        [PdfFieldName("A")]
        UppercaseLetters = 4,
        [PdfFieldName("a")]
        LowercaseLetters = 5
    }
}

