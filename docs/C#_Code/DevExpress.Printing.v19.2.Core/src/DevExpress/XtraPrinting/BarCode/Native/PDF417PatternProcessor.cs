namespace DevExpress.XtraPrinting.BarCode.Native
{
    using DevExpress.XtraPrinting.BarCode;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;

    public abstract class PDF417PatternProcessor : IPatternProcessor
    {
        private int columns;
        private int rows;
        private bool truncateSymbol;
        private DevExpress.XtraPrinting.BarCode.ErrorCorrectionLevel errorCorrectionLevel;
        private int[] encodedBuf;
        private int[] errorCorrectionCodes;
        private int[] encodedData;
        private int countAferEncode;
        private List<List<bool>> pattern;
        private List<bool> currentRow;

        protected PDF417PatternProcessor();
        protected void CalculatePattern();
        private void CreateEndOfRow(int index);
        protected void CreateErrorCorrection();
        public static PDF417PatternProcessor CreateInstance(PDF417CompactionMode mode);
        private void CreateNewString();
        private void CreateStartOfRow(int index);
        void IPatternProcessor.Assign(IPatternProcessor source);
        void IPatternProcessor.RefreshPattern(object data);
        protected abstract void ErrorCorrection(int countAfterEncode, int correctionWordsCount, int index, int[] encodedBuf, int[] encodedData);
        protected void FillPattern();
        private int GetCorrectionWordsCount();
        protected abstract int GetCountAfterEncode(int index, int correctionWordsCount);
        private int GetErrorCorrectionCoefficient(int index);
        private int GetLeftRowIndicator(int row_index);
        private int GetRightRowIndicator(int row_index);
        private int GetRowIndicator(int row_index, int firstRest, int secondRest, int thirdRest);
        private int GetRows(int countAferEncode);
        protected abstract bool IsValidData(object data);
        protected abstract void SetData(object data);
        private void UpdatePattern(int numPattern);
        protected abstract int WriteDataInEncodedBuffer(int[] encodedBuf);

        [DefaultValue(1)]
        public int Columns { get; set; }

        [DefaultValue(3)]
        public int Rows { get; set; }

        [DefaultValue(2)]
        public DevExpress.XtraPrinting.BarCode.ErrorCorrectionLevel ErrorCorrectionLevel { get; set; }

        [DefaultValue(false)]
        public bool TruncateSymbol { get; set; }

        ArrayList IPatternProcessor.Pattern { get; }
    }
}

