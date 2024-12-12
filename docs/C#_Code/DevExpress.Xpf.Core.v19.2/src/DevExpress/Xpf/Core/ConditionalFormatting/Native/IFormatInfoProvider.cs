namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using DevExpress.Data;
    using System;

    public interface IFormatInfoProvider
    {
        object GetCellValue(string fieldName);
        object GetCellValueByListIndex(int listIndex, string fieldName);
        object GetTotalSummaryValue(string fieldName, ConditionalFormatSummaryType summaryType);

        DevExpress.Data.ValueComparer ValueComparer { get; }
    }
}

