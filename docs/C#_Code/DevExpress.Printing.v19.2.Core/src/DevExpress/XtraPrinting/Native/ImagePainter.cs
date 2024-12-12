namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils;
    using DevExpress.Utils.Design;
    using DevExpress.Utils.Svg;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Drawing;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Runtime.InteropServices;

    public static class ImagePainter
    {
        public static System.Drawing.Image CreateRasterizedSvgImage(SvgImage svgImage)
        {
            int num2 = (int) Math.Round(svgImage.Height);
            Bitmap image = new Bitmap(Math.Max(1, (int) Math.Round(svgImage.Width)), Math.Max(1, num2));
            image.SetResolution(96f, 96f);
            using (Graphics graphics = Graphics.FromImage(image))
            {
                DrawSvgImage(svgImage, graphics, new RectangleF((PointF) Point.Empty, (SizeF) image.Size), null);
            }
            return image;
        }

        public static void Draw(ImageSource imageSource, IGraphicsBase gr, RectangleF imageRect)
        {
            if (!ImageSource.IsNullOrEmpty(imageSource) && !imageRect.IsEmpty)
            {
                if (imageSource.HasSvgImage)
                {
                    DrawSvgImage(imageSource, gr, imageRect);
                }
                else
                {
                    gr.DrawImage(imageSource.Image, imageRect);
                }
            }
        }

        public static void Draw(ImageSource imageSource, Graphics gr, RectangleF imageRect)
        {
            if (!ImageSource.IsNullOrEmpty(imageSource) && !imageRect.IsEmpty)
            {
                if (imageSource.HasSvgImage)
                {
                    DrawSvgImage(imageSource.SvgImage, gr, imageRect, null);
                }
                else
                {
                    gr.DrawImage(imageSource.Image, imageRect);
                }
            }
        }

        private static void DrawSvgImage(ImageSource imageSource, IGraphicsBase gr, RectangleF imageRect)
        {
            DrawSvgImage(imageSource.SvgImage, gr, imageRect, null);
        }

        public static void DrawSvgImage(SvgImage svgImage, IGraphicsBase gr, RectangleF imageRect, ISvgPaletteProvider paletteProvider = null)
        {
            if ((svgImage.Width != 0.0) && (svgImage.Height != 0.0))
            {
                IGraphicsState gstate = gr.Save();
                try
                {
                    gr.TranslateTransform(imageRect.X, imageRect.Y);
                    gr.ScaleTransform(imageRect.Width / ((float) svgImage.Width), imageRect.Height / ((float) svgImage.Height));
                    new SvgBitmap(svgImage).RenderToGraphicsBase(gr, paletteProvider, 1.0);
                }
                finally
                {
                    gr.Restore(gstate);
                }
            }
        }

        public static void DrawSvgImage(SvgImage svgImage, Graphics gr, RectangleF imageRect, ISvgPaletteProvider paletteProvider = null)
        {
            if ((svgImage.Width != 0.0) && (svgImage.Height != 0.0))
            {
                GraphicsState gstate = gr.Save();
                try
                {
                    gr.TranslateTransform(imageRect.X, imageRect.Y);
                    gr.ScaleTransform(imageRect.Width / ((float) svgImage.Width), imageRect.Height / ((float) svgImage.Height));
                    new SvgBitmap(svgImage).RenderToGraphics(gr, paletteProvider, 1.0, DefaultBoolean.Default);
                }
                finally
                {
                    gr.Restore(gstate);
                }
            }
        }

        public static void DrawTileSvgImage(SvgImage svgImage, IGraphicsBase gr, RectangleF imageRect)
        {
            if ((svgImage.Width != 0.0) && (svgImage.Height != 0.0))
            {
                IGraphicsState gstate = gr.Save();
                try
                {
                    float num = 3.125f;
                    float dx = ((float) svgImage.Width) * num;
                    float dy = ((float) svgImage.Height) * num;
                    SvgBitmap bitmap = new SvgBitmap(svgImage);
                    gr.TranslateTransform(imageRect.X, imageRect.Y);
                    int num4 = (int) Math.Ceiling((double) (imageRect.Width / dx));
                    int num5 = (int) Math.Ceiling((double) (imageRect.Height / dy));
                    int num6 = 0;
                    while (num6 < num4)
                    {
                        IGraphicsState state2 = gr.Save();
                        int num7 = 0;
                        while (true)
                        {
                            if (num7 >= num5)
                            {
                                gr.Restore(state2);
                                gr.TranslateTransform(dx, 0f);
                                num6++;
                                break;
                            }
                            bitmap.RenderToGraphicsBase(gr, null, (double) num);
                            gr.TranslateTransform(0f, dy);
                            num7++;
                        }
                    }
                }
                finally
                {
                    gr.Restore(gstate);
                }
            }
        }

        internal static ImageFormat GetValidFormat(ImageFormat format)
        {
            Guid guid = format.Guid;
            return (((guid == ImageFormat.Bmp.Guid) || ((guid == ImageFormat.Jpeg.Guid) || ((guid == ImageFormat.Png.Guid) || (guid == ImageFormat.Tiff.Guid)))) ? format : ImageFormat.Png);
        }
    }
}

