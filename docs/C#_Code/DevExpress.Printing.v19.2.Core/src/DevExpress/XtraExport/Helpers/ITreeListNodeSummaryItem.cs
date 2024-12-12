namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Data.Summary;
    using System;

    public interface ITreeListNodeSummaryItem : ISummaryItemEx, ISummaryItem
    {
        bool IsRecursive { get; }

        bool IsNodeSummaryItem { get; set; }
    }
}

