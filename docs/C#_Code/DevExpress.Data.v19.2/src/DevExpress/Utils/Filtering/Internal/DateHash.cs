namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct DateHash : IEquatable<DateHash>
    {
        public static readonly DateHash Empty;
        public static readonly DateHash All;
        public static readonly DateHash NotLoaded;
        private const int DAY_MASK = 0x1f;
        private const int MONTH_MASK = 480;
        private const int YEAR_MASK = 0x7ffe00;
        private const int ORIGIN_MASK = 0x6000000;
        private const int YEAR_ORIGIN_MASK = 0x4000000;
        private const int MONTH_ORIGIN_MASK = 0x2000000;
        private const int HASH_MASK = -100663297;
        private readonly int value;
        private DateHash(DateHash.Kind kind);
        internal DateHash(int year);
        internal DateHash(int year, int month);
        internal DateHash(int year, int month, int day);
        internal DateHash(int year, int month, int day, int origin);
        public bool Equals(DateHash node);
        public override bool Equals(object obj);
        public override int GetHashCode();
        [Browsable(false)]
        public int Year { get; }
        [Browsable(false)]
        public int Month { get; }
        [Browsable(false)]
        public int Day { get; }
        [Browsable(false)]
        public bool IsDay { get; }
        [Browsable(false)]
        public bool IsMonth { get; }
        [Browsable(false)]
        public bool IsYear { get; }
        [Browsable(false)]
        public bool IsExpandable { get; }
        [Browsable(false)]
        public bool IsOrigin { get; }
        [Browsable(false)]
        public bool IsMonthOrigin { get; }
        [Browsable(false)]
        public bool IsYearOrigin { get; }
        [Browsable(false)]
        public int ExpandLevel { get; }
        [Browsable(false)]
        public string SearchMember { get; }
        internal string GetText(DateHashTreeIndices indices);
        internal string GetText(DateHashTreeIndices indices, int level);
        public override string ToString();
        internal object[] Path { get; }
        internal int YearKey { get; }
        internal int MonthKey { get; }
        internal int DayKey(out int monthKey);
        internal int ParentKey { get; }
        internal int GroupKey { get; }
        internal bool TryGetInterval(out Interval<DateTime> interval);
        internal bool Match(Interval<DateTime> range);
        internal bool Match(Interval<DateTime>[] intervals);
        internal DateTime GetDate();
        static DateHash();
        private enum Kind
        {
            public const DateHash.Kind Empty = DateHash.Kind.Empty;,
            public const DateHash.Kind All = DateHash.Kind.All;,
            public const DateHash.Kind NotLoaded = DateHash.Kind.NotLoaded;
        }
    }
}

