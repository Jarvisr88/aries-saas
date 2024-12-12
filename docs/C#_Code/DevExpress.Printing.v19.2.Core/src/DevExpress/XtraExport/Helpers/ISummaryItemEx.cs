namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Data.Summary;
    using System;

    public interface ISummaryItemEx : ISummaryItem
    {
        object GetSummaryValueByGroupId(int groupId);

        object SummaryValue { get; }

        string ShowInColumnFooterName { get; }

        bool AlignByColumnInFooter { get; }

        string SummaryText { get; }
    }
}

