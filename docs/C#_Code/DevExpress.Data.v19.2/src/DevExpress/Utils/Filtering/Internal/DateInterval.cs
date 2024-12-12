namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Filtering;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    public abstract class DateInterval : Interval<DateTime>
    {
        [Browsable(false)]
        public static DefaultBoolean AllowIsSameDayFilter;
        [Browsable(false)]
        public static int IsSameDayFilterThreshold;
        private static readonly DateTime MaxIntervalBegin;

        static DateInterval();
        protected DateInterval(DateTime? begin, DateTime? end);
        protected DateInterval(int year, int? month, int? day);
        protected internal sealed override bool CanMerge(Interval<DateTime> interval);
        private static DateTime? CheckMaxValue<T>(T? to, bool exclusive) where T: struct;
        private static DateTime? CheckMinValue<T>(T? from, bool exclusive) where T: struct;
        protected internal sealed override bool Contains(Interval<DateTime> dateInterval);
        internal static bool Contains(Interval<DateTime> interval, DateInterval dateInterval);
        protected sealed override CriteriaOperator GetBinaryCriteria(string path, DateTime? value, BinaryOperatorType operatorType);
        internal static DateTime? GetDayIntervalDate(object value);
        internal static CriteriaOperator GetIsSameDay(string path, IEnumerable<DayInterval> intervals);
        protected static CriteriaOperator GetIsSameDay(string path, DateTime date, int daysCount);
        protected static DateTime GetNextIntervalBegin(DateTime? value);
        protected internal bool IsSameDayFilter(out int daysCount);
        internal static Interval<T> Range<T>(T? from, T? to, bool exclusiveBegin, bool exclusiveEnd) where T: struct;
        protected internal static bool UseIsSameDayFilter();

        public static Interval<DateTime> Empty { get; }

        public static Interval<DateTime> AllDates { get; }

        private sealed class AllDatesInterval : Interval<DateTime>
        {
            internal static readonly Interval<DateTime> Instance;

            static AllDatesInterval();
            private AllDatesInterval();
            protected internal sealed override bool CanMerge(Interval<DateTime> interval);
            protected internal sealed override CriteriaOperator GetCriteria(string path);
            public sealed override string ToString();
        }

        private sealed class EmptyInterval : Interval<DateTime>
        {
            internal static readonly Interval<DateTime> Instance;

            static EmptyInterval();
            private EmptyInterval();
            protected internal sealed override bool CanMerge(Interval<DateTime> interval);
            protected internal sealed override CriteriaOperator GetCriteria(string path);
            public sealed override string ToString();
        }
    }
}

