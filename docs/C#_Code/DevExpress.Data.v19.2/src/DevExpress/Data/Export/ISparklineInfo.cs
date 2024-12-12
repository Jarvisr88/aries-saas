namespace DevExpress.Data.Export
{
    using System;
    using System.Drawing;

    public interface ISparklineInfo
    {
        ExportSparklineType SparklineType { get; }

        Color ColorSeries { get; }

        Color ColorNegative { get; }

        Color ColorMarker { get; }

        Color ColorFirst { get; }

        Color ColorLast { get; }

        Color ColorHigh { get; }

        Color ColorLow { get; }

        double LineWeight { get; }

        bool HighlightNegative { get; }

        bool HighlightFirst { get; }

        bool HighlightLast { get; }

        bool HighlightHighest { get; }

        bool HighlightLowest { get; }

        bool DisplayMarkers { get; }

        bool SpecificSparklineType { get; }
    }
}

