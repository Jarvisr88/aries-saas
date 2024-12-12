namespace DevExpress.XtraPrinting.BarCode.Native
{
    using System;
    using System.Runtime.InteropServices;

    public class DataMatrixASCIIPatternProcessor : DataMatrixPatternProcessor
    {
        private static string validCharset;

        static DataMatrixASCIIPatternProcessor();
        protected override bool EncodeData(object data, byte[] encodeBuf, out int encodeBufSize);
        private static byte GetDigit(byte b);
        public override string GetValidCharSet();
        private static bool IsDigit(byte b);
        public static bool TextToAscii(byte[] asciiBuf, int asciiBufSize, ref int asciiLen, byte[] textBuf, int textSize, ref int textPtr);

        public static string ValidCharSet { get; }
    }
}

