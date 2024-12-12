namespace DevExpress.Printing.DataAwareExport.Export.Utils
{
    using DevExpress.Export;
    using DevExpress.Export.Xl;
    using DevExpress.Printing.ExportHelpers;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Text;

    internal class RichTextColumnExportProvider<TCol, TRow> : ColumnExportProvider<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        public RichTextColumnExportProvider(TCol target, ColumnExportInfo<TCol, TRow> columnInfo, int cIndex) : base(target, columnInfo, cIndex)
        {
        }

        protected override CellObject CreateCellObject(object value, string hyperlink)
        {
            IList<XlRichTextRun> list = value as IList<XlRichTextRun>;
            StringBuilder builder = new StringBuilder();
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    XlRichTextRun run = list[i];
                    builder.Append(run.Text);
                }
            }
            CellObject obj1 = new CellObject();
            obj1.Value = builder.ToString();
            obj1.Hyperlink = hyperlink;
            return obj1;
        }

        protected override void ExportDataValue(IXlCell cell, TRow gridRow, int exportRowIndex)
        {
            IList<XlRichTextRun> list = !base.targetLocalColumn.IsGroupColumn ? base.columnInfo.View.GetRichTextRuns(gridRow, base.targetLocalColumn) : null;
            base.SetValue(cell, gridRow, list, exportRowIndex, SheetAreaType.DataArea);
            if (base.columnInfo.AllowCellMerge)
            {
                base.columnInfo.HelpersProvider.CellMerger.ProcessVerticalMerging(exportRowIndex, gridRow.LogicalPosition, base.targetLocalColumn, cell);
            }
        }

        protected override void SetValueCore(IXlCell cell, object value, int rowIndex, bool ignoreOverride = false)
        {
            IList<XlRichTextRun> runs = value as IList<XlRichTextRun>;
            if (runs != null)
            {
                this.SetRichTextFromRuns(cell, runs);
            }
            else
            {
                base.SetValueCore(cell, value, rowIndex, false);
            }
        }
    }
}

