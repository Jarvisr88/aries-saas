namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    [PdfDefaultField(0)]
    public enum PdfPrintMode
    {
        None,
        Simplex,
        DuplexFlipShortEdge,
        DuplexFlipLongEdge
    }
}

