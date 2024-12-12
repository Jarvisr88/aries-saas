namespace DevExpress.XtraExport.Xls
{
    using System;

    public static class XlsDefs
    {
        public const int MaxRecordDataSize = 0x2020;
        public const int MaxFontNameLength = 0x1f;
        public const int MaxSheetNameLength = 0x1f;
        public const int MaxFontRecordsCount = 510;
        public const int MaxFormatRecordsCount = 0xda;
        public const int MinStringsInBucket = 8;
        public const int MaxHeaderFooterLength = 0xff;
        public const int MaxColumnCount = 0x100;
        public const int MaxRowCount = 0x10000;
        public const int MaxLelIndex = 0x800;
        public const int MaxMergeCellCount = 0x402;
        public const int MaxOutlineLevel = 7;
        public const int MaxXFCount = 0xfd2;
        public const int MaxStyleXFCount = 0x800;
        public const int MaxCellXFCount = 0x800;
        public const int MaxDefaultRowHeight = 0x1ff3;
        public const double MinMarginInInches = 0.0;
        public const double MaxMarginInInches = 49.0;
        public const int MaxCFRefCount = 0x201;
        public const int MaxFormulaStringLength = 0x400;
        public const int MaxFormulaBytesSize = 0x708;
        public const int MaxDataValidationRecordCount = 0xfffe;
        public const int MaxDataValidationSqRefCount = 0x1b0;
        public const int MaxDataValidationTitleLength = 0x20;
        public const int MaxDataValidationPromptLength = 0xff;
        public const int MaxDataValidationErrorLength = 0xe1;
        public const int UnusedFontIndex = 4;
        public const short DefaultColumnWidth = 8;
        public const int DefaultRowHeightInTwips = 0xff;
        public const int FullRangeColumnIndex = 0x100;
        public const int NoScope = -2;
        public const short BIFFVersion = 0x600;
        public const short BIFF5Version = 0x500;
        public const short DefaultBuildYear = 0x7cd;
        public const short DefaultBuildIdentifier = 0x3267;
        public const int DefaultVersionXL = 14;
        public const byte OldestVersionExcel = 0;
        public const double DefaultHeaderFooterMargin = 0.3;
        public const double DefaultLeftRightMargin = 0.7;
        public const double DefaultTopBottomMargin = 0.75;
        public const int DefaultStyleXFIndex = 0;
        public const int DefaultCellXFIndex = 15;
    }
}

