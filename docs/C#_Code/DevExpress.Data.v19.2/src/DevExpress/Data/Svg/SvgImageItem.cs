namespace DevExpress.Data.Svg
{
    using System;
    using System.Drawing;

    [FormatElement("image")]
    public class SvgImageItem : SvgSupportRectangle
    {
        private string imageFormat;
        private byte[] imageData;

        public SvgImageItem();
        public SvgImageItem(Image image, double x, double y, double width, double height);
        public override T CreatePlatformItem<T>(ISvgElementFactory<T> factory);
        protected override void ExportFields(SvgElementDataExportAgent dataAgent);
        private string GetImageMimeType(Image image);

        public string ImageFormat { get; }

        public byte[] ImageData { get; }
    }
}

