namespace DevExpress.XtraPrinting.BarCode
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public abstract class DataBarPatternProcessor
    {
        protected const int countIndentForSeparation = 4;
        protected const int fixedHeightSeparation = 1;
        protected static bool[] indentForSeparation;
        protected static int[] generalPatternLimiter;
        private const string defaultFNC1Subst = "#";
        protected const char fnc1Char = '\x00e8';
        private DataBarType type;
        protected int segmentPairsInRow;
        protected string fnc1Subst;

        static DataBarPatternProcessor();
        protected DataBarPatternProcessor();
        public void Assign(DataBarPatternProcessor source);
        private int Combins(int n, int r);
        protected int[] ComposeOddEven(int[] oddPart, int[] evenPart, int count);
        protected List<PatternElement> ConvertBooleanArrayToPattern(List<List<bool>> array);
        protected List<List<bool>> ConvertPatternToBooleanArray(List<PatternElement> pattern);
        public static DataBarPatternProcessor CreateInstance(DataBarType type);
        public abstract ArrayList GetPattern(string text);
        protected int[] GetRSSWidths(int value, int n, int elements, int maxWidth, bool noNarrow);
        public virtual bool IsValidTextFormat(string text);
        protected int[] SubPattern(int data, StructSymbol symbol, int elementsPair, bool oddFlag);
        protected int[] SubPatternInverted(int data, StructSymbol symbol, int elementsPair, bool oddFlag);

        public int SegmentsInRow { get; set; }

        public DataBarType Type { get; set; }

        public string FNC1Substitute { get; set; }
    }
}

