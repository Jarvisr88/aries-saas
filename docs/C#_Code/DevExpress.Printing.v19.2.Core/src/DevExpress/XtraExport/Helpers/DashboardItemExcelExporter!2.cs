namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Export;
    using DevExpress.Printing.ExportHelpers;
    using DevExpress.Utils;
    using System;
    using System.Runtime.InteropServices;

    public class DashboardItemExcelExporter<TCol, TRow> : GridViewExcelExporter<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        public DashboardItemExcelExporter(IGridView<TCol, TRow> viewToExport) : base(viewToExport)
        {
        }

        public DashboardItemExcelExporter(IGridView<TCol, TRow> viewToExport, IDataAwareExportOptions options) : base(viewToExport, options)
        {
        }

        protected override void AddGroupToList(int startIndex, int endIndex, int groupLevel = -1)
        {
            if (base.ExportInfo.DataAreaBottomRowIndex != 0)
            {
                endIndex = base.ExportInfo.DataAreaBottomRowIndex;
            }
            base.AddGroupToList(startIndex, endIndex, groupLevel);
        }

        protected override bool CanExportRow(IRowBase row) => 
            base.ExportInfo.ComplyWithFormatLimits();

        protected override DataAwareExportContext<TCol, TRow> CreateContext(ExportInfo<TCol, TRow> exportInfo) => 
            new DashboardDataAwareExportContext<TCol, TRow>(exportInfo);

        internal override ExportInfo<TCol, TRow> CreateExportInfo() => 
            new DashboardDataAwareExportInfo<TCol, TRow>(this);

        protected override Action<TCol> ExportColumn() => 
            delegate (TCol gridColumn) {
                int columnGroupLevel = gridColumn.GetColumnGroupLevel();
                while (true)
                {
                    if (base.ExportInfo.GroupsStack.Count > 0)
                    {
                        XlGroup group = base.ExportInfo.GroupsStack.Peek();
                        if (group.Group.OutlineLevel >= (columnGroupLevel + 1))
                        {
                            base.ExportInfo.GroupsStack.Pop();
                            base.Exporter.EndGroup();
                            continue;
                        }
                    }
                    if (!gridColumn.IsGroupColumn || (base.Options.AllowGrouping != DefaultBoolean.True))
                    {
                        base.Context.CreateColumn(gridColumn);
                        return;
                    }
                    base.ExportInfo.ColumnGrouping = true;
                    base.Context.CreateColumn(gridColumn);
                    base.Context.CreateExportDataGroup(columnGroupLevel, gridColumn.LogicalPosition, 0, gridColumn.IsCollapsed);
                    return;
                }
            };

        protected override int ExportDataCore(ref TRow lastExportedRow, ref bool wasDataRow, ref int endExcelGroupIndex, ref int groupId, TRow gridRow, int startExcelGroupIndex)
        {
            lastExportedRow = gridRow;
            int rowLevel = gridRow.GetRowLevel();
            if (!gridRow.IsDataAreaRow)
            {
                base.ExportInfo.DataAreaBottomRowIndex ??= base.ExportInfo.ExportRowIndex;
            }
            endExcelGroupIndex = this.CompleteGrouping(lastExportedRow, endExcelGroupIndex, rowLevel);
            if (this.CanExportRow(gridRow))
            {
                if (!wasDataRow)
                {
                    startExcelGroupIndex = base.ExportInfo.ExportRowIndex;
                }
                base.Context.AddRow(gridRow);
                wasDataRow = true;
            }
            this.ExportStartGroup(ref wasDataRow, startExcelGroupIndex, endExcelGroupIndex, ref groupId, gridRow, rowLevel);
            base.ExportInfo.ReportProgress(base.ExportInfo.ExportRowIndex);
            return startExcelGroupIndex;
        }
    }
}

