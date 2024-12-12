namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    internal sealed class DurationIntervalMerged : DurationInterval
    {
        private DurationIntervalMerged(TimeSpan? begin, TimeSpan? end);
        internal static Interval<T> Create<T>(T? begin, T? end) where T: struct;
    }
}

