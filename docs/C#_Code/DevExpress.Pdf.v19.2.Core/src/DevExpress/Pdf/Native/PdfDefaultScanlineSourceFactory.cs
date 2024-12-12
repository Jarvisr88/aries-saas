namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfDefaultScanlineSourceFactory : IPdfImageScanlineSourceFactory
    {
        public IPdfImageScanlineSource CreateIndexedScanlineSource(IPdfImageScanlineSource source, int width, int height, int bitsPerComponent, byte[] lookupTable, int baseColorSpaceComponentsCount) => 
            new PdfIndexedColorSpaceImageScanlineSource(source, width, bitsPerComponent, lookupTable, baseColorSpaceComponentsCount);

        public IPdfImageScanlineSource CreateInterpolator(IPdfImageScanlineSource source, int targetWidth, int targetHeight, int sourceWidth, int sourceHeight, bool shouldInterpolate)
        {
            double num = ((double) targetWidth) / ((double) sourceWidth);
            double num2 = ((double) targetHeight) / ((double) sourceHeight);
            if (num > 1.0)
            {
                source = new PdfBilinearUpsamplingHorizontalInterpolator(source, targetWidth, sourceWidth);
            }
            else if (num < 1.0)
            {
                source = new PdfSuperSamplingHorizontalInterpolator(source, targetWidth, sourceWidth);
            }
            if (num2 > 1.0)
            {
                source = new PdfBilinearUpsamplingVerticalInterpolator(source, targetWidth, targetHeight, sourceHeight);
            }
            else if (num2 < 1.0)
            {
                source = new PdfSuperSamplingVerticalInterpolator(source, targetWidth, targetHeight, sourceHeight);
            }
            return source;
        }
    }
}

