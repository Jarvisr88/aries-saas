namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Filtering;
    using System;

    internal sealed class DateIntervalMerged : DateInterval
    {
        private DateIntervalMerged(DateTime? begin, DateTime? end);
        internal static Interval<T> Create<T>(T? begin, T? end) where T: struct;
        protected sealed override CriteriaOperator GetRangeCriteria(string path, CriteriaOperator fromCriteria, CriteriaOperator toCriteria);
    }
}

