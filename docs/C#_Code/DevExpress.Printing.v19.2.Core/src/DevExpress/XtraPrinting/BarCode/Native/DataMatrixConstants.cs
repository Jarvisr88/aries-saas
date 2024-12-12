namespace DevExpress.XtraPrinting.BarCode.Native
{
    using System;

    public static class DataMatrixConstants
    {
        public const byte AsciiPad = 0x81;
        public const byte AsciiUpperShift = 0xeb;
        public const byte AsciiLatchToC40 = 230;
        public const byte AsciiLatchToX12 = 0xee;
        public const byte AsciiLatchToText = 0xef;
        public const byte AsciiLatchToB256 = 0xe7;
        public const byte AsciiLatchToEdifact = 240;
        public const byte C40X12TextUnlatch = 0xfe;
        public const byte EdifactUnlatch = 0x5f;
    }
}

