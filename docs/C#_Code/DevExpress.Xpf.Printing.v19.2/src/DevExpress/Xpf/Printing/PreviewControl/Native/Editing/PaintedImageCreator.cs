namespace DevExpress.Xpf.Printing.PreviewControl.Native.Editing
{
    using DevExpress.Data.Utils;
    using DevExpress.Utils;
    using DevExpress.Utils.Svg;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Drawing;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class PaintedImageCreator
    {
        private readonly Color background;
        private readonly bool useImageResolution;

        public PaintedImageCreator(ImageSizeMode sizeMode, ImageAlignment alignment, Color background, bool useImageResolution)
        {
            this.SizeMode = sizeMode;
            this.Alignment = alignment;
            this.background = background;
            this.useImageResolution = useImageResolution;
        }

        private System.Drawing.Image CreatePaintedBitmap(SizeF clientSize, float resolution, ImageSource actualImageSource, IEnumerable<Curve> curves)
        {
            float xDpi = this.useImageResolution ? resolution : 96f;
            Size size = Size.Round(MathMethods.Scale(clientSize, (float) (xDpi / 96f)));
            Bitmap image = new Bitmap(size.Width, size.Height);
            image.SetResolution(xDpi, xDpi);
            using (Graphics graphics = Graphics.FromImage(image))
            {
                if (!ImageSource.IsNullOrEmpty(actualImageSource))
                {
                    SizeF imageSize = MathMethods.Scale(actualImageSource.GetImageSize(this.useImageResolution), (float) (xDpi / 96f));
                    this.DrawImage(graphics, actualImageSource, clientSize, imageSize);
                }
                this.DrawCurves(graphics, xDpi, curves);
            }
            return image;
        }

        public System.Drawing.Image CreatePaintedImage(ImageSource source, List<Curve> curves, SizeF clientRectSize, float zoomFactor)
        {
            SizeF clientSize = MathMethods.Scale(clientRectSize, zoomFactor);
            if (ImageSource.IsNullOrEmpty(source) || source.HasSvgImage)
            {
                return (VisualBrickExporter.IsMetafileExportAvailable ? this.CreatePaintedMetafile(clientSize, source, curves) : this.CreatePaintedBitmap(clientSize, 300f, source, curves));
            }
            if (source.Image is Metafile)
            {
                Metafile metafile = MetafileCreator.CreateInstance(new RectangleF((PointF) Point.Empty, clientSize), MetafileFrameUnit.Pixel);
                using (Graphics graphics = Graphics.FromImage(metafile))
                {
                    this.DrawImage(graphics, source, clientSize, MathMethods.Scale((SizeF) source.Image.Size, (float) (96f / source.Image.HorizontalResolution)));
                    this.DrawCurves(graphics, 96f, curves);
                }
                return EmfMetafilePatcher.PatchResolution(metafile, (int) source.Image.HorizontalResolution);
            }
            float xDpi = this.useImageResolution ? this.GetEffectiveResolution(source.Image, clientSize) : 96f;
            Size size = Size.Round(MathMethods.Scale(clientSize, (float) (xDpi / 96f)));
            Bitmap image = new Bitmap(size.Width, size.Height);
            using (Stream stream = new MemoryStream())
            {
                image.Save(stream, GetValidFormat(source.Image.RawFormat));
                image = (Bitmap) System.Drawing.Image.FromStream(stream);
            }
            image.SetResolution(xDpi, xDpi);
            using (Graphics graphics2 = Graphics.FromImage(image))
            {
                if (!System.Drawing.Image.IsAlphaPixelFormat(source.Image.PixelFormat))
                {
                    FillBackground(graphics2, size, this.background);
                }
                this.DrawImage(graphics2, source, clientSize, MathMethods.Scale((SizeF) source.Image.Size, (float) (xDpi / source.Image.HorizontalResolution)));
                this.DrawCurves(graphics2, xDpi, curves);
            }
            return image;
        }

        private System.Drawing.Image CreatePaintedMetafile(SizeF clientSize, ImageSource actualImageSource, IEnumerable<Curve> curves)
        {
            Metafile image = MetafileCreator.CreateInstance(new RectangleF((PointF) Point.Empty, clientSize), MetafileFrameUnit.Pixel);
            using (Graphics graphics = Graphics.FromImage(image))
            {
                if (!ImageSource.IsNullOrEmpty(actualImageSource))
                {
                    SizeF imageSize = actualImageSource.GetImageSize(this.useImageResolution);
                    this.DrawImage(graphics, actualImageSource, clientSize, imageSize);
                }
                this.DrawCurves(graphics, 96f, curves);
            }
            return EmfMetafilePatcher.PatchResolution(image, 0x60);
        }

        public void DrawCurve(Graphics graphics, PointF[] points, Color color, float thickness)
        {
            if (points.Length == 1)
            {
                using (SolidBrush brush = new SolidBrush(color))
                {
                    RectangleF rect = new RectangleF(points[0].X - (thickness / 2f), points[0].Y - (thickness / 2f), thickness, thickness);
                    graphics.FillEllipse(brush, rect);
                }
            }
            else if (points.Any<PointF>())
            {
                Pen pen1 = new Pen(color, thickness);
                pen1.StartCap = LineCap.Round;
                pen1.EndCap = LineCap.Round;
                pen1.LineJoin = LineJoin.Round;
                using (Pen pen = pen1)
                {
                    GraphicsPath path = new GraphicsPath();
                    path.AddLines(points);
                    graphics.DrawPath(pen, path);
                }
            }
        }

        private void DrawCurves(Graphics graphics, float imageDpi, IEnumerable<Curve> curves)
        {
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            float imageDpiScaleFactor = imageDpi / 96f;
            foreach (Curve curve in curves)
            {
                Func<PointF, PointF> <>9__0;
                Func<PointF, PointF> selector = <>9__0;
                if (<>9__0 == null)
                {
                    Func<PointF, PointF> local1 = <>9__0;
                    selector = <>9__0 = pt => MathMethods.Scale(pt, imageDpiScaleFactor);
                }
                PointF[] points = curve.Points.Select<PointF, PointF>(selector).ToArray<PointF>();
                float thickness = MathMethods.Scale(curve.Thickness, (double) imageDpiScaleFactor);
                this.DrawCurve(graphics, points, curve.Color, thickness);
            }
        }

        public void DrawImage(Graphics graphics, ImageSource source, SizeF clientSize, SizeF imageSize)
        {
            try
            {
                RectangleF clientRect = new RectangleF((PointF) Point.Empty, clientSize);
                RectangleF imageRect = ImageTool.CalculateImageRectCore(clientRect, imageSize, this.SizeMode, this.Alignment);
                if (source.HasSvgImage)
                {
                    this.DrawSvgImage(source, imageSize, imageRect, graphics, clientSize);
                }
                else
                {
                    Size size = new Size((int) clientSize.Width, (int) clientSize.Height);
                    System.Drawing.Image image = (this.SizeMode == ImageSizeMode.Tile) ? GetTileImage(source.Image, size) : source.Image;
                    graphics.DrawImage(image, imageRect);
                }
            }
            catch
            {
            }
        }

        private void DrawSvgImage(ImageSource imageSource, SizeF imageSize, RectangleF imageRect, Graphics graphics, SizeF clientSize)
        {
            GraphicsState gstate = graphics.Save();
            try
            {
                if (this.SizeMode != ImageSizeMode.Tile)
                {
                    graphics.TranslateTransform(imageRect.X, imageRect.Y);
                    graphics.ScaleTransform(imageRect.Width / imageSize.Width, imageRect.Height / imageSize.Height);
                    new SvgBitmap(imageSource.SvgImage).RenderToGraphics(graphics, null, 1.0, DefaultBoolean.Default);
                }
                else
                {
                    SvgImage svgImage = imageSource.SvgImage;
                    float width = (float) svgImage.Width;
                    float height = (float) svgImage.Height;
                    SvgBitmap bitmap = new SvgBitmap(svgImage);
                    graphics.TranslateTransform(imageRect.X, imageRect.Y);
                    int num3 = (int) Math.Ceiling((double) (imageRect.Width / width));
                    int num4 = (int) Math.Ceiling((double) (imageRect.Height / height));
                    int num5 = 0;
                    while (num5 < num3)
                    {
                        GraphicsState state2 = graphics.Save();
                        int num6 = 0;
                        while (true)
                        {
                            if (num6 >= num4)
                            {
                                graphics.Restore(state2);
                                graphics.TranslateTransform(width, 0f);
                                num5++;
                                break;
                            }
                            bitmap.RenderToGraphics(graphics, null, 1.0, DefaultBoolean.Default);
                            graphics.TranslateTransform(0f, height);
                            num6++;
                        }
                    }
                }
            }
            finally
            {
                graphics.Restore(gstate);
            }
        }

        private static void FillBackground(Graphics graphics, Size size, Color backColor)
        {
            Color color = DXColor.IsTransparentColor(backColor) ? DXColor.White : backColor;
            graphics.FillRectangle(new SolidBrush(color), new Rectangle(Point.Empty, size));
        }

        private float GetEffectiveResolution(System.Drawing.Image image, SizeF clientSize)
        {
            float num = Math.Min(Math.Max(image.HorizontalResolution, 96f), 300f);
            if ((this.SizeMode == ImageSizeMode.ZoomImage) || (this.SizeMode == ImageSizeMode.Squeeze))
            {
                SizeF ef = (SizeF) GraphicsUnitConverter.Convert(image.Size, image.HorizontalResolution, (float) 96f);
                float num2 = Math.Min((float) (clientSize.Width / ef.Width), (float) (clientSize.Height / ef.Height));
                if (num2 < 1f)
                {
                    num = Math.Min((float) 600f, (float) (num / num2));
                }
            }
            return num;
        }

        private static System.Drawing.Image GetTileImage(System.Drawing.Image baseImage, Size clientSize)
        {
            System.Drawing.Image image2;
            if (baseImage == null)
            {
                return baseImage;
            }
            try
            {
                object[] keyParts = new object[] { clientSize, HtmlImageHelper.GetImageHashCode(baseImage) };
                object obj2 = new MultiKey(keyParts);
                image2 = ImageHelper.CreateTileImage(baseImage, clientSize);
            }
            catch
            {
                return baseImage;
            }
            return image2;
        }

        private static ImageFormat GetValidFormat(ImageFormat format)
        {
            Guid guid = format.Guid;
            return (((guid == ImageFormat.Bmp.Guid) || ((guid == ImageFormat.Jpeg.Guid) || ((guid == ImageFormat.Png.Guid) || (guid == ImageFormat.Tiff.Guid)))) ? format : ImageFormat.Png);
        }

        public ImageSizeMode SizeMode { get; set; }

        public ImageAlignment Alignment { get; set; }
    }
}

