namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct BITMAPINFO
    {
        internal int biSize;
        internal int biWidth;
        internal int biHeight;
        internal short biPlanes;
        internal short biBitCount;
        internal int biCompression;
        internal int biSizeImage;
        internal int biXPelsPerMeter;
        internal int biYPelsPerMeter;
        internal int biClrUsed;
        internal int biClrImportant;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=0x400)]
        internal byte[] bmiColors;
        internal static BITMAPINFO Default { get; }
    }
}

