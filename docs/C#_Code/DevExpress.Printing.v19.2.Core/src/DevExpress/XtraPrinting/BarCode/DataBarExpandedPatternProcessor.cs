namespace DevExpress.XtraPrinting.BarCode
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;

    public class DataBarExpandedPatternProcessor : DataBarPatternProcessor
    {
        private const int elementsPairExpanded = 4;
        private const int checkSumModifierExpanded = 0xd3;
        private const int barsInSegmentPair = 0x15;
        private static int[] numbersAI;
        private static int[] specialPatternLimiter;
        private static bool[] boolFinderPatternType1;
        private static bool[] boolFinderPatternType2;
        private static bool[] boolPatternLimiter;
        private static int[,] weightChecksumElemExpanded;
        private static int[,] weightRowsExpanded;
        private static int[,] widthsChecksumPatternExpanded;
        private static int[,] checksumPatternExpandedSequence;
        private static StructSymbol[] symbolExpanded;

        static DataBarExpandedPatternProcessor();
        public DataBarExpandedPatternProcessor(DataBarType type);
        private void AddBinaryCompressedData(int encodingMethod, List<GS1Helper.ElementResult> textElement, StringBuilder resultBinaryString);
        protected List<PatternElement> AddStackedExpandedPattern(List<PatternElement> pattern, int segmentPairsInRow);
        private bool CanBeNumeric(List<DataBarExpandedPatternProcessor.BlockData> blocks, int startIndex);
        private int[] ConvertBinaryDataToInt(char[] binaryData);
        private string ConvertIntToBinary(int value, int mask, int n);
        private char[] ConvertTextToBinaryData(string text);
        private void ConvertTypedBlocks(List<DataBarExpandedPatternProcessor.BlockData> resultBlock);
        private List<bool> CreateExceptionArray(int segmentPairsInRow);
        private bool FiveAlphaSymbols(List<DataBarExpandedPatternProcessor.BlockData> blocks, int startIndex);
        private bool FixedLengthAI(string ai);
        private int GetEncodingMethod(List<GS1Helper.ElementResult> textElement);
        public override ArrayList GetPattern(string text);
        private bool IsMaxLength(GS1Helper.ElementResult textElement);
        public override bool IsValidTextFormat(string text);
        private void MakeBinaryDataFromUniversalData(string universalData, List<DataBarExpandedPatternProcessor.BlockData> resultBlock, StringBuilder resultBinaryString);
        private char[] MakeBinaryString(int encodingMethod, List<GS1Helper.ElementResult> textElement);
        private string MakeUniversalData(int encodingMethod, List<GS1Helper.ElementResult> textElement);
        private DataBarExpandedPatternProcessor.CodingType[] MakeUniversalDataTypesArray(string universalData);
        private void PadBinaryString(StringBuilder resultBinaryString, string universalData, List<DataBarExpandedPatternProcessor.BlockData> resultBlock, bool lastNumericOddSIze);
        private static List<DataBarExpandedPatternProcessor.BlockData> SeparateToTypedBlocks(DataBarExpandedPatternProcessor.CodingType[] universalDataTypes);
        private static void SetVaiableLengthField(StringBuilder resultBinaryString);
        protected List<PatternElement> SplitExpandedPattern(List<int> pattern, int segmentPairsInRow);

        public class BlockData
        {
            public DataBarExpandedPatternProcessor.CodingType Type;
            public int Length;

            public BlockData();
            public BlockData(DataBarExpandedPatternProcessor.BlockData blockData);
            public BlockData(DataBarExpandedPatternProcessor.CodingType type, int length);
        }

        public enum CodingType
        {
            public const DataBarExpandedPatternProcessor.CodingType ISOIEC = DataBarExpandedPatternProcessor.CodingType.ISOIEC;,
            public const DataBarExpandedPatternProcessor.CodingType ALPHA_OR_ISO = DataBarExpandedPatternProcessor.CodingType.ALPHA_OR_ISO;,
            public const DataBarExpandedPatternProcessor.CodingType ANY_ENC = DataBarExpandedPatternProcessor.CodingType.ANY_ENC;,
            public const DataBarExpandedPatternProcessor.CodingType NUMERIC = DataBarExpandedPatternProcessor.CodingType.NUMERIC;,
            public const DataBarExpandedPatternProcessor.CodingType ALPHA = DataBarExpandedPatternProcessor.CodingType.ALPHA;
        }
    }
}

