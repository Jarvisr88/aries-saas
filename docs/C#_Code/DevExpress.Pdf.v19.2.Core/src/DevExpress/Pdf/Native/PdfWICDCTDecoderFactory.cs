namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfWICDCTDecoderFactory : IPdfDCTDecoderFactory
    {
        public IPdfImageScanlineSource CreateSource(byte[] imageData, PdfImage image) => 
            PdfWICJpegScanlineSource.CreateScanlineSource(image, imageData);
    }
}

