namespace DevExpress.XtraPrinting.BarCode.Native
{
    using System;
    using System.Runtime.InteropServices;

    public class DataMatrixC40PatternProcessor : DataMatrixPatternProcessor
    {
        private int c40Len;
        private byte[] c40Buf;

        public DataMatrixC40PatternProcessor();
        private void C40ToBytes(byte[] byteBuf, int byteSize, ref int byteLen);
        private void CharToC40(byte inChar);
        protected override bool EncodeData(object data, byte[] encodeBuf, out int encodeBufSize);
        public override string GetValidCharSet();

        protected virtual string BasicCharset { get; }

        protected virtual string Shift2Charset { get; }

        protected virtual string Shift3Charset { get; }

        protected virtual byte Latch { get; }

        protected virtual bool OnlyBasicCharset { get; }

        protected virtual byte UnknownChar { get; }

        protected virtual bool UpperShift { get; }
    }
}

