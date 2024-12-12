namespace DevExpress.Office.Utils
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;

    public static class ChunkedStringBuilderExtensions
    {
        public static void Write(this Stream stream, ChunkedStringBuilder value, Encoding encoding)
        {
            byte[] bytes = new byte[value.MaxBufferSize * 4];
            List<StringBuilder> buffers = value.Buffers;
            int count = buffers.Count;
            for (int i = 0; i < count; i++)
            {
                string s = buffers[i].ToString();
                int num3 = encoding.GetBytes(s, 0, s.Length, bytes, 0);
                stream.Write(bytes, 0, num3);
            }
        }
    }
}

