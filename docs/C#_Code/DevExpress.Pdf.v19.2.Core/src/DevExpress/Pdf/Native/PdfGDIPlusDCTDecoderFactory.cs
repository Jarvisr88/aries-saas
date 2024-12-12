namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfGDIPlusDCTDecoderFactory : IPdfDCTDecoderFactory
    {
        public IPdfImageScanlineSource CreateSource(byte[] imageData, PdfImage image) => 
            new PdfGDIPlusImageScanlineSource(imageData, image);
    }
}

