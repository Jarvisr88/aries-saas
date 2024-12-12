namespace DevExpress.Printing.DataAwareExport.Export.Utils
{
    using DevExpress.Data;
    using DevExpress.Export.Xl;
    using System;

    internal static class SummaryExportUtils
    {
        public static XlSummary ConvertSummaryItemTypeToExcel(SummaryItemType sit)
        {
            switch (sit)
            {
                case SummaryItemType.Sum:
                    return XlSummary.Sum;

                case SummaryItemType.Min:
                    return XlSummary.Min;

                case SummaryItemType.Max:
                    return XlSummary.Max;

                case SummaryItemType.Count:
                    return XlSummary.CountA;

                case SummaryItemType.Average:
                    return XlSummary.Average;
            }
            return (XlSummary) 0;
        }
    }
}

