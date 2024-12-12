namespace DevExpress.Office.Utils
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;

    public sealed class PaletteHelper
    {
        private static Color[] palette0 = new Color[0];
        private static Color[] palette2 = PreparePalette2();
        private static Color[] palette16 = PreparePalette16();
        private static Color[] palette256 = PreparePalette256();

        private PaletteHelper()
        {
        }

        public static Color[] GetPalette(int bitsPerPixel) => 
            (bitsPerPixel != 8) ? ((bitsPerPixel != 4) ? ((bitsPerPixel != 1) ? palette0 : palette2) : palette16) : palette256;

        private static Color[] PreparePalette(PixelFormat pixelFormat)
        {
            using (Bitmap bitmap = new Bitmap(10, 10, pixelFormat))
            {
                return bitmap.Palette.Entries;
            }
        }

        private static Color[] PreparePalette16() => 
            PreparePalette(PixelFormat.Format4bppIndexed);

        private static Color[] PreparePalette2() => 
            PreparePalette(PixelFormat.Format1bppIndexed);

        private static Color[] PreparePalette256() => 
            PreparePalette(PixelFormat.Format8bppIndexed);
    }
}

