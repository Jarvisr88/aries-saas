namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Security;
    using System.Windows.Media;

    [SecuritySafeCritical]
    internal sealed class GlowBitmap : DisposableObject
    {
        private static readonly GlowBitmap.CachedBitmapInfo[] transparencyMasks;
        public const int GlowBitmapPartCount = 0x10;
        private const int BytesPerPixelBgra32 = 4;
        private readonly IntPtr hBitmap;
        private readonly IntPtr pbits;
        private readonly BITMAPINFO bitmapInfo;

        static GlowBitmap();
        [SecuritySafeCritical]
        public GlowBitmap(IntPtr hdcScreen, int width, int height);
        [SecuritySafeCritical]
        public static GlowBitmap Create(GlowDrawingContext drawingContext, GlowBitmapPart bitmapPart, Color color);
        [SecuritySafeCritical]
        protected override void DisposeNativeResources();
        private static GlowBitmap.CachedBitmapInfo GetOrCreateAlphaMask(GlowBitmapPart bitmapPart);
        public static Uri MakePackUri(string bitmapName);
        private static byte PremultiplyAlpha(byte channel, byte alpha);

        public IntPtr Handle { get; }

        public IntPtr DIBits { get; }

        public int Width { get; }

        public int Height { get; }

        private sealed class CachedBitmapInfo
        {
            public readonly int Width;
            public readonly int Height;
            public readonly byte[] DIBits;

            public CachedBitmapInfo(byte[] diBits, int width, int height);
        }
    }
}

