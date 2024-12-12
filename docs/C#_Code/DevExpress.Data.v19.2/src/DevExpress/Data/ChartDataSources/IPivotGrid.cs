namespace DevExpress.Data.ChartDataSources
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;

    public interface IPivotGrid : IChartDataSource
    {
        void LockListChanged();
        void UnlockListChanged();

        IList<string> ArgumentColumnNames { get; }

        IList<string> ValueColumnNames { get; }

        bool RetrieveDataByColumns { get; set; }

        bool RetrieveEmptyCells { get; set; }

        DefaultBoolean RetrieveFieldValuesAsText { get; set; }

        bool SelectionSupported { get; }

        bool SelectionOnly { get; set; }

        bool SinglePageSupported { get; }

        bool SinglePageOnly { get; set; }

        bool RetrieveColumnTotals { get; set; }

        bool RetrieveColumnGrandTotals { get; set; }

        bool RetrieveColumnCustomTotals { get; set; }

        bool RetrieveRowTotals { get; set; }

        bool RetrieveRowGrandTotals { get; set; }

        bool RetrieveRowCustomTotals { get; set; }

        bool RetrieveDateTimeValuesAsMiddleValues { get; set; }

        int MaxAllowedSeriesCount { get; set; }

        int MaxAllowedPointCountInSeries { get; set; }

        int UpdateDelay { get; set; }

        int AvailableSeriesCount { get; }

        IDictionary<object, int> AvailablePointCountInSeries { get; }

        IDictionary<DateTime, DateTimeMeasureUnitNative> DateTimeMeasureUnitByArgument { get; }
    }
}

