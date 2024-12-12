namespace DevExpress.Utils
{
    using DevExpress.Utils.Internal;
    using System;
    using System.Text;

    public static class DXEncoding
    {
        public static readonly Encoding UTF8NoByteOrderMarks = new UTF8Encoding(false);
        public static readonly Encoding Default = Encoding.Default;
        public static readonly Encoding ASCII = Encoding.ASCII;

        public static int CharsetFromCodePage(int codePage) => 
            DXCharsetAndCodePageTranslator.Instance.CharsetFromCodePage(codePage);

        public static int CodePageFromCharset(int charset) => 
            DXCharsetAndCodePageTranslator.Instance.CodePageFromCharset(charset);

        public static Encoding GetEncoding(int codePage) => 
            Encoding.GetEncoding(codePage);

        public static Encoding GetEncoding(string name) => 
            Encoding.GetEncoding(name);

        public static int GetEncodingCodePage(Encoding encoding) => 
            encoding.CodePage;

        public static Encoding GetEncodingFromCodePage(int codePage) => 
            (codePage != 0x2a) ? Encoding.GetEncoding(codePage) : EmptyEncoding.Instance;

        public static EncodingInfo[] GetEncodings() => 
            Encoding.GetEncodings();

        public static bool IsSingleByteEncoding(Encoding encoding) => 
            encoding.IsSingleByte;
    }
}

