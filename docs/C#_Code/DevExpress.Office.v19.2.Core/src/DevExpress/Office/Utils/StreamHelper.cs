namespace DevExpress.Office.Utils
{
    using System;
    using System.IO;

    public static class StreamHelper
    {
        public static void WriteTo(Stream inputStream, Stream outputStream)
        {
            byte[] buffer = new byte[0x400];
            while (true)
            {
                int count = inputStream.Read(buffer, 0, buffer.Length);
                if (count == 0)
                {
                    return;
                }
                outputStream.Write(buffer, 0, count);
            }
        }
    }
}

