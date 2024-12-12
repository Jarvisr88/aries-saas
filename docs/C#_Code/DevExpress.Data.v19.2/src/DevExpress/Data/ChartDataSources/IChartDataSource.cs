namespace DevExpress.Data.ChartDataSources
{
    using System;
    using System.Runtime.CompilerServices;

    public interface IChartDataSource
    {
        event DataChangedEventHandler DataChanged;

        string SeriesDataMember { get; }

        string ArgumentDataMember { get; }

        string ValueDataMember { get; }

        DateTimeMeasureUnitNative? DateTimeArgumentMeasureUnit { get; }
    }
}

