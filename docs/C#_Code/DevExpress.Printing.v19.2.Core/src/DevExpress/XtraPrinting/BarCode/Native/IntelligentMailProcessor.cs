namespace DevExpress.XtraPrinting.BarCode.Native
{
    using DevExpress.XtraPrinting.BarCode;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal class IntelligentMailProcessor : IPatternProcessor
    {
        private IntelligentMailProcessor.Codeword[] codewords;
        private ArrayList pattern;
        private const short BitsInShort = 0x10;
        private const short BitsInByte = 8;
        private const int Table2Of13Size = 0x4e;
        private const int Table5Of13Size = 0x507;
        private const int TablesSize = 0x555;
        private const short CharacterBitCount = 13;
        private const short CodewordCount = 10;
        private const short MaxCharacter = 0x1fff;
        private static int[] table2Of13;
        private static int[] table5Of13;
        private static Dictionary<int, long> correctionValuesByLength;
        private static readonly int[] barTopCharIndexes;
        private static readonly int[] barBottomCharIndexes;
        private static readonly int[] barTopCharShifts;
        private static readonly int[] barBottomCharShifts;

        static IntelligentMailProcessor();
        public IntelligentMailProcessor();
        private static int CalculateFcs(int[] binaryDataFcs);
        public ArrayList CalculatePattern(string source);
        private void ConvertBinaryDataToCodewords(string binaryDataString);
        void IPatternProcessor.Assign(IPatternProcessor source);
        void IPatternProcessor.RefreshPattern(object data);
        private static string DivideString(string divident, long divider, out long reminder);
        private static string GetTrackingCodeWithoutBarcodeId(string source);
        private static string GetZip(string source);
        private static void InitializeNOf13Table(int[] table, int n);
        private static int[] MakeBinaryData(string source, out string binaryDataString);
        private static long MakeRoutingCodeConversion(string source);
        private static int MathReverse(int value);
        private static int[] StringBase10ToArrayIntBase32(string source);

        ArrayList IPatternProcessor.Pattern { get; }

        [StructLayout(LayoutKind.Sequential)]
        private struct Codeword
        {
            public int Divider { get; set; }
            public int Character { get; set; }
            public int NewCharacter { get; set; }
        }
    }
}

