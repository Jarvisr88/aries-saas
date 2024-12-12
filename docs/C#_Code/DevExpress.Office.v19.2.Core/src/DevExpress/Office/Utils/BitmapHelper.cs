namespace DevExpress.Office.Utils
{
    using DevExpress.Office.Model;
    using System;
    using System.Drawing;
    using System.IO;

    public sealed class BitmapHelper
    {
        private BitmapHelper()
        {
        }

        public static MemoryStreamBasedImage CreateBitmap(MemoryStream stream, int width, int height, int colorPlanesCount, int bitsPerPixel, int bytesInLine) => 
            CreateBitmap(stream, width, height, colorPlanesCount, bitsPerPixel, bytesInLine);

        public static MemoryStreamBasedImage CreateBitmap(MemoryStream stream, int width, int height, int colorPlanesCount, int bitsPerPixel, int bytesInLine, IUniqueImageId imageId)
        {
            MemoryStream output = new MemoryStream();
            using (BinaryWriter writer = new BinaryWriter(output))
            {
                Color[] palette = PaletteHelper.GetPalette(bitsPerPixel);
                int num = 0x36;
                int num2 = palette.Length * 4;
                BitmapFileHelper.WriteBITMAPFILEHEADER(writer, (num + num2) + ((int) stream.Length), num + num2);
                BitmapFileHelper.WriteBITMAPINFOHEADER(writer, width, height, colorPlanesCount, bitsPerPixel, bytesInLine, palette);
                BitmapFileHelper.WritePalette(writer, palette);
                stream.WriteTo(output);
            }
            return ImageLoaderHelper.ImageFromStream(new MemoryStream(output.GetBuffer()), imageId);
        }
    }
}

