namespace DevExpress.XtraPrinting.BarCode.Native
{
    using System;

    public class PDF417Constants
    {
        public const int StartPattern = 0x1fea8;
        public const int StopPattern = 0x3fa29;
        public const int TruncatedStopPattern = 3;
        public const int MinRowsCount = 3;
        public const int MinColumnsCount = 1;
        public const int MaxRowsCount = 90;
        public const int MaxColumnsCount = 30;
        public const int MaxCodewordsCapacity = 0x3a0;
        public const int MaxCodewordsCount = 0x3a1;
        public const int MaxSizeOfBuffer = 0xbb8;
        public const int MaxCharsCountInRow = 30;
        public const int MaxTextLength = 0x6be;
        public const int MaxDataLength = 0x409;
        public const long ByteCalculationCoefficient = 0x100L;
        public const long ByteConversionCoefficient = 900L;
        public const int ErrorCorrectionPlacebo = 900;
        public const int CalculateDegree = 6;
        public const int EndLatchToByteCompaction = 0x39c;
        public const int ModeLatchToByteCompaction = 0x385;
        public const float MinYToXRatio = 3f;
        public static readonly int[,] PDF417Bits;

        static PDF417Constants();
    }
}

