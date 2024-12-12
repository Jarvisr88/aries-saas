namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;

    public static class ImageHelper
    {
        public static Image CreateTileBitmap(Image baseImage, Size clientSize, bool zoomImage);
        public static Image CreateTileImage(Image baseImage, Size pixClientSize);
        public static Image CreateTileImage(Image baseImage, Size pixClientSize, bool zoomImage);
        private static Image CreateTileMetafile(Metafile baseImage, SizeF clientSize, bool zoomImage);
        private static float GetZoomFactor(SizeF imageSize, SizeF clientSize);
    }
}

