namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data.Summary;
    using System;

    public interface IAlignmentItem : ISummaryItem
    {
        GridSummaryItemAlignment Alignment { get; set; }
    }
}

