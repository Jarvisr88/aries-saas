namespace DevExpress.XtraPrinting.Export
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct TableCellImageInfo
    {
        private ImageSizeMode sizeMode;
        private ImageAlignment imageAlignment;
        private Size imageSize;
        public TableCellImageInfo(Size imageSize, ImageSizeMode sizeMode, ImageAlignment imageAlignment)
        {
            this.imageSize = imageSize;
            this.sizeMode = sizeMode;
            this.imageAlignment = imageAlignment;
        }

        public ImageSizeMode SizeMode =>
            this.sizeMode;
        public ImageAlignment Alignment =>
            this.imageAlignment;
        public Size ImageSize =>
            this.imageSize;
    }
}

