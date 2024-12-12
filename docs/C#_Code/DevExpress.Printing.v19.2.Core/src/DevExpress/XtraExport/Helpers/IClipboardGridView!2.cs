namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Export.Xl;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public interface IClipboardGridView<TCol, TRow> : IGridView<TCol, TRow>, IGridViewBase<TCol, TRow, TCol, TRow> where TCol: IColumn where TRow: IRowBase
    {
        bool CanCopyToClipboard();
        bool GetAlignGroupSummaryInGroupRow();
        XlCellFormatting GetCellAppearance(TRow row, TCol col);
        string GetRowCellDisplayText(TRow row, string columnName);
        int GetSelectedCellsCount();
        IEnumerable<TCol> GetSelectedColumns();
        IEnumerable<TRow> GetSelectedRows();
        bool GetShowGroupedColumns();
        void ProgressBarCallBack(int progress);
        bool RaiseClipboardDataRowCopying(int rowHandle, IEnumerable<TCol> selectedColumns, IEnumerable<ClipboardCellInfo> rowInfo);
        bool RaiseClipboardGroupRowCopying(int rowHandle, IEnumerable<TCol> selectedColumns, IEnumerable<ClipboardCellInfo> rowInfo, ClipboardInfoType type = 3);
        bool RaiseClipboardHeaderRowCopying(IEnumerable<TCol> selectedColumns, IEnumerable<ClipboardCellInfo> rowInfo);
        bool UseHierarchyIndent(TRow row, TCol col);

        bool AllowHtmlDrawHeaders { get; }

        bool AllowHtmlDrawGroups { get; }
    }
}

