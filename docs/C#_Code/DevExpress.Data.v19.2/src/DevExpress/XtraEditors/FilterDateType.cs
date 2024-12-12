namespace DevExpress.XtraEditors
{
    using System;

    [Flags]
    public enum FilterDateType
    {
        public const FilterDateType None = FilterDateType.None;,
        public const FilterDateType SpecificDate = FilterDateType.SpecificDate;,
        public const FilterDateType BeyondThisYear = FilterDateType.BeyondThisYear;,
        public const FilterDateType LaterThisYear = FilterDateType.LaterThisYear;,
        public const FilterDateType LaterThisMonth = FilterDateType.LaterThisMonth;,
        public const FilterDateType LaterThisWeek = FilterDateType.LaterThisWeek;,
        public const FilterDateType NextWeek = FilterDateType.NextWeek;,
        public const FilterDateType Tomorrow = FilterDateType.Tomorrow;,
        public const FilterDateType Today = FilterDateType.Today;,
        public const FilterDateType Yesterday = FilterDateType.Yesterday;,
        public const FilterDateType EarlierThisWeek = FilterDateType.EarlierThisWeek;,
        public const FilterDateType LastWeek = FilterDateType.LastWeek;,
        public const FilterDateType EarlierThisMonth = FilterDateType.EarlierThisMonth;,
        public const FilterDateType EarlierThisYear = FilterDateType.EarlierThisYear;,
        public const FilterDateType PriorThisYear = FilterDateType.PriorThisYear;,
        public const FilterDateType Empty = FilterDateType.Empty;,
        public const FilterDateType User = FilterDateType.User;,
        public const FilterDateType Beyond = FilterDateType.Beyond;,
        public const FilterDateType ThisWeek = FilterDateType.ThisWeek;,
        public const FilterDateType ThisMonth = FilterDateType.ThisMonth;,
        public const FilterDateType MonthAfter1 = FilterDateType.MonthAfter1;,
        public const FilterDateType MonthAfter2 = FilterDateType.MonthAfter2;,
        public const FilterDateType MonthAgo1 = FilterDateType.MonthAgo1;,
        public const FilterDateType MonthAgo2 = FilterDateType.MonthAgo2;,
        public const FilterDateType MonthAgo3 = FilterDateType.MonthAgo3;,
        public const FilterDateType MonthAgo4 = FilterDateType.MonthAgo4;,
        public const FilterDateType MonthAgo5 = FilterDateType.MonthAgo5;,
        public const FilterDateType MonthAgo6 = FilterDateType.MonthAgo6;,
        public const FilterDateType Earlier = FilterDateType.Earlier;
    }
}

