namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public interface IPdfDCTDecoderFactory
    {
        IPdfImageScanlineSource CreateSource(byte[] imageData, PdfImage image);
    }
}

