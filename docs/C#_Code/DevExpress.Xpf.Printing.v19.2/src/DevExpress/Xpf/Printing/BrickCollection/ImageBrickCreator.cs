namespace DevExpress.Xpf.Printing.BrickCollection
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Printing;
    using DevExpress.Xpf.Printing.Native;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    internal class ImageBrickCreator : BrickCreator
    {
        public ImageBrickCreator(PrintingSystemBase ps, Dictionary<BrickStyleKey, BrickStyle> brickStyles, Dictionary<IVisualBrick, IOnPageUpdater> onPageUpdaters) : base(ps, brickStyles, onPageUpdaters)
        {
        }

        public override VisualBrick Create(UIElement source, UIElement parent)
        {
            object obj2;
            Guard.ArgumentNotNull(source, "source");
            Guard.ArgumentNotNull(parent, "parent");
            IImageExportSettings exportSettings = new EffectiveImageExportSettings(source);
            ImageBrick brick = new ImageBrick();
            base.InitializeBrickCore(source, parent, brick, exportSettings);
            FrameworkElement element1 = exportSettings.SourceElement;
            FrameworkElement element2 = element1;
            if (element1 == null)
            {
                FrameworkElement local1 = element1;
                element2 = (FrameworkElement) source;
            }
            FrameworkElement sourceElement = element2;
            ImageRenderMode imageRenderMode = exportSettings.ImageRenderMode;
            if (imageRenderMode != ImageRenderMode.MakeScreenshot)
            {
                if (imageRenderMode == ImageRenderMode.UseImageSource)
                {
                    System.Windows.Controls.Image image = exportSettings.SourceElement as System.Windows.Controls.Image;
                    if (image == null)
                    {
                        throw new InvalidOperationException();
                    }
                    BitmapSource bitmapSource = image.Source as BitmapSource;
                    if (bitmapSource != null)
                    {
                        obj2 = exportSettings.ImageKey ?? bitmapSource;
                        brick.Image = GetImageFromCache(base.ps.Images, obj2, () => DrawingConverter.FromBitmapSource(bitmapSource));
                    }
                    brick.SizeMode = exportSettings.ForceCenterImageMode ? ImageSizeMode.CenterImage : GetImageSizeModeFromStretch(image.Stretch);
                }
            }
            else
            {
                MultiKey key1;
                if (exportSettings.ImageKey == null)
                {
                    key1 = null;
                }
                else
                {
                    object[] keyParts = new object[] { exportSettings.ImageKey, sourceElement.ActualWidth, sourceElement.ActualHeight };
                    key1 = new MultiKey(keyParts);
                }
                obj2 = key1;
                brick.Image = GetImageFromCache(base.ps.Images, obj2, () => this.CreateImage(sourceElement));
                brick.SizeMode = exportSettings.ForceCenterImageMode ? ImageSizeMode.CenterImage : ImageSizeMode.Normal;
            }
            brick.UseImageResolution = true;
            return brick;
        }

        private System.Drawing.Image CreateImage(FrameworkElement source) => 
            DrawingConverter.CreateGdiImage(source);

        private static System.Drawing.Image GetImageFromCache(ImagesContainer container, object key, Func<System.Drawing.Image> createIfNotExists)
        {
            if (key == null)
            {
                System.Drawing.Image image = createIfNotExists();
                return container.GetImage(image);
            }
            System.Drawing.Image imageByKey = container.GetImageByKey(key);
            if (imageByKey == null)
            {
                imageByKey = createIfNotExists();
                if (imageByKey != null)
                {
                    container.Add(key, imageByKey);
                }
            }
            return imageByKey;
        }

        private static ImageSizeMode GetImageSizeModeFromStretch(Stretch stretch)
        {
            ImageSizeMode normal = ImageSizeMode.Normal;
            switch (stretch)
            {
                case Stretch.None:
                    normal = ImageSizeMode.Normal;
                    break;

                case Stretch.Fill:
                    normal = ImageSizeMode.StretchImage;
                    break;

                case Stretch.Uniform:
                    normal = ImageSizeMode.ZoomImage;
                    break;

                case Stretch.UniformToFill:
                    throw new NotSupportedException("UniformToFill mode is not supported");

                default:
                    break;
            }
            return normal;
        }
    }
}

