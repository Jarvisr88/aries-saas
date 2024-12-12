namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    [PdfDefaultField(0)]
    public enum PdfTextJustification
    {
        [PdfFieldValue(0)]
        LeftJustified = 0,
        [PdfFieldValue(1)]
        Centered = 1,
        [PdfFieldValue(2)]
        RightJustified = 2
    }
}

