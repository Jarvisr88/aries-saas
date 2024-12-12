namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Export;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public interface IClipboardPasteHelper<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        void AbortPastedRows(List<TRow> rows);
        void BeginUpdate(IClipboardData data = null);
        void ConvertIClipboardRowValue(TCol targetColumn, IClipboardRow row, int i);
        void EndUpdate();
        TRow GetAppendRow(int clipboardRowPosition, TRow startRow);
        IEnumerable<TCol> GetGroupsColumn(TRow startRow);
        TRow GetStartRow();
        IEnumerable<TCol> GetTargetColumns(int clipboardColumnCount, TRow startRow);
        TRow GetUpdateRow(int clipboardRowPosition, TRow startRow);
        bool IsCaptionRows(IClipboardRow row, IEnumerable<TCol> targetColumns);
        void ProgressBarCallBack(int progress);
        void RestoreSelection();
        void SelectPastedRows(List<TRow> updatedRows, IEnumerable<TCol> targetColumns, int clipboardColumnCount);
        void SetClipboardRowsInfo(int rowsCountForPasting, int totalRowCount);
        void SetRowCellValue(TRow rowBase, TCol col, object rowCellValue, RowPasteMode pasteMode = 0);
        void SetRowCellValueFromGroupColumn(TRow rowBase, TCol groupColumn, TRow startRow);
        TRow TryGetPasteRow(IClipboardRow row, IEnumerable<TCol> targetColumns, int pasteRowColumnCount, int pasteRowIndex, out PasteRow<TCol> pasteRow);
        void UpdateRowAfterPaste(TRow rowBase);

        bool IsCancelPending { get; }

        DevExpress.Export.PasteMode PasteMode { get; }
    }
}

