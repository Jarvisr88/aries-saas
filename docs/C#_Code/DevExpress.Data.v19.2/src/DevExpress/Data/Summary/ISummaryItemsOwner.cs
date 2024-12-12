namespace DevExpress.Data.Summary
{
    using DevExpress.Data;
    using System;
    using System.Collections.Generic;

    public interface ISummaryItemsOwner
    {
        ISummaryItem CreateItem(string fieldName, SummaryItemType summaryType);
        string GetCaptionByFieldName(string fieldName);
        List<string> GetFieldNames();
        List<ISummaryItem> GetItems();
        Type GetTypeByFieldName(string fieldName);
        void SetItems(List<ISummaryItem> items);
    }
}

