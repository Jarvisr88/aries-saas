namespace DevExpress.XtraExport.Xls
{
    using System;

    public enum XlsCFRuleTemplate
    {
        CellValue = 0,
        Formula = 1,
        ColorScale = 2,
        DataBar = 3,
        IconSet = 4,
        Filter = 5,
        UniqueValues = 7,
        ContainsText = 8,
        ContainsBlanks = 9,
        ContainsNoBlanks = 10,
        ContainsErrors = 11,
        ContainsNoError = 12,
        Today = 15,
        Tomorrow = 0x10,
        Yesterday = 0x11,
        Last7Days = 0x12,
        LastMonth = 0x13,
        NextMonth = 20,
        ThisWeek = 0x15,
        NextWeek = 0x16,
        LastWeek = 0x17,
        ThisMonth = 0x18,
        AboveAverage = 0x19,
        BelowAverage = 0x1a,
        DuplicateValues = 0x1b,
        AboveOrEqualToAverage = 0x1d,
        BelowOrEqualToAverage = 30
    }
}

