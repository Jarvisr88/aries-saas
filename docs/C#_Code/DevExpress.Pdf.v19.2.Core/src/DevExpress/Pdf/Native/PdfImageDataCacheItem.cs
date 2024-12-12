namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public abstract class PdfImageDataCacheItem : IDisposable
    {
        private readonly PdfImageParameters parameters;
        private readonly int stride;
        private readonly PdfPixelFormat pixelFormat;
        private readonly PdfImageColor[] palette;
        private readonly PdfImage image;

        protected PdfImageDataCacheItem(PdfImageData imageData, PdfImage image)
        {
            this.parameters = new PdfImageParameters(imageData.Width, imageData.Height, true);
            this.stride = imageData.Stride;
            this.pixelFormat = imageData.PixelFormat;
            this.palette = imageData.Palette;
            this.image = image;
        }

        public virtual void Dispose()
        {
        }

        public abstract void DrawImage(PdfLegacyCommandInterpreter interpreter);

        public PdfImageParameters Parameters =>
            this.parameters;

        public int Stride =>
            this.stride;

        public PdfPixelFormat PixelFormat =>
            this.pixelFormat;

        public PdfImageColor[] Palette =>
            this.palette;

        public PdfImage Image =>
            this.image;
    }
}

