namespace DevExpress.Xpf.Core.FilteringUI
{
    using System;

    [Flags]
    internal enum AllowedCustomDateFilters
    {
        None = 0,
        BetweenDates = 1,
        IsOnDates = 2,
        IsOnDate = 4,
        IsNotOnDate = 8,
        All = 15
    }
}

