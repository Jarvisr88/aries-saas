namespace DevExpress.Office.Crypto
{
    using DevExpress.Utils;
    using System;
    using System.IO;
    using System.Security.Cryptography;

    public static class MD5Hash
    {
        public static byte[] ComputeHash(Stream stream)
        {
            Guard.ArgumentNotNull(stream, "stream");
            return CreateMD5().ComputeHash(stream);
        }

        public static byte[] ComputeHash(byte[] buffer)
        {
            Guard.ArgumentNotNull(buffer, "buffer");
            return CreateMD5().ComputeHash(buffer);
        }

        public static byte[] ComputeHash(byte[] buffer, int start, int count)
        {
            Guard.ArgumentNotNull(buffer, "buffer");
            Guard.ArgumentNonNegative(start, "start");
            Guard.ArgumentNonNegative(count, "count");
            if (buffer.Length < (start + count))
            {
                throw new ArgumentException("Buffer is not long enough for that start and count!");
            }
            return CreateMD5().ComputeHash(buffer, start, count);
        }

        private static MD5 CreateMD5() => 
            MD5.Create();
    }
}

