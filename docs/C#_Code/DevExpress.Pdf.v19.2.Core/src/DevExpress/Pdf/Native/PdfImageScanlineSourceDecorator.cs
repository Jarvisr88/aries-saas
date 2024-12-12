namespace DevExpress.Pdf.Native
{
    using System;

    public abstract class PdfImageScanlineSourceDecorator : IPdfImageScanlineSource, IDisposable
    {
        private readonly IPdfImageScanlineSource source;
        private readonly int sourceWidth;

        protected PdfImageScanlineSourceDecorator(IPdfImageScanlineSource source, int sourceWidth)
        {
            this.source = source;
            this.sourceWidth = sourceWidth;
        }

        public void Dispose()
        {
            this.source.Dispose();
        }

        public abstract void FillNextScanline(byte[] scanlineData);

        public bool HasAlpha =>
            this.source.HasAlpha;

        public virtual int ComponentsCount =>
            this.source.ComponentsCount;

        protected int SourceWidth =>
            this.sourceWidth;

        protected IPdfImageScanlineSource Source =>
            this.source;
    }
}

