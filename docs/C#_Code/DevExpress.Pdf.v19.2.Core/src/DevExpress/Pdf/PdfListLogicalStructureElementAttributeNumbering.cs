namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    [PdfDefaultField(0)]
    public enum PdfListLogicalStructureElementAttributeNumbering
    {
        None = 0,
        [PdfFieldName("Disc")]
        SolidCircularBullet = 1,
        [PdfFieldName("Circle")]
        OpenCircularBullet = 2,
        [PdfFieldName("Square")]
        SolidSquareBullet = 3,
        Decimal = 4,
        UpperRoman = 5,
        LowerRoman = 6,
        UpperAlpha = 7,
        LowerAlpha = 8
    }
}

