namespace DevExpress.Data.Utils
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Security;

    public class ImageTool
    {
        private const int MM_ANISOTROPIC = 8;

        public static RectangleF CalculateImageRect(RectangleF clientRect, SizeF imageSize, ImageSizeMode sizeMode);
        public static RectangleF CalculateImageRect(RectangleF clientRect, SizeF imageSize, ImageSizeMode sizeMode, ImageAlignment alignment);
        public static RectangleF CalculateImageRectCore(RectangleF clientRect, SizeF imageSize, ImageSizeMode sizeMode);
        public static RectangleF CalculateImageRectCore(RectangleF clientRect, SizeF imageSize, ImageSizeMode sizeMode, ImageAlignment alignment);
        private static ushort CalcWmfHeaderChecksum(byte[] headerBytes);
        private static byte[] CorrectPlaceableWmf(Metafile mf, byte[] data);
        [DllImport("gdi32")]
        private static extern bool DeleteEnhMetaFile(IntPtr hemf);
        [DllImport("gdi32")]
        private static extern bool DeleteMetaFile(IntPtr hmf);
        private static ImageCodecInfo FindEncoder(ImageFormat format);
        public Image FromArray(byte[] buffer);
        protected virtual Image FromArrayCore(byte[] buffer, int offset);
        [DllImport("gdiplus")]
        private static extern uint GdipEmfToWmfBits(IntPtr hemf, uint bufferSize, byte[] buffer, int mappingMode, ImageTool.EmfToWmfBitsFlags flags);
        [DllImport("gdi32")]
        private static extern int GetEnhMetaFileBits(IntPtr hemf, int cbBuffer, byte[] lpbBuffer);
        [SecuritySafeCritical]
        private static byte[] GetMetafileArray(Metafile srcMetafile);
        [DllImport("gdi32")]
        private static extern int GetMetaFileBitsEx(IntPtr hmf, int cbBuffer, byte[] lpbBuffer);
        private static byte[] GetWmfImageArray(Image image);
        public static Image ImageFromStream(Stream stream);
        public static Image ImageFromStream(Stream stream, bool useEmbeddedColorManagement);
        public void SaveImage(Image img, Stream stream, ImageFormat format);
        public virtual byte[] ToArray(Image img);
        public byte[] ToArray(Image img, ImageFormat format);
        protected byte[] ToArrayCore(Image img, ImageFormat format);

        private static bool IsWin7 { get; }

        private static bool IsUnmanagedCodeGranted { get; }

        private enum EmfToWmfBitsFlags
        {
            public const ImageTool.EmfToWmfBitsFlags EmfToWmfBitsFlagsDefault = ImageTool.EmfToWmfBitsFlags.EmfToWmfBitsFlagsDefault;,
            public const ImageTool.EmfToWmfBitsFlags EmfToWmfBitsFlagsEmbedEmf = ImageTool.EmfToWmfBitsFlags.EmfToWmfBitsFlagsEmbedEmf;,
            public const ImageTool.EmfToWmfBitsFlags EmfToWmfBitsFlagsIncludePlaceable = ImageTool.EmfToWmfBitsFlags.EmfToWmfBitsFlagsIncludePlaceable;,
            public const ImageTool.EmfToWmfBitsFlags EmfToWmfBitsFlagsNoXORClip = ImageTool.EmfToWmfBitsFlags.EmfToWmfBitsFlagsNoXORClip;
        }
    }
}

