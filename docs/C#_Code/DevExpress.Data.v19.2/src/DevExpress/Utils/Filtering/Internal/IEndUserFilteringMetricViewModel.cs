namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Filtering;
    using System;

    public interface IEndUserFilteringMetricViewModel
    {
        void EnsureValueType();
        IDisposable LockValue();

        IEndUserFilteringMetric Metric { get; }

        IMetricAttributesQuery Query { get; }

        bool HasValue { get; }

        IValueViewModel Value { get; }

        Type ValueType { get; }

        CriteriaOperator FilterCriteria { get; }
    }
}

