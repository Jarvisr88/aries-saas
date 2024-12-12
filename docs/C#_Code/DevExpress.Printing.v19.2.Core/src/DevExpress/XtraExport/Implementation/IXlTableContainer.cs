namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using System;

    public interface IXlTableContainer
    {
        void AddTable(XlTable table);
        bool ContainsTable(IXlTable table);
        void DeactivateTable(XlTable table);
        bool HasIntersectionWithTable(XlCellRange range);
        bool IsValidRange(XlCellRange range);
        void SetPendingTable(XlTable table);
    }
}

