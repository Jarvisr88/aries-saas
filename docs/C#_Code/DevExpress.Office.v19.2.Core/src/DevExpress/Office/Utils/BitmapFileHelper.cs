namespace DevExpress.Office.Utils
{
    using System;
    using System.Drawing;
    using System.IO;

    public sealed class BitmapFileHelper
    {
        public const int sizeofBITMAPFILEHEADER = 14;
        public const int sizeofBITMAPINFOHEADER = 40;

        private BitmapFileHelper()
        {
        }

        public static void WriteBITMAPFILEHEADER(BinaryWriter writer, int fileSize, int bitsOffset)
        {
            writer.Write((byte) 0x42);
            writer.Write((byte) 0x4d);
            writer.Write(fileSize);
            writer.Write((short) 0);
            writer.Write((short) 0);
            writer.Write(bitsOffset);
        }

        public static void WriteBITMAPINFOHEADER(BinaryWriter writer, int width, int height, int colorPlanesCount, int bitsPerPixel, int bytesInLine, Color[] palette)
        {
            int num = height * bytesInLine;
            writer.Write(40);
            writer.Write(width);
            writer.Write(height);
            writer.Write((short) colorPlanesCount);
            writer.Write((short) bitsPerPixel);
            writer.Write(0);
            writer.Write(num);
            writer.Write(0);
            writer.Write(0);
            writer.Write(palette.Length);
            writer.Write(0);
        }

        public static void WritePalette(BinaryWriter writer, Color[] palette)
        {
            int length = palette.Length;
            for (int i = 0; i < length; i++)
            {
                Color color = palette[i];
                writer.Write(color.B);
                writer.Write(color.G);
                writer.Write(color.R);
                writer.Write((byte) 0);
            }
        }
    }
}

