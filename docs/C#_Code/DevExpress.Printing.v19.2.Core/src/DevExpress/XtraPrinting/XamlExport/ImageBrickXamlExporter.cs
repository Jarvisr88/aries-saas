namespace DevExpress.XtraPrinting.XamlExport
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;

    internal class ImageBrickXamlExporter : VisualBrickXamlExporter
    {
        private SizeF CalculateImageSize(ImageBrick imageBrick, bool needSqueeze, float scaleFactor)
        {
            if ((imageBrick.SizeMode == ImageSizeMode.Normal) || ((imageBrick.SizeMode == ImageSizeMode.CenterImage) || (!needSqueeze && (imageBrick.SizeMode == ImageSizeMode.Squeeze))))
            {
                return (!imageBrick.UseImageResolution ? new SizeF(imageBrick.Width.DocToDip(), imageBrick.Height.DocToDip()) : MathMethods.Scale(new SizeF(imageBrick.Image.Width * (96f / imageBrick.Image.HorizontalResolution), imageBrick.Image.Height * (96f / imageBrick.Image.VerticalResolution)), scaleFactor));
            }
            if (((imageBrick.SizeMode != ImageSizeMode.AutoSize) && ((imageBrick.SizeMode != ImageSizeMode.StretchImage) && ((imageBrick.SizeMode != ImageSizeMode.ZoomImage) && (!needSqueeze || (imageBrick.SizeMode != ImageSizeMode.Squeeze))))) && (imageBrick.SizeMode != ImageSizeMode.Tile))
            {
                return SizeF.Empty;
            }
            RectangleF borderBoundsInPixels = this.GetBorderBoundsInPixels(imageBrick);
            return new SizeF(borderBoundsInPixels.Width, borderBoundsInPixels.Height);
        }

        protected override XamlBrickExportMode GetBrickExportMode() => 
            XamlBrickExportMode.Content;

        private static string GetHorizontalAlignment(ImageBrick imageBrick)
        {
            switch (imageBrick.SizeMode)
            {
                case ImageSizeMode.Normal:
                case ImageSizeMode.StretchImage:
                case ImageSizeMode.AutoSize:
                case ImageSizeMode.Tile:
                    return XamlNames.AlignmentLeft;

                case ImageSizeMode.CenterImage:
                case ImageSizeMode.ZoomImage:
                case ImageSizeMode.Squeeze:
                    return XamlNames.AlignmentCenter;
            }
            throw new ArgumentException("imageBrick");
        }

        private static string GetStretchModeName(ImageSizeMode sizeMode, float scaleFactor)
        {
            switch (sizeMode)
            {
                case ImageSizeMode.StretchImage:
                    return XamlNames.StretchFill;

                case ImageSizeMode.ZoomImage:
                case ImageSizeMode.Squeeze:
                    return XamlNames.StretchUniform;
            }
            return ((scaleFactor == 1f) ? XamlNames.StretchNone : XamlNames.StretchUniformToFill);
        }

        private static string GetVerticalAlignment(ImageBrick imageBrick)
        {
            switch (imageBrick.SizeMode)
            {
                case ImageSizeMode.Normal:
                case ImageSizeMode.StretchImage:
                case ImageSizeMode.AutoSize:
                case ImageSizeMode.Tile:
                    return XamlNames.AlignmentTop;

                case ImageSizeMode.CenterImage:
                case ImageSizeMode.ZoomImage:
                case ImageSizeMode.Squeeze:
                    return XamlNames.AlignmentCenter;
            }
            throw new ArgumentException("imageBrick");
        }

        private static bool NeedSqueeze(ImageBrick imageBrick)
        {
            SizeF ef = imageBrick.Size.DocToDip();
            return ((imageBrick.Image.Width > ef.Width) || (imageBrick.Image.Height > ef.Height));
        }

        public override bool RequiresImageResource() => 
            true;

        protected override void WriteBrickToXamlCore(XamlWriter writer, VisualBrick brick, XamlExportContext exportContext)
        {
            ImageBrick key = brick as ImageBrick;
            if (key == null)
            {
                throw new ArgumentException("brick");
            }
            if (exportContext.ResourceMap.ImageResourcesDictionary.ContainsKey(key))
            {
                writer.WriteStartElement(XamlTag.Image);
                writer.WriteAttribute(XamlAttribute.Source, exportContext.ResourceMap.ImageResourcesDictionary[key]);
                writer.WriteAttribute(XamlAttribute.VerticalAlignment, GetVerticalAlignment(key));
                writer.WriteAttribute(XamlAttribute.HorizontalAlignment, GetHorizontalAlignment(key));
                if (!ReferenceEquals(key.Image, EmptyImage.Instance))
                {
                    SizeF ef = this.CalculateImageSize(key, NeedSqueeze(key), exportContext.Page.Document.ScaleFactor);
                    writer.WriteAttribute(XamlAttribute.Width, ef.Width);
                    writer.WriteAttribute(XamlAttribute.Height, ef.Height);
                    writer.WriteAttribute(XamlAttribute.Stretch, GetStretchModeName(key.SizeMode, exportContext.Page.Document.ScaleFactor));
                }
                if (exportContext.Compatibility == XamlCompatibility.WPF)
                {
                    writer.WriteAttribute(XamlAttribute.RenderOptionsBitmapScalingMode, XamlNames.Fant);
                }
                writer.WriteEndElement();
            }
        }
    }
}

