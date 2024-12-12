namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    internal static class BitmapHelpers
    {
        public static int CreateArgb32(int a, int r, int g, int b) => 
            (((a << 0x18) | (r << 0x10)) | (g << 8)) | b;

        public static ImageSource GenerateBitmap(int width, int height, Action<int[]> generator)
        {
            WriteableBitmap bitmap = new WriteableBitmap(width, height, 96.0, 96.0, PixelFormats.Bgra32, null);
            int[] numArray = new int[bitmap.PixelWidth * bitmap.PixelHeight];
            generator(numArray);
            Int32Rect sourceRect = new Int32Rect(0, 0, bitmap.PixelWidth, bitmap.PixelHeight);
            bitmap.WritePixels(sourceRect, numArray, (bitmap.PixelWidth * bitmap.Format.BitsPerPixel) / 8, 0);
            return bitmap;
        }
    }
}

