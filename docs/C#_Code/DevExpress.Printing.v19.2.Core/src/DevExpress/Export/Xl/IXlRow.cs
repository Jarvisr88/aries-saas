namespace DevExpress.Export.Xl
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public interface IXlRow : IDisposable
    {
        void ApplyFormatting(XlCellFormatting formatting);
        IXlTable BeginTable(IEnumerable<string> columnNames, bool hasHeaderRow);
        IXlTable BeginTable(IEnumerable<XlTableColumnInfo> columns, bool hasHeaderRow, XlCellFormatting headerRowFormatting);
        IXlTable BeginTable(IEnumerable<string> columnNames, bool hasHeaderRow, XlCellFormatting headerRowFormatting);
        void BlankCells(int count, XlCellFormatting formatting);
        void BulkCells(IEnumerable values, XlCellFormatting formatting);
        IXlCell CreateCell();
        IXlCell CreateCell(int columnIndex);
        void EndTable(IXlTable table, bool hasTotalRow);
        void SkipCells(int count);

        int RowIndex { get; }

        XlCellFormatting Formatting { get; set; }

        bool IsHidden { get; set; }

        bool IsCollapsed { get; set; }

        int HeightInPixels { get; set; }

        float HeightInPoints { get; set; }
    }
}

