namespace DevExpress.XtraPrinting.BarCode.Native
{
    using System;
    using System.Collections.Generic;

    public class PDF417TextPatternProcessor : PDF417PatternProcessor
    {
        private string text;

        public PDF417TextPatternProcessor();
        protected override void ErrorCorrection(int countAfterEncode, int correctionWordsCount, int index, int[] encodedBuf, int[] encodedData);
        protected override int GetCountAfterEncode(int index, int correctionWordsCount);
        protected override bool IsValidData(object data);
        protected override void SetData(object data);
        protected override int WriteDataInEncodedBuffer(int[] encodedBuf);

        private enum TextMode
        {
            public const PDF417TextPatternProcessor.TextMode UpperCase = PDF417TextPatternProcessor.TextMode.UpperCase;,
            public const PDF417TextPatternProcessor.TextMode LowerCase = PDF417TextPatternProcessor.TextMode.LowerCase;,
            public const PDF417TextPatternProcessor.TextMode Digit = PDF417TextPatternProcessor.TextMode.Digit;,
            public const PDF417TextPatternProcessor.TextMode Symbol = PDF417TextPatternProcessor.TextMode.Symbol;
        }

        private class TextModeHelper
        {
            private Dictionary<char, int> digitTable;
            private Dictionary<char, int> symbolTable;
            private DevExpress.XtraPrinting.BarCode.Native.PDF417TextPatternProcessor.TextMode textMode;
            private int textValue;
            private char ch;

            public TextModeHelper(char ch);
            private int GetLowerValue();
            public static int GetPDF417TextMode(DevExpress.XtraPrinting.BarCode.Native.PDF417TextPatternProcessor.TextMode textMode, DevExpress.XtraPrinting.BarCode.Native.PDF417TextPatternProcessor.TextMode previousTextMode);
            public static DevExpress.XtraPrinting.BarCode.Native.PDF417TextPatternProcessor.TextMode GetPreviousLetterTextMode(DevExpress.XtraPrinting.BarCode.Native.PDF417TextPatternProcessor.TextMode textMode, DevExpress.XtraPrinting.BarCode.Native.PDF417TextPatternProcessor.TextMode previousTextMode, DevExpress.XtraPrinting.BarCode.Native.PDF417TextPatternProcessor.TextMode previousLetterTextMode);
            private int GetUpperValue();
            private void InitializeTables();
            private void SetModeAndValue();

            public DevExpress.XtraPrinting.BarCode.Native.PDF417TextPatternProcessor.TextMode TextMode { get; }

            public int TextValue { get; }
        }
    }
}

