namespace DevExpress.XtraPrinting.BarCode.Native
{
    using System;

    [CLSCompliant(false)]
    public class QRCodeAlphaNumericPatternProcessor : QRCodeNumericPatternProcessor
    {
        protected override int GetCodeWordCount(int[] dataValue, ref int dataCounter, int dataLength, sbyte[] dataBits);
        public override string GetValidCharset();
    }
}

