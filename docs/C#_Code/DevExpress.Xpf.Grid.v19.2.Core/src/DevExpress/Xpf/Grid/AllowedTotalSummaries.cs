namespace DevExpress.Xpf.Grid
{
    using System;

    [Flags]
    public enum AllowedTotalSummaries
    {
        None = 0,
        Average = 1,
        Max = 2,
        Min = 4,
        Sum = 8,
        All = 15
    }
}

