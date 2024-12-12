namespace DevExpress.XtraPrinting.BarCode
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class DataBarBasePatternProcessor : DataBarPatternProcessor
    {
        private const int elementsPair = 4;
        private const long combSignSymbol = 0x453af5L;
        private const int combSignSymbolIn = 0x63d;
        private const int checkSumModifier = 0x4f;
        private static int[,] widthsChecksumPattern;
        private static int[,] weightChecksumElem;
        private static StructSymbol[] symbolOut;
        private static StructSymbol[] symbolIn;

        static DataBarBasePatternProcessor();
        public DataBarBasePatternProcessor(DataBarType type);
        private List<PatternElement> AddStackedOmnidirectionPattern(List<PatternElement> pattern);
        private List<PatternElement> AddStackedPattern(List<PatternElement> pattern);
        private List<PatternElement> ConvertPatternToNewFormat(List<int> pattern);
        private int[] GetDataType(int[] data);
        public override ArrayList GetPattern(string text);
        private List<int> MakeFinalPattern(int[][] signPattern, int[] checkSumLeftPartPattern, int[] checkSumRightPartPattern);
        private bool SpecailKindPattern(List<bool> testBoolPattern);
        private List<PatternElement> SplitPattern(List<int> pattern, int firstHight, int secondHight);
        private int[] TextToData(string text);
    }
}

