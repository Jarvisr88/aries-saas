namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data.Summary;
    using System;

    public interface IGridSummaryItemsOwner : ISummaryItemsOwner
    {
        string FormatSummaryItemCaption(ISummaryItem summaryItem, string defaultCaption);
        bool IsSummaryItemExists(ISummaryItem summaryItem);
    }
}

