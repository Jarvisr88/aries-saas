namespace DevExpress.Data.Summary
{
    using DevExpress.Data;
    using System;

    public interface ISummaryItem
    {
        string FieldName { get; }

        SummaryItemType SummaryType { get; }

        string DisplayFormat { get; set; }
    }
}

