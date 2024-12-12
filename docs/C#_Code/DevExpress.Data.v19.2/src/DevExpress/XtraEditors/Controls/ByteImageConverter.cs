namespace DevExpress.XtraEditors.Controls
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Runtime.CompilerServices;

    public class ByteImageConverter
    {
        private static bool CanSave(ImageFormat format);
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static byte[] FromBinary(object obj);
        public static Image FromByteArray(byte[] b);
        protected static Image FromByteArray(byte[] b, int offset);
        public static ImageFormat GetImageFormatByPixelFormat(Image image);
        private static byte[] MetafileToByteArray(Image img);
        public static byte[] ToByteArray(object obj);
        public static byte[] ToByteArray(Image image, ImageFormat imageFormat);
    }
}

