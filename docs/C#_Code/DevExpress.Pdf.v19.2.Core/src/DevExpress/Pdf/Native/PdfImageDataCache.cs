namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfImageDataCache : PdfImageCache<PdfImageDataCacheItem>
    {
        public PdfImageDataCache(long capacity) : base(capacity)
        {
        }

        protected override PdfImageCache<PdfImageDataCacheItem>.ImageCacheItem CreateValue(PdfImage image, PdfImageParameters imageParameters)
        {
            PdfImageData actualData = image.GetActualData(imageParameters, true);
            long size = actualData.Stride * actualData.Height;
            if (size > base.Capacity)
            {
                return new PdfImageCache<PdfImageDataCacheItem>.ImageCacheItem(new PdfImageDataSourceCacheItem(actualData, image), image, imageParameters, size);
            }
            try
            {
                byte[] imageRaster = GetImageRaster(actualData);
                return new PdfImageCache<PdfImageDataCacheItem>.ImageCacheItem(new PdfImageDataRasterCacheItem(actualData, image, imageRaster), image, imageParameters, size);
            }
            catch (OutOfMemoryException)
            {
                return new PdfImageCache<PdfImageDataCacheItem>.ImageCacheItem(new PdfImageDataSourceCacheItem(actualData, image), image, imageParameters, size);
            }
        }

        private static byte[] GetImageRaster(PdfImageData imageData)
        {
            byte[] buffer = new byte[imageData.Stride * imageData.Height];
            using (PdfImageDataSource source = imageData.Data)
            {
                source.FillBuffer(buffer, imageData.Height);
            }
            return buffer;
        }
    }
}

