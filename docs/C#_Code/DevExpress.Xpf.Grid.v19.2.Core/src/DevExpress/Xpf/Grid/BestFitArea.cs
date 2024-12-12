namespace DevExpress.Xpf.Grid
{
    using System;

    [Flags]
    public enum BestFitArea
    {
        None = 0,
        Header = 1,
        Rows = 2,
        TotalSummary = 4,
        GroupSummary = 8,
        GroupFooter = 0x10,
        All = 0x1f
    }
}

