namespace DevExpress.Xpf.Core.HandleDecorator
{
    using DevExpress.Utils;
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    public class ThemeElementPainter
    {
        private Decorator ownerCore;
        private System.Drawing.Color targetColor;
        private ThemeElementImage leftImage;
        private ThemeElementImage rightImage;
        private ThemeElementImage topImage;
        private ThemeElementImage bottomImage;
        private const int defaultDecoratorOffset = 40;
        private const string themeEditorProcessName = "DevExpress.Xpf.ThemeEditor.Launcher";
        private const string decoratorBmpPath1 = "DevExpress.Xpf.Themes.";
        private const string decoratorBmpPath2 = ".Images.HandleDecorator.";
        private const string decoratorDefaultPath = "DevExpress.Xpf.Core.Core.Window.HandleDecorator.DefaultImages.";
        private const string decTopBmp = "decorator_top.png";
        private const string decBottomBmp = "decorator_bottom.png";
        private const string decLeftBmp = "decorator_left.png";
        private const string decRightBmp = "decorator_right.png";
        public const string strLoadDefault = "loadDefault";
        private PaddingEdges DefaultTopAndBottomMargins;
        private PaddingEdges DefaultLeftAndRightMargins;
        private bool loadDefault;

        public ThemeElementPainter(Decorator owner)
        {
            PaddingEdges edges1 = new PaddingEdges();
            edges1.Top = 0;
            edges1.Bottom = 0;
            edges1.Left = 0;
            edges1.Right = 0;
            this.DefaultTopAndBottomMargins = edges1;
            PaddingEdges edges2 = new PaddingEdges();
            edges2.Top = 80;
            edges2.Bottom = 80;
            edges2.Left = 0;
            edges2.Right = 0;
            this.DefaultLeftAndRightMargins = edges2;
            this.ownerCore = owner;
            this.ScaleFactor = 1.0;
        }

        public void CalculateAndSetScaleFactor(System.Drawing.Size size)
        {
            int num = this.GetOffsetByWindowTypeCore(HandleDecoratorWindowTypes.Left) + this.GetOffsetByWindowTypeCore(HandleDecoratorWindowTypes.Right);
            int num2 = (this.GetImageSize(HandleDecoratorWindowTypes.Left).Width + this.GetImageSize(HandleDecoratorWindowTypes.Right).Width) + ((num > 0) ? num : 0);
            num = this.GetOffsetByWindowTypeCore(HandleDecoratorWindowTypes.Top) + this.GetOffsetByWindowTypeCore(HandleDecoratorWindowTypes.Bottom);
            int num3 = (this.GetImageSize(HandleDecoratorWindowTypes.Top).Height + this.GetImageSize(HandleDecoratorWindowTypes.Bottom).Height) + ((num > 0) ? num : 0);
            double num4 = Math.Min((double) (((double) size.Width) / ((double) num2)), (double) (((double) size.Height) / ((double) num3)));
            if ((num4 < 0.25) || (num4 >= 1.0))
            {
                this.ScaleFactor = 1.0;
            }
            else if (num4 < 0.5)
            {
                this.ScaleFactor = 0.25;
            }
            else
            {
                this.ScaleFactor = 0.5;
            }
        }

        private bool CheckThemeEditorIsRunning() => 
            Process.GetProcessesByName("DevExpress.Xpf.ThemeEditor.Launcher").Length != 0;

        public void ClearImages()
        {
            ThemeElementImage image;
            this.bottomImage = (ThemeElementImage) (image = null);
            ThemeElementImage image1 = image;
            ThemeElementImage image2 = this.topImage = image1;
            this.leftImage = this.rightImage = image2;
        }

        private static void DoMargins(out int dstLower, out int dstHigher, int dstLength, int srcLower, int srcHigher)
        {
            if ((srcLower + srcHigher) <= dstLength)
            {
                dstLower = srcLower;
                dstHigher = srcHigher;
            }
            else if (dstLength <= 0)
            {
                dstLower = dstHigher = 0;
            }
            else
            {
                dstLower = (dstLength * srcLower) / (srcLower + srcHigher);
                if ((dstLower == 0) && ((srcLower > 0) && (dstLength >= 2)))
                {
                    dstLower++;
                }
                if ((dstLower == dstLength) && ((srcHigher > 0) && (dstLower >= 2)))
                {
                    dstLower--;
                }
                dstHigher = dstLength - dstLower;
            }
        }

        private void DrawImage(ThemeElementInfo info, System.Drawing.Image image, Rectangle imageBounds, Rectangle screenBounds, ThemeImageStretch stretch, Graphics graphics)
        {
            if (stretch == ThemeImageStretch.Tile)
            {
                this.DrawImageTile(info, image, imageBounds, screenBounds, graphics);
            }
            else
            {
                this.DrawImageStretch(info, image, imageBounds, screenBounds, graphics);
            }
        }

        private unsafe void DrawImageStretch(ThemeElementInfo info, System.Drawing.Image image, Rectangle imageBounds, Rectangle screenBounds, Graphics graphics)
        {
            if ((imageBounds.Width >= 1) && ((imageBounds.Height >= 1) && ((screenBounds.Width >= 1) && (screenBounds.Height >= 1))))
            {
                if ((imageBounds.Width < screenBounds.Width) && (imageBounds.Width > 1))
                {
                    Rectangle* rectanglePtr1 = &imageBounds;
                    rectanglePtr1.Width--;
                }
                if ((imageBounds.Height < screenBounds.Height) && (imageBounds.Height > 1))
                {
                    Rectangle* rectanglePtr2 = &imageBounds;
                    rectanglePtr2.Height--;
                }
                float num = ((float) this.targetColor.R) / 255f;
                float num2 = ((float) this.targetColor.G) / 255f;
                float num3 = ((float) this.targetColor.B) / 255f;
                float[] singleArray1 = new float[5];
                singleArray1[0] = 1f;
                float[][] newColorMatrix = new float[5][];
                newColorMatrix[0] = singleArray1;
                float[] singleArray2 = new float[5];
                singleArray2[1] = 1f;
                newColorMatrix[1] = singleArray2;
                float[] singleArray3 = new float[5];
                singleArray3[2] = 1f;
                newColorMatrix[2] = singleArray3;
                float[] singleArray4 = new float[5];
                singleArray4[3] = 1f;
                newColorMatrix[3] = singleArray4;
                float[] singleArray5 = new float[5];
                singleArray5[0] = num;
                singleArray5[1] = num2;
                singleArray5[2] = num3;
                singleArray5[4] = 1f;
                newColorMatrix[4] = singleArray5;
                ImageAttributes imageAttr = new ImageAttributes();
                imageAttr.SetColorMatrix(new ColorMatrix(newColorMatrix), ColorMatrixFlag.Default, ColorAdjustType.Default);
                graphics.DrawImage(image, screenBounds, imageBounds.X, imageBounds.Y, imageBounds.Width, imageBounds.Height, GraphicsUnit.Pixel, imageAttr);
                imageAttr.Dispose();
            }
        }

        private void DrawImageStretchTile(ThemeElementInfo info, PaddingEdges paddingEdgesData, System.Drawing.Image image, Rectangle imageBounds, Rectangle destBounds, ThemeImageStretch stretch, Graphics graphics)
        {
            int num;
            int num2;
            int num3;
            int num4;
            DoMargins(out num, out num2, destBounds.Width, paddingEdgesData.Left, paddingEdgesData.Right);
            DoMargins(out num3, out num4, destBounds.Height, (int) (paddingEdgesData.Top * this.ScaleFactor), (int) (paddingEdgesData.Bottom * this.ScaleFactor));
            int x = imageBounds.X;
            int num6 = x + paddingEdgesData.Left;
            int num7 = (x + imageBounds.Width) - paddingEdgesData.Right;
            int y = imageBounds.Y;
            int num9 = y + paddingEdgesData.Top;
            int num10 = (y + imageBounds.Height) - paddingEdgesData.Bottom;
            int width = num7 - num6;
            int height = num10 - num9;
            int num13 = destBounds.X;
            int num14 = num13 + num;
            int num15 = (num13 + destBounds.Width) - num2;
            int num16 = destBounds.Y;
            int num17 = num16 + num3;
            int num18 = (num16 + destBounds.Height) - num4;
            int num19 = num15 - num14;
            int num20 = num18 - num17;
            this.DrawImage(info, image, new Rectangle(num6, num9, width, height), new Rectangle(num14, num17, num19, num20), stretch, graphics);
            this.DrawImage(info, image, new Rectangle(num6, y, width, paddingEdgesData.Top), new Rectangle(num14, num16, num19, num3), stretch, graphics);
            this.DrawImage(info, image, new Rectangle(num7, num9, paddingEdgesData.Right, height), new Rectangle(num15, num17, num2, num20), stretch, graphics);
            this.DrawImage(info, image, new Rectangle(num6, num10, width, paddingEdgesData.Bottom), new Rectangle(num14, num18, num19, num4), stretch, graphics);
            this.DrawImage(info, image, new Rectangle(x, num9, paddingEdgesData.Left, height), new Rectangle(num13, num17, num, num20), stretch, graphics);
            this.DrawImage(info, image, new Rectangle(x, y, paddingEdgesData.Left, paddingEdgesData.Top), new Rectangle(num13, num16, num, num3), stretch, graphics);
            this.DrawImage(info, image, new Rectangle(num7, y, paddingEdgesData.Right, paddingEdgesData.Top), new Rectangle(num15, num16, num2, num3), stretch, graphics);
            this.DrawImage(info, image, new Rectangle(num7, num10, paddingEdgesData.Right, paddingEdgesData.Bottom), new Rectangle(num15, num18, num2, num4), stretch, graphics);
            this.DrawImage(info, image, new Rectangle(x, num10, paddingEdgesData.Left, paddingEdgesData.Bottom), new Rectangle(num13, num18, num, num4), stretch, graphics);
        }

        private void DrawImageTile(ThemeElementInfo info, System.Drawing.Image image, Rectangle imageBounds, Rectangle screenBounds, Graphics graphics)
        {
            imageBounds.X = Math.Max(0, imageBounds.X);
            imageBounds.Y = Math.Max(0, imageBounds.Y);
            if ((imageBounds.Width >= 1) && ((imageBounds.Height >= 1) && ((screenBounds.Width >= 1) && (screenBounds.Height >= 1))))
            {
                try
                {
                    using (TextureBrush brush = new TextureBrush(image, WrapMode.Tile, imageBounds))
                    {
                        int x = 0;
                        int y = 0;
                        x = screenBounds.X;
                        y = screenBounds.Y;
                        if ((x != 0) || (y != 0))
                        {
                            brush.TranslateTransform((float) x, (float) y);
                        }
                        graphics.FillRectangle(brush, screenBounds);
                    }
                }
                catch
                {
                }
            }
        }

        public void DrawObject(ThemeElementInfo info, Graphics g)
        {
            this.targetColor = this.GetTargetColor(info.Active);
            this.DrawThemeImage(info, this.GetElementImageByWindowType(info.WindowType), g);
        }

        private void DrawThemeImage(ThemeElementInfo elementInfo, ThemeElementImage themeImage, Graphics graphics)
        {
            if (themeImage != null)
            {
                int index = 0;
                if (elementInfo.ImageIndex != -1)
                {
                    index = elementInfo.ImageIndex;
                }
                if (index >= themeImage.ImageCount)
                {
                    index = 0;
                }
                Rectangle imageBounds = themeImage.GetImageBounds(index);
                if (!imageBounds.IsEmpty)
                {
                    Rectangle bounds = elementInfo.Bounds;
                    if (themeImage.SizingMargins.IsEmpty)
                    {
                        this.DrawImage(elementInfo, themeImage.Image, imageBounds, bounds, themeImage.Stretch, graphics);
                    }
                    else
                    {
                        this.DrawImageStretchTile(elementInfo, themeImage.SizingMargins, themeImage.Image, imageBounds, bounds, themeImage.Stretch, graphics);
                    }
                }
            }
        }

        private System.Drawing.Image GetDefaultDecoratorImage(string path)
        {
            string name = "DevExpress.Xpf.Core.Core.Window.HandleDecorator.DefaultImages.";
            if (path.EndsWith("decorator_top.png"))
            {
                name = name + "decorator_top.png";
            }
            if (path.EndsWith("decorator_bottom.png"))
            {
                name = name + "decorator_bottom.png";
            }
            if (path.EndsWith("decorator_left.png"))
            {
                name = name + "decorator_left.png";
            }
            if (path.EndsWith("decorator_right.png"))
            {
                name = name + "decorator_right.png";
            }
            return ResourceImageHelper.CreateImageFromResources(name, Assembly.GetExecutingAssembly());
        }

        public ThemeElementImage GetElementImageByWindowType(HandleDecoratorWindowTypes windowType)
        {
            Assembly themeAssembly = null;
            if (this.ownerCore.CurrentThemeName != "loadDefault")
            {
                themeAssembly = AssemblyHelper.GetThemeAssembly(this.ownerCore.CurrentThemeName);
            }
            switch (windowType)
            {
                case HandleDecoratorWindowTypes.Left:
                    if (this.leftImage == null)
                    {
                        ThemeElementImage image1 = new ThemeElementImage();
                        image1.ImageCount = 2;
                        image1.Image = this.GetImageFromThemeAssembly("DevExpress.Xpf.Themes." + this.ownerCore.CurrentThemeName + ".Images.HandleDecorator.decorator_left.png", themeAssembly);
                        image1.Stretch = ThemeImageStretch.Stretch;
                        image1.Layout = ThemeImageLayout.Horizontal;
                        image1.SizingMargins = this.GetSizingMargins(this.ownerCore.DecoratorLeftMargins, true);
                        this.leftImage = image1;
                    }
                    return this.leftImage;

                case HandleDecoratorWindowTypes.Top:
                    if (this.topImage == null)
                    {
                        ThemeElementImage image3 = new ThemeElementImage();
                        image3.ImageCount = 2;
                        image3.Image = this.GetImageFromThemeAssembly("DevExpress.Xpf.Themes." + this.ownerCore.CurrentThemeName + ".Images.HandleDecorator.decorator_top.png", themeAssembly);
                        image3.Stretch = ThemeImageStretch.Stretch;
                        image3.Layout = ThemeImageLayout.Vertical;
                        image3.SizingMargins = this.GetSizingMargins(this.ownerCore.DecoratorTopMargins, false);
                        this.topImage = image3;
                    }
                    return this.topImage;

                case HandleDecoratorWindowTypes.Right:
                    if (this.rightImage == null)
                    {
                        ThemeElementImage image2 = new ThemeElementImage();
                        image2.ImageCount = 2;
                        image2.Image = this.GetImageFromThemeAssembly("DevExpress.Xpf.Themes." + this.ownerCore.CurrentThemeName + ".Images.HandleDecorator.decorator_right.png", themeAssembly);
                        image2.Stretch = ThemeImageStretch.Stretch;
                        image2.Layout = ThemeImageLayout.Horizontal;
                        image2.SizingMargins = this.GetSizingMargins(this.ownerCore.DecoratorRightMargins, true);
                        this.rightImage = image2;
                    }
                    return this.rightImage;

                case HandleDecoratorWindowTypes.Bottom:
                    if (this.bottomImage == null)
                    {
                        ThemeElementImage image4 = new ThemeElementImage();
                        image4.ImageCount = 2;
                        image4.Image = this.GetImageFromThemeAssembly("DevExpress.Xpf.Themes." + this.ownerCore.CurrentThemeName + ".Images.HandleDecorator.decorator_bottom.png", themeAssembly);
                        image4.Stretch = ThemeImageStretch.Stretch;
                        image4.Layout = ThemeImageLayout.Vertical;
                        image4.SizingMargins = this.GetSizingMargins(this.ownerCore.DecoratorBottomMargins, false);
                        this.bottomImage = image4;
                    }
                    return this.bottomImage;
            }
            ThemeElementImage image5 = new ThemeElementImage();
            image5.ImageCount = 1;
            image5.Image = new Bitmap(1, 1);
            PaddingEdges edges1 = new PaddingEdges();
            edges1.Left = 0;
            edges1.Right = 0;
            edges1.Bottom = 0;
            edges1.Top = 0;
            image5.SizingMargins = edges1;
            image5.Stretch = ThemeImageStretch.Stretch;
            return image5;
        }

        private System.Drawing.Image GetImageFromThemeAssembly(string path, Assembly themeAssembly)
        {
            System.Drawing.Image defaultDecoratorImage = null;
            if (themeAssembly == null)
            {
                this.loadDefault = true;
                return this.GetDefaultDecoratorImage(path);
            }
            Stream manifestResourceStream = themeAssembly.GetManifestResourceStream(path);
            if (manifestResourceStream == null)
            {
                this.loadDefault = true;
                defaultDecoratorImage = this.GetDefaultDecoratorImage(path);
            }
            else
            {
                defaultDecoratorImage = ResourceImageHelper.CreateImageFromResources(path, themeAssembly);
                manifestResourceStream.Dispose();
                this.loadDefault = false;
            }
            return defaultDecoratorImage;
        }

        private System.Drawing.Size GetImageSize(HandleDecoratorWindowTypes windowType) => 
            HandleDecoratorWindowLayoutCalculator.GetImageSize(this.GetElementImageByWindowType(windowType));

        public int GetOffsetByWindowType(HandleDecoratorWindowTypes windowType) => 
            (int) (this.GetOffsetByWindowTypeCore(windowType) * this.ScaleFactor);

        private int GetOffsetByWindowTypeCore(HandleDecoratorWindowTypes windowType)
        {
            if (this.ownerCore != null)
            {
                if (this.loadDefault)
                {
                    return 40;
                }
                switch (windowType)
                {
                    case HandleDecoratorWindowTypes.Left:
                        return (int) this.ownerCore.DecoratorOffset.Left;

                    case HandleDecoratorWindowTypes.Top:
                        return (int) this.ownerCore.DecoratorOffset.Top;

                    case HandleDecoratorWindowTypes.Right:
                        return (int) this.ownerCore.DecoratorOffset.Right;

                    case HandleDecoratorWindowTypes.Bottom:
                        return (int) this.ownerCore.DecoratorOffset.Bottom;
                }
            }
            return 0;
        }

        public System.Drawing.Color GetRenderedColor(SolidColorBrush inputColor)
        {
            if (!this.CheckThemeEditorIsRunning())
            {
                return System.Drawing.Color.FromArgb(inputColor.Color.A, inputColor.Color.R, inputColor.Color.G, inputColor.Color.B);
            }
            DrawingVisual visual = new DrawingVisual();
            DrawingContext context = visual.RenderOpen();
            context.DrawRectangle(inputColor, null, new Rect(0.0, 0.0, 1.0, 1.0));
            context.Close();
            RenderTargetBitmap source = new RenderTargetBitmap(1, 1, 120.0, 96.0, PixelFormats.Pbgra32);
            source.Render(visual);
            BitmapEncoder encoder = new BmpBitmapEncoder {
                Frames = { BitmapFrame.Create(source) }
            };
            MemoryStream stream = new MemoryStream();
            encoder.Save(stream);
            Bitmap bitmap2 = new Bitmap(stream);
            bitmap2.Dispose();
            stream.Dispose();
            source = null;
            encoder = null;
            visual = null;
            context = null;
            return bitmap2.GetPixel(0, 0);
        }

        private PaddingEdges GetSizingMargins(Thickness margins, bool leftOrRight)
        {
            PaddingEdges edges;
            if (this.loadDefault)
            {
                edges = !leftOrRight ? this.DefaultTopAndBottomMargins : this.DefaultLeftAndRightMargins;
            }
            else
            {
                PaddingEdges edges1 = new PaddingEdges();
                edges1.Left = (int) margins.Left;
                edges1.Right = (int) margins.Right;
                edges1.Bottom = (int) margins.Bottom;
                edges1.Top = (int) margins.Top;
                edges = edges1;
            }
            return edges;
        }

        private System.Drawing.Color GetTargetColor(bool active) => 
            (this.ownerCore != null) ? (!active ? this.GetRenderedColor(this.ownerCore.InactiveColor) : this.GetRenderedColor(this.ownerCore.ActiveColor)) : System.Drawing.Color.FromArgb(0, 0, 0, 0);

        public ThemeElementImage Image { get; set; }

        public double ScaleFactor { get; set; }
    }
}

