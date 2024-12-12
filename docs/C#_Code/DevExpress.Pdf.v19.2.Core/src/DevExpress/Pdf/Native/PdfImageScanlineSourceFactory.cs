namespace DevExpress.Pdf.Native
{
    using System;
    using System.Threading;

    public static class PdfImageScanlineSourceFactory
    {
        private static readonly ThreadLocal<IPdfImageScanlineSourceFactory> factory = new ThreadLocal<IPdfImageScanlineSourceFactory>(new Func<IPdfImageScanlineSourceFactory>(PdfImageScanlineSourceFactory.CreateFactory));

        private static IPdfImageScanlineSourceFactory CreateFactory()
        {
            try
            {
                return ((Environment.OSVersion.Version.Major >= 6) ? ((IPdfImageScanlineSourceFactory) new PdfWICScanlineSourceFactory()) : ((IPdfImageScanlineSourceFactory) new PdfDefaultScanlineSourceFactory()));
            }
            catch
            {
                return new PdfDefaultScanlineSourceFactory();
            }
        }

        public static IPdfImageScanlineSource CreateIndexedScanlineSource(IPdfImageScanlineSource source, int width, int height, int bitsPerComponent, byte[] lookupTable, int baseColorSpaceComponentsCount) => 
            factory.Value.CreateIndexedScanlineSource(source, width, height, bitsPerComponent, lookupTable, baseColorSpaceComponentsCount);

        public static IPdfImageScanlineSource CreateInterpolator(IPdfImageScanlineSource source, int targetWidth, int targetHeight, int sourceWidth, int sourceHeight, bool shouldInterpolate) => 
            factory.Value.CreateInterpolator(source, targetWidth, targetHeight, sourceWidth, sourceHeight, shouldInterpolate);
    }
}

