namespace DevExpress.Pdf.Native
{
    using DevExpress.DirectX.NativeInterop.WIC;
    using System;

    public class PdfWICScanlineSourceFactory : IPdfImageScanlineSourceFactory
    {
        private readonly PdfDefaultScanlineSourceFactory defaultFactory = new PdfDefaultScanlineSourceFactory();
        private WICImagingFactory factory = WICImagingFactory.Instance;

        public IPdfImageScanlineSource CreateIndexedScanlineSource(IPdfImageScanlineSource source, int width, int height, int bitsPerComponent, byte[] lookupTable, int baseColorSpaceComponentsCount) => 
            ((baseColorSpaceComponentsCount != 3) || source.HasAlpha) ? this.defaultFactory.CreateIndexedScanlineSource(source, width, height, bitsPerComponent, lookupTable, baseColorSpaceComponentsCount) : new PdfWICIndexedImageScanlineSource(this.factory, source, width, height, bitsPerComponent, lookupTable);

        public IPdfImageScanlineSource CreateInterpolator(IPdfImageScanlineSource source, int targetWidth, int targetHeight, int sourceWidth, int sourceHeight, bool shouldInterpolate)
        {
            switch (source.ComponentsCount)
            {
                case 1:
                case 3:
                    break;

                case 4:
                    if (source.HasAlpha)
                    {
                        break;
                    }
                    goto TR_0000;

                default:
                    goto TR_0000;
            }
            return new PdfWICImageInterpolator(source, targetWidth, targetHeight, sourceWidth, sourceHeight, this.factory, shouldInterpolate);
        TR_0000:
            return this.defaultFactory.CreateInterpolator(source, targetWidth, targetHeight, sourceWidth, sourceHeight, shouldInterpolate);
        }
    }
}

