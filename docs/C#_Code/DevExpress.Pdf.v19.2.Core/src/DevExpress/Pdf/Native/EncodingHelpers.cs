namespace DevExpress.Pdf.Native
{
    using System;
    using System.Text;

    internal static class EncodingHelpers
    {
        private const string winAnsiEncodingName = "Windows-1252";
        private static readonly Encoding ansiEncoding = DXEncoding.GetEncoding("Windows-1252");

        public static Encoding AnsiEncoding =>
            ansiEncoding;
    }
}

