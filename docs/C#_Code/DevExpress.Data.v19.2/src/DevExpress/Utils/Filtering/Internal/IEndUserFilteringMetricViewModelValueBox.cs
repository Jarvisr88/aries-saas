namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Runtime.CompilerServices;

    public interface IEndUserFilteringMetricViewModelValueBox
    {
        event EventHandler ValueChanged;

        void EnsureValueType();
        void ReleaseValue();

        System.Type Type { get; }

        IValueViewModel Value { get; }

        IMetricAttributesQuery Query { get; }
    }
}

