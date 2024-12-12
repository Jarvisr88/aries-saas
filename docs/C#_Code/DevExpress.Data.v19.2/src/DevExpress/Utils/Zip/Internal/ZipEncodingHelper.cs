namespace DevExpress.Utils.Zip.Internal
{
    using DevExpress.Utils;
    using System;
    using System.Text;

    public static class ZipEncodingHelper
    {
        public static bool CanCodeToASCII(string sourceString) => 
            CanCodeToEncoding(DXEncoding.ASCII, sourceString);

        public static bool CanCodeToEncoding(Encoding encoding, string sourceString)
        {
            byte[] bytes = encoding.GetBytes(sourceString);
            return (encoding.GetString(bytes, 0, bytes.Length) == sourceString);
        }
    }
}

