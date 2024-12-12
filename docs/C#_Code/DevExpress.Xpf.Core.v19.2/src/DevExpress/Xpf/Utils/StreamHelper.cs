namespace DevExpress.Xpf.Utils
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;

    public static class StreamHelper
    {
        public static byte[] CopyAllBytes(this Stream stream)
        {
            int num;
            if (!stream.CanRead)
            {
                return null;
            }
            if (stream.CanSeek)
            {
                stream.Seek(0L, SeekOrigin.Begin);
            }
            List<byte> list = new List<byte>();
            byte[] buffer = new byte[0x400];
            while ((num = stream.Read(buffer, 0, 0x400)) > 0)
            {
                for (int i = 0; i < num; i++)
                {
                    list.Add(buffer[i]);
                }
            }
            return list.ToArray();
        }

        public static string ToStringWithDispose(this Stream stream)
        {
            string str;
            using (stream)
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    str = reader.ReadToEnd();
                }
            }
            return str;
        }
    }
}

