namespace DevExpress.Data.Printing.Native
{
    using System;
    using System.Drawing;
    using System.Security;

    public static class DxDibImageConverter
    {
        private const string dxDibHeader = "DXDIB";
        private const int version = 1;

        private static Bitmap CreateBitmap(byte[] src, int startIndex, int width, int height);
        private static Bitmap CreateBitmapManaged(byte[] src, int width, int height);
        [SecuritySafeCritical]
        private static Bitmap CreateBitmapUnmanaged(byte[] src, int startIndex, int width, int height);
        public static Bitmap Decode(byte[] data);
        private static void Decompress(byte[] src, int offset, byte[] dst);
        public static bool IsDxDib(byte[] bytes);
        private static int ReadInt(byte[] src, ref int index);

        public static bool HasUnmanagedCodePermitted { get; }
    }
}

