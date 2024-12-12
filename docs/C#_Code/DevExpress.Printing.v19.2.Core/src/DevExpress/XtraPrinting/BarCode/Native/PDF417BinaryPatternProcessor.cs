namespace DevExpress.XtraPrinting.BarCode.Native
{
    using System;

    public class PDF417BinaryPatternProcessor : PDF417PatternProcessor
    {
        private byte[] data;

        public PDF417BinaryPatternProcessor();
        protected override void ErrorCorrection(int countAfterEncode, int correctionWordsCount, int index, int[] encodedBuf, int[] encodedData);
        protected override int GetCountAfterEncode(int index, int correctionWordsCount);
        protected override bool IsValidData(object data);
        protected override void SetData(object data);
        protected override int WriteDataInEncodedBuffer(int[] encodedBuf);
    }
}

