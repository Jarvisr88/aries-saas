namespace DevExpress.Data
{
    using DevExpress.Data.Helpers;
    using System;

    public interface IDataControllerSort
    {
        void AfterGrouping();
        void AfterSorting();
        void BeforeGrouping();
        void BeforeSorting();
        ExpressiveSortInfo.Row GetCompareRowsMethodInfo();
        string GetDisplayText(int listSourceRow, DataColumnInfo info, object value, string columnName);
        string[] GetFindByPropertyNames();
        ExpressiveSortInfo.Cell GetSortCellMethodInfo(DataColumnInfo dataColumnInfo, Type baseExtractorType, ColumnSortOrder order);
        ExpressiveSortInfo.Cell GetSortGroupCellMethodInfo(DataColumnInfo dataColumnInfo, Type baseExtractorType);
        bool? IsEqualGroupValues(int listSourceRow1, int listSourceRow2, object value1, object value2, DataColumnInfo sortColumn);
        bool RequireDisplayText(DataColumnInfo column);
        void SubstituteSortInfo(SubstituteSortInfoEventArgs args);
    }
}

