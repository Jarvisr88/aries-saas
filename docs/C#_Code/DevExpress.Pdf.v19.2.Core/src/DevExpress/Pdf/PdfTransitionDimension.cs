namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    [PdfDefaultField(0)]
    public enum PdfTransitionDimension
    {
        [PdfFieldName("H")]
        Horizontal = 0,
        [PdfFieldName("V")]
        Vertical = 1
    }
}

