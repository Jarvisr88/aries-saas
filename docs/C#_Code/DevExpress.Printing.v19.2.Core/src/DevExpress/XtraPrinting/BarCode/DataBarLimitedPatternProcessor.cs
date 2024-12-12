namespace DevExpress.XtraPrinting.BarCode
{
    using System;
    using System.Collections;

    public class DataBarLimitedPatternProcessor : DataBarPatternProcessor
    {
        private const int elementsPairLimited = 7;
        private const long combSignSymbolLimited = 0x1eb983L;
        private const int checkSumModifierLimited = 0x59;
        private static int[] rightPatternLimiter;
        private static int[] sequenceNumber;
        private static StructSymbol[] symbolLimited;
        private static StructSymbol symbolCheckSum;
        private static int[,] weightChecksumElemLimited;

        static DataBarLimitedPatternProcessor();
        private int[] GetDataType(long[] data);
        public override ArrayList GetPattern(string text);
        public override bool IsValidTextFormat(string text);
        private long[] TextToData(string text);
    }
}

