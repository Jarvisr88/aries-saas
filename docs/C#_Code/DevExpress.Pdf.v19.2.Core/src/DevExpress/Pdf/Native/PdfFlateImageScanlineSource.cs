namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfFlateImageScanlineSource : IPdfImageScanlineSource, IDisposable
    {
        private readonly IPdfFlateDataSource source;
        private readonly PdfImageScanlineDecoder scanlineDecoder;
        private readonly byte[] rowBuffer;

        public PdfFlateImageScanlineSource(IPdfFlateDataSource source, PdfImageScanlineDecoder scanlineDecoder)
        {
            this.scanlineDecoder = scanlineDecoder;
            this.source = source;
            this.rowBuffer = new byte[scanlineDecoder.Stride];
        }

        public void Dispose()
        {
            this.source.Dispose();
        }

        public void FillNextScanline(byte[] scanlineData)
        {
            this.source.FillBuffer(this.rowBuffer);
            this.scanlineDecoder.FillNextScanline(scanlineData, this.rowBuffer, 0);
        }

        public int ComponentsCount =>
            this.scanlineDecoder.ComponentsCount + (this.HasAlpha ? 1 : 0);

        public bool HasAlpha =>
            this.scanlineDecoder.IsColorKeyPresent;
    }
}

