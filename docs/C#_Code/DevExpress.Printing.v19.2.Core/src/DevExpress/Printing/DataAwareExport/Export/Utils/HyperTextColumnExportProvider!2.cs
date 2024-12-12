namespace DevExpress.Printing.DataAwareExport.Export.Utils
{
    using DevExpress.Export;
    using DevExpress.Export.Xl;
    using DevExpress.Printing.ExportHelpers;
    using DevExpress.XtraExport.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    internal class HyperTextColumnExportProvider<TCol, TRow> : RichTextColumnExportProvider<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        public HyperTextColumnExportProvider(TCol target, ColumnExportInfo<TCol, TRow> columnInfo, int cIndex) : base(target, columnInfo, cIndex)
        {
        }

        protected override CellObject CreateCellObject(object value, string hyperlink)
        {
            CellObject obj1 = new CellObject();
            obj1.Value = value;
            obj1.Hyperlink = hyperlink;
            return obj1;
        }

        protected override void ExportDataValue(IXlCell cell, TRow gridRow, int exportRowIndex)
        {
            object obj2 = !base.targetLocalColumn.IsGroupColumn ? (base.columnInfo.View.GetHypertextCellInfo(gridRow, base.targetLocalColumn) ?? base.GetRowCellValueCore(gridRow)) : null;
            base.SetValue(cell, gridRow, obj2, exportRowIndex, SheetAreaType.DataArea);
            if (base.columnInfo.AllowCellMerge)
            {
                base.columnInfo.HelpersProvider.CellMerger.ProcessVerticalMerging(exportRowIndex, gridRow.LogicalPosition, base.targetLocalColumn, cell);
            }
        }

        private void SetTextRunCore(IXlCell cell, XlRichTextString richText, HypertextExportInfoContainer run)
        {
            FormattingUtils.ValidateTextRun(run.TextRun);
            richText.Runs.Add(run.TextRun);
            cell.SetRichText(richText);
        }

        protected override void SetValueCore(IXlCell cell, object value, int rowIndex, bool ignoreOverride = false)
        {
            IList<HypertextExportInfoContainer> list = value as IList<HypertextExportInfoContainer>;
            if (list == null)
            {
                base.SetValueCore(cell, value, rowIndex, false);
            }
            else
            {
                XlRichTextString richText = new XlRichTextString();
                bool flag = false;
                int num = 0;
                while (true)
                {
                    if (num >= list.Count)
                    {
                        if (!flag)
                        {
                            break;
                        }
                        FormattingUtils.SetRichTextCellWrap(cell);
                        return;
                    }
                    HypertextExportInfoContainer run = list[num];
                    if (!string.IsNullOrEmpty(run.Link) && (list.Count == 1))
                    {
                        if (run.TextRun != null)
                        {
                            HyperlinkExporter.SetHyperlink(run.Link, run.TextRun.Text, string.Empty, cell, base.columnInfo.Sheet, true);
                            cell.Formatting.Font = run.TextRun.Font;
                        }
                    }
                    else if (run.TextRun != null)
                    {
                        this.SetTextRunCore(cell, richText, run);
                        flag = FormattingUtils.IsRunContainsNewLineChars(run.TextRun);
                    }
                    else if (run.Image != null)
                    {
                        new PictureExporter<TCol, TRow>(base.columnInfo.ExportInfo).SetPicture(run.Image, base.Position, rowIndex, 0x15, 0x15, XlAnchorType.OneCell);
                    }
                    num++;
                }
            }
        }
    }
}

