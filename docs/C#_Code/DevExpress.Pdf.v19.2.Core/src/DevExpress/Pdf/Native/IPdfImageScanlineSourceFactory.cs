namespace DevExpress.Pdf.Native
{
    using System;

    public interface IPdfImageScanlineSourceFactory
    {
        IPdfImageScanlineSource CreateIndexedScanlineSource(IPdfImageScanlineSource source, int width, int height, int bitsPerComponent, byte[] lookupTable, int baseColorSpaceComponentsCount);
        IPdfImageScanlineSource CreateInterpolator(IPdfImageScanlineSource source, int targetWidth, int targetHeight, int sourceWidth, int sourceHeight, bool shouldInterpolate);
    }
}

