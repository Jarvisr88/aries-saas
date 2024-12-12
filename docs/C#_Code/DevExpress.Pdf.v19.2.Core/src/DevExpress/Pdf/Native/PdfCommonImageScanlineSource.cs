namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfCommonImageScanlineSource : IPdfImageScanlineSource, IDisposable
    {
        private readonly byte[] sourceData;
        private readonly PdfImageScanlineDecoder scanlineDecoder;
        private int sourceOffset;

        private PdfCommonImageScanlineSource(byte[] sourceData, PdfImageScanlineDecoder scanlineDecoder)
        {
            this.sourceData = sourceData;
            this.scanlineDecoder = scanlineDecoder;
        }

        public static IPdfImageScanlineSource CreateImageScanlineSource(byte[] sourceData, PdfImage image, int componentsCount) => 
            new PdfCommonImageScanlineSource(sourceData, PdfImageScanlineDecoder.CreateImageScanlineDecoder(image, componentsCount));

        public void Dispose()
        {
        }

        public void FillNextScanline(byte[] scanlineData)
        {
            this.scanlineDecoder.FillNextScanline(scanlineData, this.sourceData, this.sourceOffset);
            this.sourceOffset += this.scanlineDecoder.Stride;
        }

        public int ComponentsCount =>
            this.scanlineDecoder.ComponentsCount + (this.HasAlpha ? 1 : 0);

        public bool HasAlpha =>
            this.scanlineDecoder.IsColorKeyPresent;
    }
}

