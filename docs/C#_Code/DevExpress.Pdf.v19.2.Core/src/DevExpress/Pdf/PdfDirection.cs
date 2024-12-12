namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    [PdfDefaultField(0)]
    public enum PdfDirection
    {
        [PdfFieldName("L2R")]
        LeftToRight = 0,
        [PdfFieldName("R2L")]
        RightToLeft = 1
    }
}

