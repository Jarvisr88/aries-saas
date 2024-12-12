namespace DevExpress.Xpf.Core.FilteringUI
{
    using System;

    [Flags]
    public enum AllowedDateTimeFilters
    {
        None = 0,
        MultipleDateRanges = 1,
        SingleDateRange = 2,
        IsBeyondThisYear = 4,
        IsLaterThisYear = 8,
        IsLaterThisMonth = 0x10,
        IsNextWeek = 0x20,
        IsLaterThisWeek = 0x40,
        IsTomorrow = 0x80,
        IsToday = 0x100,
        IsYesterday = 0x200,
        IsEarlierThisWeek = 0x400,
        IsLastWeek = 0x800,
        IsEarlierThisMonth = 0x1000,
        IsEarlierThisYear = 0x2000,
        IsPriorThisYear = 0x4000,
        All = 0x7fff
    }
}

