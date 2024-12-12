namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    [PdfDefaultField(1)]
    public enum PdfViewArea
    {
        MediaBox,
        CropBox,
        BleedBox,
        TrimBox,
        ArtBox
    }
}

