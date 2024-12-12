namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Filtering;
    using System;

    public abstract class DurationInterval : Interval<TimeSpan>
    {
        private static readonly TimeSpan IntervalMillisecond;
        private static readonly TimeSpan MaxIntervalBegin;

        static DurationInterval();
        protected DurationInterval(TimeSpan? begin, TimeSpan? end);
        protected DurationInterval(int days, int? hours, int? minutes, int? seconds, int? milliseconds);
        protected internal sealed override bool CanMerge(Interval<TimeSpan> interval);
        private static TimeSpan? CheckMaxValue<T>(T? to, bool exclusive) where T: struct;
        private static TimeSpan? CheckMinValue<T>(T? from, bool exclusive) where T: struct;
        protected sealed override CriteriaOperator GetBinaryCriteria(string path, TimeSpan? value, BinaryOperatorType operatorType);
        private static TimeSpan GetNextIntervalBegin(TimeSpan? value);
        internal static Interval<T> Range<T>(T? from, T? to, bool exclusiveBegin, bool exclusiveEnd) where T: struct;

        public static Interval<TimeSpan> Empty { get; }

        private sealed class EmptyInterval : Interval<TimeSpan>
        {
            internal static readonly Interval<TimeSpan> Instance;

            static EmptyInterval();
            private EmptyInterval();
            protected internal sealed override bool CanMerge(Interval<TimeSpan> interval);
            protected internal sealed override CriteriaOperator GetCriteria(string path);
        }
    }
}

