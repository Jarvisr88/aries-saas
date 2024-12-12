namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Diagnostics;

    [DebuggerDisplay("{Begin.Value.ToShortDateString(),nq}-{End.Value.ToShortDateString(),nq}")]
    internal sealed class DateRangeInterval : DateInterval
    {
        internal DateRangeInterval(DateTime? begin, DateTime? end);
        protected sealed override CriteriaOperator GetRangeCriteria(string path, CriteriaOperator fromCriteria, CriteriaOperator toCriteria);
    }
}

