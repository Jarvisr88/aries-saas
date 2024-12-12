namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;

    public static class BitmapUtils
    {
        private const int byteCorrection = 4;

        public static int CalculateCorrectedByteWidth(int byteWidth)
        {
            int num = (byteWidth / 4) * 4;
            if ((byteWidth % 4) > 0)
            {
                num += 4;
            }
            return num;
        }

        public static byte[] ImageToByteArray(Image image)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                image.Save(stream, ImageFormat.Bmp);
                return stream.ToArray();
            }
        }
    }
}

