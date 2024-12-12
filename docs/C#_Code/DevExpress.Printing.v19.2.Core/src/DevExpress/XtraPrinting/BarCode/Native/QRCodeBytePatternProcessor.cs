namespace DevExpress.XtraPrinting.BarCode.Native
{
    using System;

    [CLSCompliant(false)]
    public class QRCodeBytePatternProcessor : QRCodePatternProcessor
    {
        protected override int GetCodeWordCount(int[] dataValue, ref int dataCounter, int dataLength, sbyte[] dataBits);
        protected override int[] GetCodeWordNumPlus();
        internal override int GetMaxDataLength();
        private int GetMaxDataSize();
        public override string GetValidCharset();
        public override bool IsValidData(object data);
        protected override void SetData(object data);
    }
}

