namespace DevExpress.Export.Xl
{
    using System;

    public enum XlDynamicFilterType
    {
        AboveAverage = 1,
        BelowAverage = 2,
        Tomorrow = 8,
        Today = 9,
        Yesterday = 10,
        NextWeek = 11,
        ThisWeek = 12,
        LastWeek = 13,
        NextMonth = 14,
        ThisMonth = 15,
        LastMonth = 0x10,
        NextQuarter = 0x11,
        ThisQuarter = 0x12,
        LastQuarter = 0x13,
        NextYear = 20,
        ThisYear = 0x15,
        LastYear = 0x16,
        YearToDate = 0x17,
        Quarter1 = 0x18,
        Quarter2 = 0x19,
        Quarter3 = 0x1a,
        Quarter4 = 0x1b,
        Month1 = 0x1c,
        Month2 = 0x1d,
        Month3 = 30,
        Month4 = 0x1f,
        Month5 = 0x20,
        Month6 = 0x21,
        Month7 = 0x22,
        Month8 = 0x23,
        Month9 = 0x24,
        Month10 = 0x25,
        Month11 = 0x26,
        Month12 = 0x27
    }
}

