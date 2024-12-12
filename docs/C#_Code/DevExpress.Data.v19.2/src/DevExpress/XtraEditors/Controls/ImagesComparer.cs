namespace DevExpress.XtraEditors.Controls
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;

    public class ImagesComparer
    {
        public static DevExpress.XtraEditors.Controls.CryptoServiceProvider CryptoServiceProvider;

        public static bool AreEqual(Image imageA, Image imageB);
        private static byte[] ConvertImageToByteArray(Image image);
        private static ImageFormat FindAppropriateImageFormat(ImageFormat imageFormat);
        private static string GetHashString(byte[] arrayToHash);
        public static string GetImageHash(Image image);
    }
}

