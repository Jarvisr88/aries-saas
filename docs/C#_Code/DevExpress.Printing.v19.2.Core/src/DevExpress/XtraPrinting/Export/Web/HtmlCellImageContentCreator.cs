namespace DevExpress.XtraPrinting.Export.Web
{
    using DevExpress.Printing;
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Drawing;
    using DevExpress.XtraPrinting.HtmlExport;
    using DevExpress.XtraPrinting.HtmlExport.Controls;
    using DevExpress.XtraPrinting.HtmlExport.Native;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class HtmlCellImageContentCreator
    {
        [ThreadStatic]
        private static System.Drawing.Image blankGif;
        protected IImageRepository imageRepository;

        public HtmlCellImageContentCreator(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
            this.<WriteSizeToAttributes>k__BackingField = imageRepository is MailImageRepository;
        }

        private static DXHtmlImage CloneHtmlImage(DXHtmlImage source)
        {
            DXHtmlImage image = new DXHtmlImage();
            foreach (string str in source.Attributes.Keys)
            {
                image.Attributes.Add(str, source.Attributes[str]);
            }
            foreach (string str2 in source.Style.Keys)
            {
                image.Style.Add(str2, source.Style[str2]);
            }
            image.Src = source.Src;
            return image;
        }

        public void CreateContent(DXHtmlContainerControl cell, System.Drawing.Image image, string imageSrc, ImageSizeMode sizeMode, ImageAlignment align, Rectangle bounds, Size imgSize, PaddingInfo padding)
        {
            Guard.ArgumentNotNull(cell, "cell");
            if (this.ValidateImageSrc(image, ref imageSrc))
            {
                PaddingInfo pixPadding = new PaddingInfo(padding, 96f);
                cell.Style.Add(DXHtmlTextWriterStyle.TextAlign, "left");
                cell.Style.Add(DXHtmlTextWriterStyle.VerticalAlign, "top");
                Size htmlImageSize = this.GetHtmlImageSize(sizeMode, bounds, pixPadding, imgSize);
                DXWebControlBase imageControl = this.CreateHtmlImage(image, htmlImageSize, imageSrc);
                Rectangle rectangle = this.GetClipBounds(sizeMode, align, pixPadding, bounds);
                ClipControl control = HtmlHelper.SetClip(cell, this.GetImagePosition(sizeMode, align, pixPadding, bounds, htmlImageSize), rectangle.Size);
                this.ProcessImage(control.InnerContainer, imageControl, image);
            }
        }

        protected virtual DXWebControlBase CreateHtmlImage(System.Drawing.Image image, Size htmlImageSize, string imageSrc)
        {
            DXHtmlImage control = new DXHtmlImage();
            this.SetControlSize(control, htmlImageSize);
            control.Attributes.Add("alt", string.Empty);
            control.Src = imageSrc;
            return control;
        }

        protected string GetBlankGifSrc() => 
            this.imageRepository.GetImageSource(BlankGif, false);

        private unsafe Rectangle GetClipBounds(ImageSizeMode sizeMode, ImageAlignment align, PaddingInfo pixPadding, Rectangle bounds)
        {
            if (((sizeMode == ImageSizeMode.Normal) && (align != ImageAlignment.MiddleCenter)) || (sizeMode == ImageSizeMode.AutoSize))
            {
                Rectangle* rectanglePtr1 = &bounds;
                rectanglePtr1.Width -= pixPadding.Right;
                Rectangle* rectanglePtr2 = &bounds;
                rectanglePtr2.Height -= pixPadding.Bottom;
            }
            return bounds;
        }

        private Size GetHtmlImageSize(ImageSizeMode sizeMode, Rectangle bounds, PaddingInfo pixPadding, Size imgSize)
        {
            if (imgSize == Size.Empty)
            {
                return Size.Empty;
            }
            Size size = imgSize;
            Rectangle rectangle = Rectangle.Round(pixPadding.Deflate(bounds, 96f));
            if ((sizeMode == ImageSizeMode.StretchImage) || ((sizeMode == ImageSizeMode.AutoSize) || (sizeMode == ImageSizeMode.Tile)))
            {
                size.Height = rectangle.Size.Height;
                size.Width = rectangle.Size.Width;
            }
            else if ((sizeMode == ImageSizeMode.ZoomImage) || ((sizeMode == ImageSizeMode.Squeeze) && ((size.Width > rectangle.Width) || (size.Height > rectangle.Height))))
            {
                size = MathMethods.ZoomInto((SizeF) rectangle.Size, (SizeF) imgSize).ToSize();
            }
            return size;
        }

        private Point GetImagePosition(ImageSizeMode sizeMode, ImageAlignment align, PaddingInfo pixPadding, Rectangle bounds, Size htmlImageSize)
        {
            switch (sizeMode)
            {
                case ImageSizeMode.Normal:
                case ImageSizeMode.ZoomImage:
                case ImageSizeMode.Squeeze:
                    switch (align)
                    {
                        case ImageAlignment.Default:
                            return ((sizeMode != ImageSizeMode.Normal) ? new Point((((bounds.Width - htmlImageSize.Width) - pixPadding.Right) + pixPadding.Left) / 2, (((bounds.Height - htmlImageSize.Height) - pixPadding.Bottom) + pixPadding.Top) / 2) : new Point(pixPadding.Left, pixPadding.Top));

                        case ImageAlignment.TopLeft:
                            return new Point(pixPadding.Left, pixPadding.Top);

                        case ImageAlignment.TopCenter:
                            return new Point((((bounds.Width - htmlImageSize.Width) - pixPadding.Right) + pixPadding.Left) / 2, pixPadding.Top);

                        case ImageAlignment.TopRight:
                            return new Point((bounds.Width - htmlImageSize.Width) - pixPadding.Right, pixPadding.Top);

                        case ImageAlignment.MiddleLeft:
                            return new Point(pixPadding.Left, (((bounds.Height - htmlImageSize.Height) - pixPadding.Bottom) + pixPadding.Top) / 2);

                        case ImageAlignment.MiddleCenter:
                            return new Point((((bounds.Width - htmlImageSize.Width) - pixPadding.Right) + pixPadding.Left) / 2, (((bounds.Height - htmlImageSize.Height) - pixPadding.Bottom) + pixPadding.Top) / 2);

                        case ImageAlignment.MiddleRight:
                            return new Point((bounds.Width - htmlImageSize.Width) - pixPadding.Right, (((bounds.Height - htmlImageSize.Height) - pixPadding.Bottom) + pixPadding.Top) / 2);

                        case ImageAlignment.BottomLeft:
                            return new Point(pixPadding.Left, (bounds.Height - htmlImageSize.Height) - pixPadding.Bottom);

                        case ImageAlignment.BottomCenter:
                            return new Point((((bounds.Width - htmlImageSize.Width) - pixPadding.Right) + pixPadding.Left) / 2, (bounds.Height - htmlImageSize.Height) - pixPadding.Bottom);

                        case ImageAlignment.BottomRight:
                            return new Point((bounds.Width - htmlImageSize.Width) - pixPadding.Right, (bounds.Height - htmlImageSize.Height) - pixPadding.Bottom);
                    }
                    return Point.Empty;

                case ImageSizeMode.StretchImage:
                case ImageSizeMode.AutoSize:
                case ImageSizeMode.Tile:
                    return new Point(pixPadding.Left, pixPadding.Top);

                case ImageSizeMode.CenterImage:
                    return new Point((((bounds.Width - htmlImageSize.Width) - pixPadding.Right) + pixPadding.Left) / 2, (((bounds.Height - htmlImageSize.Height) - pixPadding.Bottom) + pixPadding.Top) / 2);
            }
            return Point.Empty;
        }

        private static string GetSizeString(int size) => 
            new DXWebUnit((double) size, DXWebUnitType.Pixel).ToString();

        protected internal virtual string GetWatermarkImageSrc(ImageSource imageSource)
        {
            System.Drawing.Image img = ImageSource.GetImage(imageSource);
            return this.imageRepository.GetImageSource(img, false);
        }

        protected static bool IsPng(System.Drawing.Image image) => 
            (image != null) && (HtmlImageHelper.GetMimeType(image) == "png");

        protected virtual void ProcessImage(DXWebControlBase imgContainer, DXWebControlBase imageControl, System.Drawing.Image image)
        {
            if (!(imageControl is DXHtmlImage) || !IsPng(image))
            {
                imgContainer.Controls.Add(imageControl);
            }
            else
            {
                DXHtmlImage child = CloneHtmlImage((DXHtmlImage) imageControl);
                string str = DXHttpUtility.HtmlAttributeEncode(((DXHtmlImage) imageControl).Src);
                child.Style.Add("filter", "progid:DXImageTransform.Microsoft.AlphaImageLoader(src='" + str + "',sizingMethod='scale')");
                child.Src = this.GetBlankGifSrc();
                imgContainer.Controls.Add(new DXHtmlLiteralControl("<!--[if lt IE 7]>"));
                imgContainer.Controls.Add(child);
                imgContainer.Controls.Add(new DXHtmlLiteralControl("<div style=\"display:none\"><![endif]-->"));
                imgContainer.Controls.Add(imageControl);
                imgContainer.Controls.Add(new DXHtmlLiteralControl("<!--[if lt IE 7]></div><![endif]-->"));
            }
        }

        protected void SetControlSize(DXHtmlGenericControl control, Size htmlSize)
        {
            if (this.WriteSizeToAttributes)
            {
                control.Attributes.Add("width", htmlSize.Width.ToString());
                control.Attributes.Add("height", htmlSize.Height.ToString());
            }
            else
            {
                control.Style.Add("width", GetSizeString(htmlSize.Width));
                control.Style.Add("height", GetSizeString(htmlSize.Height));
            }
        }

        protected virtual bool ValidateImageSrc(System.Drawing.Image image, ref string imageSrc)
        {
            if (string.IsNullOrEmpty(imageSrc))
            {
                imageSrc = this.imageRepository.GetImageSource(image, false);
            }
            return !string.IsNullOrEmpty(imageSrc);
        }

        private static System.Drawing.Image BlankGif =>
            (blankGif != null) ? blankGif : (blankGif = ResourceImageHelper.CreateImageFromResources("Core.Images.blank.gif", typeof(ResFinder)));

        private bool WriteSizeToAttributes { get; }
    }
}

