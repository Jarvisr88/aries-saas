namespace DevExpress.Utils.Zip.Internal
{
    using System;
    using System.IO;

    public static class StreamUtils
    {
        public static void CopyStream(Stream sourceStream, Stream targetStream)
        {
            CopyStream(sourceStream, targetStream, null);
        }

        public static void CopyStream(Stream sourceStream, Stream targetStream, CopyProgressHandler copyDelegate)
        {
            byte[] buffer = new byte[0x8000];
            int count = 0;
            bool flag = true;
            while (true)
            {
                count = sourceStream.Read(buffer, 0, 0x8000);
                targetStream.Write(buffer, 0, count);
                if (copyDelegate != null)
                {
                    flag = copyDelegate(count);
                }
                if (!((count == 0x8000) & flag))
                {
                    return;
                }
            }
        }

        public static void MakeReadingPass(Stream sourceStream, CopyProgressHandler copyDelegate)
        {
            byte[] buffer = new byte[0x8000];
            int bytesCopied = 0;
            bool flag = true;
            while (true)
            {
                bytesCopied = sourceStream.Read(buffer, 0, 0x8000);
                if (copyDelegate != null)
                {
                    flag = copyDelegate(bytesCopied);
                }
                if (!((bytesCopied == 0x8000) & flag))
                {
                    return;
                }
            }
        }
    }
}

