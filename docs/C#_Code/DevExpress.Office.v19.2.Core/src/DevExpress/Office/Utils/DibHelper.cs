namespace DevExpress.Office.Utils
{
    using DevExpress.Office.Model;
    using System;
    using System.IO;
    using System.Text;

    public sealed class DibHelper
    {
        private DibHelper()
        {
        }

        private static MemoryStream CreateBmpFileStreamForDib(Stream dibStream, int dibHeight, int bytesInLine)
        {
            MemoryStream output = new MemoryStream();
            using (BinaryWriter writer = new BinaryWriter(output))
            {
                int length = (int) dibStream.Length;
                BitmapFileHelper.WriteBITMAPFILEHEADER(writer, 14 + length, (length - (dibHeight * bytesInLine)) + 14);
                StreamHelper.WriteTo(dibStream, output);
            }
            return new MemoryStream(output.GetBuffer());
        }

        public static MemoryStreamBasedImage CreateDib(Stream stream, int width, int height, int bytesInLine) => 
            CreateDib(stream, width, height, bytesInLine, null);

        public static MemoryStreamBasedImage CreateDib(Stream stream, int width, int height, int bytesInLine, IUniqueImageId imageId) => 
            ImageLoaderHelper.ImageFromStream(CreateBmpFileStreamForDib(stream, height, bytesInLine), imageId);

        public static MemoryStreamBasedImage CreateDibFromDib12(Stream stream)
        {
            int num;
            int num2;
            short num3;
            byte[] buffer;
            int num4;
            using (BinaryReader reader = new BinaryReader(stream))
            {
                int num5 = reader.ReadInt32();
                num = reader.ReadInt16();
                num2 = reader.ReadInt16();
                reader.ReadInt16();
                num3 = reader.ReadInt16();
                buffer = reader.ReadBytes(((int) stream.Length) - num5);
                num4 = num3 * num;
                num4 = ((num4 % 0x20) != 0) ? (((num4 / 0x20) + 1) * 4) : (num4 / 8);
            }
            MemoryStream output = new MemoryStream();
            using (BinaryWriter writer = new BinaryWriter(output, new UTF8Encoding(false, true), true))
            {
                writer.Write(40);
                writer.Write(num);
                writer.Write(num2);
                writer.Write((short) 1);
                writer.Write(num3);
                writer.Write(0);
                writer.Write(0);
                writer.Write(0);
                writer.Write(0);
                writer.Write(0);
                writer.Write(0);
                writer.Write(buffer);
            }
            output.Position = 0L;
            return CreateDib(output, num, num2, num4);
        }
    }
}

