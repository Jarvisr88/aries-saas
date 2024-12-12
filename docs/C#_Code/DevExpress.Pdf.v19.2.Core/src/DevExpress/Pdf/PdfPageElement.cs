namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    [PdfDefaultField(0, false)]
    public enum PdfPageElement
    {
        None = 0,
        [PdfFieldName("HF")]
        HeaderFooter = 1,
        [PdfFieldName("FG")]
        Foreground = 2,
        [PdfFieldName("BG")]
        Background = 3,
        [PdfFieldName("L")]
        Logo = 4
    }
}

