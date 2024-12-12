namespace DevExpress.XtraPrinting.BarCode.Native
{
    using System;
    using System.Runtime.InteropServices;

    public class DataMatrixEdifactPatternProcessor : DataMatrixPatternProcessor
    {
        private static string validCharset;
        private byte[] byteText;
        private int textPtrAlignedToEdifactWord;
        private int encodePtrAlignedToEdifactWord;
        private int edifactLen;
        private byte[] edifact;

        static DataMatrixEdifactPatternProcessor();
        public DataMatrixEdifactPatternProcessor();
        private void CharToEdifact(byte inChar);
        private void ConvertToEdifactCharset(byte[] byteText, int byteSize);
        private int EdifactToBytes(byte[] byteBuf, int byteSize, ref int byteLen);
        protected override bool EncodeData(object data, byte[] encodeBuf, out int encodeBufSize);
        public override string GetValidCharSet();
        protected override void PadData(byte[] encodeBuf, ref int protectedDataSize);
    }
}

