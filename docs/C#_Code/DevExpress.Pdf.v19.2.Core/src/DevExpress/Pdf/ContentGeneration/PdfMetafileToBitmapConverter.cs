namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;

    public static class PdfMetafileToBitmapConverter
    {
        public static Bitmap ConvertToBitmap(Image image, Color backgroundColor, int resolution)
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
    }
}

