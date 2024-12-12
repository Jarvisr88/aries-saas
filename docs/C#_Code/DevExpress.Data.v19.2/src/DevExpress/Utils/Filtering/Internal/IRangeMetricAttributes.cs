namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Utils.Filtering;
    using System;

    public interface IRangeMetricAttributes : IMetricAttributes, IUniqueValuesMetricAttributes, ISummaryMetricAttributes
    {
        bool IsNumericRange { get; }

        bool IsTimeSpanRange { get; }

        RangeUIEditorType NumericRangeUIEditorType { get; }

        DevExpress.Utils.Filtering.DateTimeRangeUIEditorType DateTimeRangeUIEditorType { get; }

        string FromName { get; }

        string ToName { get; }

        string NullName { get; }
    }
}

