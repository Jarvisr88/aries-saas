namespace DevExpress.XtraPrinting.BarCode.Native
{
    using System;

    [CLSCompliant(false)]
    public class QRCodeNumericPatternProcessor : QRCodePatternProcessor
    {
        private static byte[] GetAsciiBytes(string value);
        private static string GetAsciiString(byte[] ascii);
        protected override int GetCodeWordCount(int[] dataValue, ref int dataCounter, int dataLength, sbyte[] dataBits);
        protected override int[] GetCodeWordNumPlus();
        public override string GetValidCharset();
        public override bool IsValidData(object data);
        protected override void SetData(object data);
    }
}

