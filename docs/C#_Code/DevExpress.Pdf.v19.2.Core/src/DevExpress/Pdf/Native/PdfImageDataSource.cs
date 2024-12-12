namespace DevExpress.Pdf.Native
{
    using System;

    public abstract class PdfImageDataSource : IPdfImageScanlineSource, IDisposable
    {
        protected const int BaseSourceComponentsCount = 3;
        private readonly IPdfImageScanlineSource source;
        private readonly int width;
        private readonly byte[] sourceScanline;

        protected PdfImageDataSource(IPdfImageScanlineSource source, int width)
        {
            this.source = source;
            this.width = width;
            this.sourceScanline = new byte[width * source.ComponentsCount];
        }

        public virtual void Dispose()
        {
            this.source.Dispose();
        }

        public abstract void FillBuffer(byte[] buffer, int scanlineCount);
        public void FillNextScanline(byte[] scanlineData)
        {
            this.FillBuffer(scanlineData, 1);
        }

        protected byte[] GetNextSourceScanline()
        {
            this.source.FillNextScanline(this.sourceScanline);
            return this.sourceScanline;
        }

        protected int Width =>
            this.width;

        public abstract int ComponentsCount { get; }

        public virtual bool HasAlpha =>
            false;

        protected int SourceComponentsCount =>
            this.source.ComponentsCount;

        protected bool SourceHasAlpha =>
            this.source.HasAlpha;
    }
}

