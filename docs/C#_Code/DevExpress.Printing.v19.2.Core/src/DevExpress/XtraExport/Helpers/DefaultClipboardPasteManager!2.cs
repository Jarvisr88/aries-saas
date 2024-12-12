namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Export;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class DefaultClipboardPasteManager<TCol, TRow> : IClipboardPasteManager where TCol: class, IColumn where TRow: class, IRowBase
    {
        private IClipboardPasteHelper<TCol, TRow> helper;

        public DefaultClipboardPasteManager(IClipboardPasteHelper<TCol, TRow> helper)
        {
            this.helper = helper;
        }

        public void Paste(IClipboardData data)
        {
            this.helper.BeginUpdate(data);
            List<TRow> rows = new List<TRow>();
            TRow startRow = this.helper.GetStartRow();
            IEnumerable<TCol> targetColumns = this.helper.GetTargetColumns(data.ColumnCount, startRow);
            int pasteRowIndex = 0;
            int rowsCountForPasting = data.RowCount - data.Rows.Count<IClipboardRow>(x => ((DefaultClipboardPasteManager<TCol, TRow>) this).helper.IsCaptionRows(x, targetColumns));
            this.helper.SetClipboardRowsInfo(rowsCountForPasting, data.RowCount);
            for (int i = 0; (i < data.RowCount) && !this.helper.IsCancelPending; i++)
            {
                this.helper.ProgressBarCallBack((int) Math.Round((double) ((i * 100.0) / ((double) data.RowCount))));
                IClipboardRow row = data.Rows[i];
                if ((row != null) && ((row.Cells != null) && !this.helper.IsCaptionRows(row, targetColumns)))
                {
                    int index = 0;
                    while (true)
                    {
                        if ((index >= row.Cells.Length) || (index >= targetColumns.Count<TCol>()))
                        {
                            PasteRow<TCol> pasteRow = null;
                            TRow rowBase = this.helper.TryGetPasteRow(row, targetColumns, data.ColumnCount, pasteRowIndex, out pasteRow);
                            if (rowBase != null)
                            {
                                pasteRowIndex++;
                                foreach (PasteCellValue<TCol> value2 in pasteRow.Cells)
                                {
                                    this.helper.SetRowCellValue(rowBase, value2.Column, value2.Value, value2.PasteMode);
                                }
                                if (this.helper.PasteMode == PasteMode.Append)
                                {
                                    IEnumerable<TCol> groupsColumn = this.helper.GetGroupsColumn(startRow);
                                    if (groupsColumn != null)
                                    {
                                        foreach (TCol local4 in groupsColumn)
                                        {
                                            this.helper.SetRowCellValueFromGroupColumn(rowBase, local4, startRow);
                                        }
                                    }
                                }
                                this.helper.UpdateRowAfterPaste(rowBase);
                                rows.Add(rowBase);
                            }
                            break;
                        }
                        TCol iColumn = targetColumns.ElementAt<TCol>(index);
                        DefaultClipboardConverter.ConvertIClipboardRowValue(iColumn, row, index);
                        this.helper.ConvertIClipboardRowValue(iColumn, row, index);
                        index++;
                    }
                }
            }
            if (this.helper.IsCancelPending)
            {
                this.helper.AbortPastedRows(rows);
                this.helper.EndUpdate();
                this.helper.RestoreSelection();
            }
            else
            {
                this.helper.ProgressBarCallBack(100);
                this.helper.EndUpdate();
                this.helper.SelectPastedRows(rows, targetColumns, data.ColumnCount);
            }
        }
    }
}

