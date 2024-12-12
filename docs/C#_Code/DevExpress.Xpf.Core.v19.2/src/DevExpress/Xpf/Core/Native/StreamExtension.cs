namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public static class StreamExtension
    {
        public static byte[] GetDataFromStream(this Stream stream);
        public static string ReadString(this Stream stream);
        public static string ReadToString(this Stream stream);
    }
}

