namespace DevExpress.Printing.Core.PdfExport.Metafile
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Security;

    public class DIBHelper
    {
        [SecuritySafeCritical]
        public static Bitmap CreateBitmap(byte[] bitmapData, int width, int height, PixelFormat format)
        {
            if (format == PixelFormat.Undefined)
            {
                return new Bitmap(new MemoryStream(bitmapData));
            }
            Bitmap bitmap = new Bitmap(width, height, format);
            int startIndex = 0;
            if ((format & PixelFormat.Indexed) != PixelFormat.Undefined)
            {
                using (BinaryReader reader = new BinaryReader(new MemoryStream(bitmapData)))
                {
                    int num2 = reader.ReadInt32();
                    int num3 = reader.ReadInt32();
                    ColorPalette palette = bitmap.Palette;
                    Color[] entries = palette.Entries;
                    int index = 0;
                    while (true)
                    {
                        if (index >= num3)
                        {
                            startIndex += 4 * (2 + num3);
                            bitmap.Palette = palette;
                            break;
                        }
                        entries[index] = Color.FromArgb(reader.ReadInt32());
                        index++;
                    }
                }
            }
            BitmapData bitmapdata = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.WriteOnly, format);
            Marshal.Copy(bitmapData, startIndex, bitmapdata.Scan0, bitmapdata.Stride * bitmapdata.Height);
            bitmap.UnlockBits(bitmapdata);
            return bitmap;
        }

        [SecuritySafeCritical]
        public static Bitmap CreateBitmapFromDIB(byte[] dibBytes)
        {
            GCHandle handle = GCHandle.Alloc(dibBytes, GCHandleType.Pinned);
            BitmapInfoHeader structure = (BitmapInfoHeader) Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(BitmapInfoHeader));
            if (structure.HeaderSize == 12)
            {
                BitmapCoreHeader header2 = (BitmapCoreHeader) Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(BitmapCoreHeader));
                structure = new BitmapInfoHeader {
                    HeaderSize = header2.HeaderSize,
                    Width = header2.Width,
                    Height = header2.Height,
                    Planes = header2.Planes,
                    BitCount = header2.BitCount
                };
            }
            handle.Free();
            if ((structure.Planes != 1) || (structure.Compression != 0))
            {
                throw new NotSupportedException("Not supported Bitmap format");
            }
            PixelFormat format = PixelFormat.Format24bppRgb;
            BitCount bitCount = (BitCount) structure.BitCount;
            if (bitCount == BitCount.BI_BITCOUNT_4)
            {
                format = PixelFormat.Format16bppRgb555;
            }
            else if (bitCount == BitCount.BI_BITCOUNT_5)
            {
                format = PixelFormat.Format24bppRgb;
            }
            else
            {
                if (bitCount != BitCount.BI_BITCOUNT_6)
                {
                    throw new NotSupportedException("Not supported Bitmap format");
                }
                format = PixelFormat.Format32bppRgb;
            }
            int sourceIndex = Marshal.SizeOf<BitmapInfoHeader>(structure);
            byte[] destinationArray = new byte[dibBytes.Length - sourceIndex];
            Array.Copy(dibBytes, sourceIndex, destinationArray, 0, destinationArray.Length);
            return CreateBitmap(destinationArray, structure.Width, structure.Height, format);
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct BitmapCoreHeader
        {
            public uint HeaderSize;
            public ushort Width;
            public ushort Height;
            public ushort Planes;
            public ushort BitCount;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct BitmapInfoHeader
        {
            public uint HeaderSize;
            public int Width;
            public int Height;
            public ushort Planes;
            public ushort BitCount;
            public uint Compression;
            public uint ImageSize;
            public int XPelsPerMeter;
            public int YPelsPerMeter;
            public uint ColorUsed;
            public uint ColorImportant;
        }
    }
}

