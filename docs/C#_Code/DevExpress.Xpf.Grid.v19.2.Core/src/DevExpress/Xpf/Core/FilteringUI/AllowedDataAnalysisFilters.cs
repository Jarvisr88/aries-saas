namespace DevExpress.Xpf.Core.FilteringUI
{
    using System;

    [Flags]
    public enum AllowedDataAnalysisFilters
    {
        None = 0,
        Top = 1,
        Bottom = 2,
        AboveAverage = 4,
        BelowAverage = 8,
        Unique = 0x10,
        Duplicate = 0x20,
        All = 0x3f
    }
}

