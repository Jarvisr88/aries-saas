namespace DevExpress.Office.Utils
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;

    public static class StreamExtensions
    {
        public static void ReadToEnd(this Stream stream)
        {
            byte[] buffer = new byte[0x2000];
            while (true)
            {
                int num = stream.Read(buffer, 0, buffer.Length);
                if (num < buffer.Length)
                {
                    return;
                }
            }
        }

        public static void Write(this Stream stream, string value, Encoding encoding)
        {
            byte[] bytes = new byte[0x8000];
            int length = value.Length;
            int charIndex = 0;
            while (length > 0x2000)
            {
                int count = encoding.GetBytes(value, charIndex, 0x2000, bytes, 0);
                stream.Write(bytes, 0, count);
                length -= 0x2000;
                charIndex += 0x2000;
            }
            if (length > 0)
            {
                stream.Write(bytes, 0, encoding.GetBytes(value, charIndex, length, bytes, 0));
            }
        }

        public static void Write(this Stream stream, StringBuilder value, Encoding encoding)
        {
            byte[] buffer = new byte[0x8000];
            stream.Write(value, encoding, buffer, 0x8000);
        }

        public static void Write(this Stream stream, StringBuilder value, Encoding encoding, byte[] buffer, int bufferSize)
        {
            int charCount = bufferSize / 4;
            int length = value.Length;
            int startIndex = 0;
            while (length > charCount)
            {
                int count = encoding.GetBytes(value.ToString(startIndex, charCount), 0, charCount, buffer, 0);
                stream.Write(buffer, 0, count);
                length -= charCount;
                startIndex += charCount;
            }
            if (length > 0)
            {
                int count = encoding.GetBytes(value.ToString(startIndex, length), 0, length, buffer, 0);
                stream.Write(buffer, 0, count);
            }
        }
    }
}

