namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfImageDataSourceCacheItem : PdfImageDataCacheItem
    {
        private readonly PdfImageDataSource dataSource;

        public PdfImageDataSourceCacheItem(PdfImageData imageData, PdfImage image) : base(imageData, image)
        {
            this.dataSource = imageData.Data;
        }

        public override void Dispose()
        {
            this.dataSource.Dispose();
        }

        public override void DrawImage(PdfLegacyCommandInterpreter renderer)
        {
            renderer.DrawImage(this, this.dataSource);
        }
    }
}

