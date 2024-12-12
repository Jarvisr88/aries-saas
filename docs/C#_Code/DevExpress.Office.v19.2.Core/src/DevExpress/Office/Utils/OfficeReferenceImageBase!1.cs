namespace DevExpress.Office.Utils
{
    using DevExpress.Office;
    using System;
    using System.Drawing;
    using System.IO;

    public abstract class OfficeReferenceImageBase<TInnerImage> : OfficeImage where TInnerImage: OfficeImage
    {
        protected OfficeReferenceImageBase()
        {
        }

        protected internal override Size CalculateImageSizeInModelUnits(DocumentModelUnitConverter unitConverter) => 
            this.InnerImage.CalculateImageSizeInModelUnits(unitConverter);

        public override bool CanGetImageBytes(OfficeImageFormat imageFormat) => 
            this.InnerImage.CanGetImageBytes(imageFormat);

        protected internal override void EnsureLoadComplete(TimeSpan timeout)
        {
            this.InnerImage.EnsureLoadComplete(timeout);
        }

        public override byte[] GetImageBytes(OfficeImageFormat imageFormat) => 
            this.InnerImage.GetImageBytes(imageFormat);

        public override byte[] GetImageBytesSafe(OfficeImageFormat imageFormat) => 
            this.InnerImage.GetImageBytesSafe(imageFormat);

        protected internal override Stream GetImageBytesStream(OfficeImageFormat imageFormat) => 
            this.InnerImage.GetImageBytesStream(imageFormat);

        public override Stream GetImageBytesStreamSafe(OfficeImageFormat imageFormat) => 
            this.InnerImage.GetImageBytesStreamSafe(imageFormat);

        protected internal override Size UnitsToDocuments(Size sizeInUnits) => 
            this.InnerImage.UnitsToDocuments(sizeInUnits);

        protected internal override Size UnitsToHundredthsOfMillimeter(Size sizeInUnits) => 
            this.InnerImage.UnitsToHundredthsOfMillimeter(sizeInUnits);

        protected internal override Size UnitsToPixels(Size sizeInUnits) => 
            this.InnerImage.UnitsToPixels(sizeInUnits);

        protected internal override Size UnitsToTwips(Size sizeInUnits) => 
            this.InnerImage.UnitsToTwips(sizeInUnits);

        protected internal abstract TInnerImage InnerImage { get; }

        public override Image NativeImage =>
            this.InnerImage.NativeImage;

        public override Size SizeInOriginalUnits =>
            this.InnerImage.SizeInOriginalUnits;

        public override OfficeImageFormat RawFormat =>
            this.InnerImage.RawFormat;

        public override OfficePixelFormat PixelFormat =>
            this.InnerImage.PixelFormat;

        public override int PaletteLength =>
            this.InnerImage.PaletteLength;

        public override float HorizontalResolution =>
            this.InnerImage.HorizontalResolution;

        public override float VerticalResolution =>
            this.InnerImage.VerticalResolution;

        public override bool IsLoaded =>
            this.InnerImage.IsLoaded;

        public override int ImageCacheKey =>
            this.InnerImage.ImageCacheKey;

        public override OfficeNativeImage EncapsulatedOfficeNativeImage =>
            this.InnerImage.EncapsulatedOfficeNativeImage;
    }
}

