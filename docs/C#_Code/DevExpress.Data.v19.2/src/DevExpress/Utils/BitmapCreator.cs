namespace DevExpress.Utils
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;

    public class BitmapCreator
    {
        private const int defaultResolutionLimit = 300;

        private static Bitmap CopyContent(Image original, Bitmap newBitmap, Color backColor)
        {
            using (Graphics graphics = Graphics.FromImage(newBitmap))
            {
                if (!DXColor.IsEmpty(backColor))
                {
                    graphics.Clear(backColor);
                }
                graphics.DrawImage(original, 0, 0);
            }
            return newBitmap;
        }

        private static void Correct(ref int size, ref float resolution, float resolutionLimit)
        {
            if (resolution > 300f)
            {
                size = (int) ((((float) size) / resolution) * resolutionLimit);
                resolution = 300f;
            }
        }

        public static Bitmap CreateBitmap(Image original, Color backColor)
        {
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);
            newBitmap.SetResolution(GraphicsDpi.GetSafeResolution(original.HorizontalResolution), GraphicsDpi.GetSafeResolution(original.VerticalResolution));
            return CopyContent(original, newBitmap, backColor);
        }

        public static Bitmap CreateBitmapWithResolutionLimit(Image original, Color backColor)
        {
            Bitmap newBitmap = CreateClearBitmap(original, 300f);
            return CopyContent(original, newBitmap, backColor);
        }

        public static Bitmap CreateClearBitmap(Image original, float resolutionLimit)
        {
            int width = original.Width;
            int height = original.Height;
            float safeResolution = GraphicsDpi.GetSafeResolution(original.HorizontalResolution);
            float resolution = GraphicsDpi.GetSafeResolution(original.VerticalResolution);
            if (IsMetafile(original))
            {
                Correct(ref width, ref safeResolution, resolutionLimit);
                Correct(ref height, ref resolution, resolutionLimit);
            }
            Bitmap bitmap = new Bitmap(Math.Max(1, width), Math.Max(1, height));
            bitmap.SetResolution(safeResolution, resolution);
            return bitmap;
        }

        public static ImageAttributes CreateTransparencyAttributes(int imageTransparency)
        {
            float num = 1f - (((float) imageTransparency) / 255f);
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
            singleArray4[3] = num;
            newColorMatrix[3] = singleArray4;
            float[] singleArray5 = new float[5];
            singleArray5[4] = 1f;
            newColorMatrix[4] = singleArray5;
            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(new ColorMatrix(newColorMatrix), ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            return attributes;
        }

        private static bool IsMetafile(Image original) => 
            original is Metafile;

        public static void TransformBitmap(Image original, Bitmap bitmap, ImageAttributes attributes)
        {
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.Transparent);
            graphics.DrawImage(original, new Rectangle(Point.Empty, bitmap.Size), 0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);
            graphics.Dispose();
        }
    }
}

