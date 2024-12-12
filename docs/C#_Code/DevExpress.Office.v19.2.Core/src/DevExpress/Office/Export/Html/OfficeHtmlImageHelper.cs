namespace DevExpress.Office.Export.Html
{
    using DevExpress.Office;
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Runtime.CompilerServices;

    public class OfficeHtmlImageHelper
    {
        private readonly IDocumentModel documentModel;
        private readonly DocumentModelUnitConverter unitConverter;

        public OfficeHtmlImageHelper(IDocumentModel documentModel, DocumentModelUnitConverter unitConverter)
        {
            Guard.ArgumentNotNull(documentModel, "documentModel");
            Guard.ArgumentNotNull(unitConverter, "unitConverter");
            this.documentModel = documentModel;
            this.unitConverter = unitConverter;
            this.CalculateDefaultImageResolution();
        }

        public void ApplyImageProperties(WebImageControl imageControl, OfficeImage image, Size actualSize, IOfficeImageRepository imageRepository, bool disposeConvertedImagesImmediately)
        {
            this.ApplyImageProperties(imageControl, image, actualSize, imageRepository, disposeConvertedImagesImmediately, true);
        }

        public void ApplyImageProperties(WebImageControl imageControl, OfficeImage image, Size actualSize, IOfficeImageRepository imageRepository, bool disposeConvertedImagesImmediately, bool correctWidth)
        {
            this.ApplyImageProperties(imageControl, image, actualSize, imageRepository, disposeConvertedImagesImmediately, correctWidth, false);
        }

        public void ApplyImageProperties(WebImageControl imageControl, OfficeImage image, Size actualSize, IOfficeImageRepository imageRepository, bool disposeConvertedImagesImmediately, bool correctWidth, bool alwaysWriteImageSize)
        {
            this.ApplyImageProperties(imageControl, image, actualSize, imageRepository, disposeConvertedImagesImmediately, correctWidth, alwaysWriteImageSize, false);
        }

        public void ApplyImageProperties(WebImageControl imageControl, OfficeImage image, Size actualSize, IOfficeImageRepository imageRepository, bool disposeConvertedImagesImmediately, bool correctWidth, bool alwaysWriteImageSize, bool keepImageSize)
        {
            UriBasedOfficeImageBase base2 = image as UriBasedOfficeImageBase;
            bool flag = false;
            if (!this.ShouldConvertImage(image))
            {
                imageControl.ImageUrl = imageRepository.GetImageSource(image, false);
                if ((base2 == null) || ((base2.PixelTargetWidth != 0) || (base2.PixelTargetHeight != 0)))
                {
                    flag = true;
                }
            }
            else if (!keepImageSize)
            {
                imageControl.ImageUrl = imageRepository.GetImageSource(this.GetHtmlImage(image, actualSize, correctWidth), disposeConvertedImagesImmediately);
            }
            else
            {
                imageControl.ImageUrl = imageRepository.GetImageSource(this.documentModel.CreateImage(new Bitmap(image.NativeImage)), disposeConvertedImagesImmediately);
                flag = true;
            }
            if (alwaysWriteImageSize | flag)
            {
                imageControl.Attributes.Add("width", this.unitConverter.RoundModelUnitsToPixels(actualSize.Width, this.HorizontalResolution).ToString());
                imageControl.Attributes.Add("height", this.unitConverter.RoundModelUnitsToPixels(actualSize.Height, this.VerticalResolution).ToString());
            }
            else if (base2 != null)
            {
                if (base2.PixelTargetWidth > 0)
                {
                    imageControl.Attributes.Add("width", this.unitConverter.RoundModelUnitsToPixels(actualSize.Width, this.HorizontalResolution).ToString());
                }
                if (base2.PixelTargetHeight > 0)
                {
                    imageControl.Attributes.Add("height", this.unitConverter.RoundModelUnitsToPixels(actualSize.Height, this.VerticalResolution).ToString());
                }
            }
        }

        private void CalculateDefaultImageResolution()
        {
            using (Bitmap bitmap = new Bitmap(1, 1))
            {
                this.HorizontalResolution = (bitmap.HorizontalResolution > 0f) ? bitmap.HorizontalResolution : 96f;
                this.VerticalResolution = (bitmap.VerticalResolution > 0f) ? bitmap.VerticalResolution : 96f;
            }
        }

        private Bitmap ConvertToBitmap(Image image, Color backgroundColor, int resolution)
        {
            Metafile metafile = image as Metafile;
            if (metafile == null)
            {
                return (DXColor.IsTransparentColor(backgroundColor) ? ((Bitmap) image) : BitmapCreator.CreateBitmapWithResolutionLimit(image, backgroundColor));
            }
            float toDpi = Math.Max((float) resolution, 96f);
            int width = GraphicsUnitConverter.Convert(metafile.Width, metafile.HorizontalResolution, toDpi);
            int height = GraphicsUnitConverter.Convert(metafile.Height, metafile.VerticalResolution, toDpi);
            int num4 = 0xc7ff38;
            if ((width * height) > num4)
            {
                double a = Math.Sqrt(((double) num4) / (((double) width) / ((double) height)));
                Bitmap bitmap2 = new Bitmap((int) Math.Ceiling((double) (((double) num4) / a)), (int) Math.Ceiling(a));
                using (Graphics graphics = Graphics.FromImage(bitmap2))
                {
                    if (!DXColor.IsEmpty(backgroundColor))
                    {
                        graphics.Clear(backgroundColor);
                    }
                    graphics.DrawImage(metafile, new Rectangle(Point.Empty, bitmap2.Size));
                }
                return bitmap2;
            }
            Bitmap bitmap = new Bitmap(width, height);
            bitmap.SetResolution(toDpi, toDpi);
            using (Graphics graphics2 = Graphics.FromImage(bitmap))
            {
                if (!DXColor.IsEmpty(backgroundColor))
                {
                    graphics2.Clear(backgroundColor);
                }
                graphics2.DrawImage(metafile, 0, 0);
            }
            return bitmap;
        }

        private OfficeImage CreateBitmapFromSourceImage(OfficeImage image, int width, int height, bool correctWidth)
        {
            Bitmap bitmap = new Bitmap(Math.Max(1, correctWidth ? (width - 1) : width), Math.Max(1, height));
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                Image nativeImage = image.NativeImage;
                Metafile metafile = nativeImage as Metafile;
                if (metafile != null)
                {
                    nativeImage = this.ConvertToBitmap(metafile, Color.Empty, 300);
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                }
                graphics.Clear(DXColor.Transparent);
                graphics.DrawImage(nativeImage, 0, 0, width, height);
            }
            return this.documentModel.CreateImage(bitmap);
        }

        private OfficeImage GetHtmlImage(OfficeImage image, Size actualSize, bool correctWidth)
        {
            int width = Math.Max(1, this.unitConverter.RoundModelUnitsToPixels(actualSize.Width, this.HorizontalResolution));
            OfficeImage image2 = this.CreateBitmapFromSourceImage(image, width, Math.Max(1, this.unitConverter.RoundModelUnitsToPixels(actualSize.Height, this.VerticalResolution)), correctWidth);
            image2.Uri = image.Uri;
            return image2;
        }

        private bool ShouldConvertImage(OfficeImage image)
        {
            switch (image.RawFormat)
            {
                case OfficeImageFormat.None:
                case OfficeImageFormat.Emf:
                case OfficeImageFormat.Exif:
                case OfficeImageFormat.Icon:
                case OfficeImageFormat.MemoryBmp:
                case OfficeImageFormat.Wmf:
                    return true;
            }
            return false;
        }

        public float HorizontalResolution { get; set; }

        public float VerticalResolution { get; set; }
    }
}

